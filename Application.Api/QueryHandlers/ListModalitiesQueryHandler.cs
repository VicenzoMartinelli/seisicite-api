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
  public class ListModalitiesQueryHandler : IRequestHandler<ListModalitiesQuery, List<string>>
  {
    private readonly IRepository _repository;

    public ListModalitiesQueryHandler(IRepository repository)
    {
      _repository = repository;
    }
    public Task<List<string>> Handle(ListModalitiesQuery request, CancellationToken cancellationToken)
    {
      var modalities = _repository.Query<Article>()
        .Select(x => x.Modality)
        .OrderBy(x => x)
        .Distinct();

      return Task.FromResult(modalities.ToList());
    }
  }
}
