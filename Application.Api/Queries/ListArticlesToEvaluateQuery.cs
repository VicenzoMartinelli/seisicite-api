using Application.Api.ViewModels;
using Domains.Article;
using MediatR;
using System.Collections.Generic;

namespace Application.Api.Queries
{
  public class ListArticlesToEvaluateQuery : IRequest<List<ArticleViewModel>>
  {
    public string EvaluatorId { get; set; }
    public EEventIdentifier EEvent { get; set; }
    public QueryToEvaluateType Type { get; set; }
  }

  public enum QueryToEvaluateType
  {
    ToEvaluate = 1,
    Opened = 2,
    Closed = 3
  }
}