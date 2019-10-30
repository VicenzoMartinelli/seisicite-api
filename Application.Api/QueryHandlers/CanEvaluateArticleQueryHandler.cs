using Application.Api.Queries;
using Domain.Core.Enumerators;
using Domain.Core.Notifications;
using Domain.Interfaces;
using Domains.Article;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Api.QueryHandlers
{
  public class CanEvaluateArticleQueryHandler : IRequestHandler<CanEvaluateArticleQuery, bool>
  {
    private readonly IRepository _repository;
    private readonly NotificationContext _notificationContext;

    public CanEvaluateArticleQueryHandler(
      IRepository repository,
      NotificationContext notificationContext)
    {
      _repository = repository;
      _notificationContext = notificationContext;
    }

    public async Task<bool> Handle(CanEvaluateArticleQuery request, CancellationToken cancellationToken)
    {
      var article = await _repository.GetByIdAsync<Article>(request.Id);

      if (article == null)
      {
        return false;
      }

      var turnoAtual = GetTurno(DateTime.Now);
      var turnoArtigo = GetTurno(article.StartDate);

      if (DateTime.Now.Date != article.StartDate.Date || turnoAtual == -1 || turnoAtual != turnoArtigo)
      {
        _notificationContext.PushNotification(ReturnCode.NaoEhPossivelEditarAvaliacaoArtigo);
        return false;
      }

      return article.EvaluatorId == request.EvaluatorId || article.Evaluator2Id == request.EvaluatorId;
    }

    private int GetTurno(DateTime time)
    {
      if (time.Hour > 11)
      {
        if (time.Hour < 16)
        {
          return 0;
        }
        else if (time.Hour < 22)
        {
          return 1;
        }
      }

      return -1;
    }
  }
}
