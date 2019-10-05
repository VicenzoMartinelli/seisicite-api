using Application.Api.Queries;
using Application.Api.ViewModels;
using Domain.Domains.Article;
using Domain.Interfaces;
using Domains.Article;
using Infra.Data.MongoIdentityStore;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Api.QueryHandlers
{
  public class ListAvaliadoresByEventoQueryHandler : IRequestHandler<ListAvaliadoresByEventoQuery, List<AvaliadorViewModel>>
  {
    private readonly IRepository _repository;

    public ListAvaliadoresByEventoQueryHandler(IRepository repository)
    {
      _repository = repository;
    }
    public async Task<List<AvaliadorViewModel>> Handle(ListAvaliadoresByEventoQuery request, CancellationToken cancellationToken)
    {
      var filterEvent = request.Event == EEventIdentifier.Sei ?
        Builders<Person>.Filter.Eq(x => x.IsSei, true) :
        Builders<Person>.Filter.Eq(x => x.IsSicite, true);

      var filters = new List<FilterDefinition<Person>>() {
        Builders<Person>.Filter.Eq(x => x.Type, EUserType.Evaluator),
        Builders<Person>.Filter.Eq(x => x.Approved, true),
        filterEvent
      };

      if (!string.IsNullOrEmpty(request.Modality))
      {
        filters.Add(Builders<Person>.Filter.AnyEq(x => x.AttendedModalities, request.Modality));
      }

      var items = await _repository.GetByFilter(Builders<Person>.Filter.And(filters));

      return items.Select(x => new AvaliadorViewModel()
      {
        Id = x.Id,
        Name = x.Name
      }).ToList();
    }
  }
}
