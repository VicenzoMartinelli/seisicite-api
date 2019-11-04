using Application.Api.Queries;
using Application.Api.ViewModels;
using Domain.Domains.Article;
using Domain.Interfaces;
using Domains.Article;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Api.QueryHandlers
{
  public class ReportArticlesNoteQueryHandler : IRequestHandler<ReportArticlesNoteQuery, List<ArticleFinalReportViewModel>>
  {
    private readonly IRepository _repository;

    public ReportArticlesNoteQueryHandler(IRepository repository)
    {
      _repository = repository;
    }
    public async Task<List<ArticleFinalReportViewModel>> Handle(ReportArticlesNoteQuery request, CancellationToken cancellationToken)
    {
      var articlesQuery = _repository.Query<Article>()
        .Where(x => x.NotaConhecimentoAssunto != 0 && x.NotaConhecimentoAssunto2 != 0)
        .Where(x => x.Modality == request.Modality)
        .Where(x => x.Event == request.Event);

      if (request.Type.HasValue)
      {
        articlesQuery = articlesQuery.Where(x => x.ApresentationType == request.Type.Value);
      }

      var rt = new List<ArticleFinalReportViewModel>();

      foreach (var article in articlesQuery.ToList())
      {
        rt.Add(new ArticleFinalReportViewModel()
        {
          Title = article.Title,
          SubmissionId = article.SubmissionId,
          CommissionNote = article.CommissionNote,
          Evaluator1Average = article.FinalAverage,
          Evaluator1Name = _repository.Query<Person>().Where(x => x.Id == article.EvaluatorId).Select(x => x.Name).FirstOrDefault(),
          Evaluator2Average = article.FinalAverage2,
          Evaluator2Name = _repository.Query<Person>().Where(x => x.Id == article.Evaluator2Id).Select(x => x.Name).FirstOrDefault(),
          FinalAverage = string.IsNullOrEmpty(article.Evaluator2Id) ? Math.Round((article.FinalAverage + article.CommissionNote) / 2, 2) : Math.Round((article.FinalAverage + article.FinalAverage2 + article.CommissionNote) / 3, 2)
        });
      }

      return rt.OrderByDescending(x => x.FinalAverage).ToList();
    }
  }
}
