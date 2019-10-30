using Application.Api.Queries;
using Domain.Interfaces;
using Domains.Article;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Api.QueryHandlers
{
  public class ListInstitutionsQueryHandler : IRequestHandler<ListInstitutionsQuery, List<string>>
  {
    private readonly IRepository _repository;

    public ListInstitutionsQueryHandler(IRepository repository)
    {
      _repository = repository;
    }
    public Task<List<string>> Handle(ListInstitutionsQuery request, CancellationToken cancellationToken)
    {
      var insts = _repository.Query<Article>()
        .Where(x => x.PrimaryAuthor.Institution != null)
        .Where(x => x.PrimaryAuthor.Institution != "")
        .Select(x => x.PrimaryAuthor.Institution)
        .Distinct()
        .OrderBy(x => x);

      return Task.FromResult(insts.ToList());
    }
  }
}
