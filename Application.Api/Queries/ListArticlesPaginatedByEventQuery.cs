using Application.Api.ViewModels;
using Domains.Article;
using Infra.CrossCuting.Shared.Models;
using MediatR;

namespace Application.Api.Queries
{
  public class ListArticlesPaginatedByEventQuery : IRequest<PaginatedList<ArticleViewModel>>
  {
    public EEventIdentifier Event { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; } = 9;
  }
}