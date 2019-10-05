using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Api.Commands;
using Application.Api.Queries;
using Domain.Interfaces;
using Domains.Article;
using MediatR;
using Application.Api.ViewModels;
using Infra.CrossCuting.Shared.Models;

namespace Application.Api.QueryHandlers
{
  public class ListArticlesPaginatedByEventQueryHandler : IRequestHandler<ListArticlesPaginatedByEventQuery, PaginatedList<ArticleViewModel>>
  {
    private readonly IRepository _repository;

    public ListArticlesPaginatedByEventQueryHandler(IRepository repository)
    {
      _repository = repository;
    }
    public async Task<PaginatedList<ArticleViewModel>> Handle(ListArticlesPaginatedByEventQuery request, CancellationToken cancellationToken)
    {
      var query = await _repository.QueryAsync<Article>();

      var skip = request.Page == 0 ? 0 : (request.Page - 1) * request.PageSize;
      var take = request.PageSize;

      var items = query
      .Where(x => x.Event == request.Event)
      //.Skip(skip)
      //.Take(take)
      .OrderByDescending(x => x.StartDate);

      return new PaginatedList<ArticleViewModel>()
      {
        Page = request.Page++,
        Items = items.ToList().Select(x => new ArticleViewModel(){
            Id            = x.Id,
            SubmissionId  = x.SubmissionId,
            Title         = x.Title,
            Resume        = x.Resume,
            Building      = x.Building,
            Modality      = x.Modality,
            Room          = x.Room,
            PrimaryAuthor = x.PrimaryAuthor,
            StartDate     = x.StartDate.ToLocalTime(),
            Type          = x.ApresentationType,
            Evaluator2Id  = x.Evaluator2Id,
            EvaluatorId   = x.EvaluatorId,
            LocalDetails  = x.LocalDetails
        }).ToList()
      };
    }
  }
}
