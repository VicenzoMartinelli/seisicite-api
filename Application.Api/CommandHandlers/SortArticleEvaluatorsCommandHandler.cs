using Domain.Core.Notifications;
using Domain.Domains.Article;
using Domain.Interfaces;
using Domains.Article;
using MediatR;
using Services.Seisicite.Api.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Seisicite.Api.CommandHandlers
{
  public class SortArticleEvaluatorsCommandHandler : IRequestHandler<SortArticleEvaluatorsCommand, bool>
  {
    private readonly IRepository _repository;
    private readonly NotificationContext _notificationContext;

    public SortArticleEvaluatorsCommandHandler(
      IRepository repository,
      NotificationContext notificationContext)
    {
      _repository = repository;
      _notificationContext = notificationContext;
    }

    public async Task<bool> Handle(SortArticleEvaluatorsCommand request, CancellationToken cancellationToken)
    {
      var allArticles = _repository
        .Query<Article>()
        .Where(x => x.Event == request.Event)
        .Where(x => x.EvaluatorId == null || x.Evaluator2Id == null);

      var modalities = allArticles
        .Select(x => x.Modality)
        .Distinct();

      Func<Person, bool> pred = (x) => x.IsSei;

      if (request.Event == EEventIdentifier.Sicite)
      {
        pred = (x) => x.IsSicite;
      }

      var allEvaluators = _repository
        .Query<Person>()
        .Where(pred)
        .OrderBy(x => x.ArticlesCount);

      foreach (var modality in modalities)
      {
        var evaluators = allEvaluators.Where(x => x.AttendedModalities.Any(attm => attm == modality)).ToList();

        if (evaluators.Any())
        {
          foreach (var article in allArticles.Where(x => x.Modality == modality))
          {
            var evaluatorsSameInstitution = evaluators.Where(x => x.Institution.Equals(article.PrimaryAuthor.Institution));

            var evSort = evaluators.Except(evaluatorsSameInstitution).Concat(evaluatorsSameInstitution);

            var ev1 = evSort.Take(1).FirstOrDefault()?.IncrementArticleCount();
            article.EvaluatorId = ev1?.Id;

            var ev2 = evSort.Skip(1).Take(1).FirstOrDefault()?.IncrementArticleCount();
            article.Evaluator2Id = ev2?.Id;

            await _repository.SaveOrUpdateAsync(article, article.Id);

            evaluators = evaluators.OrderBy(x => x.ArticlesCount).ToList();
          }

          await Task.WhenAll(evaluators.Select(_ => _repository.SaveOrUpdateAsync(_, _.Id)));
        }
      }

      return true;
    }

  }
}
