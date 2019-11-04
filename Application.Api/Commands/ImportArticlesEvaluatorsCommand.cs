using Application.Api.ViewModels;
using Domains.Article;
using MediatR;
using System.Collections.Generic;

namespace Application.Api.Commands
{
  public class ImportArticlesEvaluatorsCommand : IRequest<bool>
  {
    public List<ArticleEvaluatorsImport> Articles { get; set; }
    public EEventIdentifier Event { get; set; }
  }
}
