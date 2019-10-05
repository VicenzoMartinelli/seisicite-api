using Application.Api.Queries;
using Application.Api.ViewModels;
using Domain.Domains.Article;
using Domain.Interfaces;
using Infra.Data.MongoIdentityStore;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Api.QueryHandlers
{
  public class ListEvaluatorsToApproveQueryHandler : IRequestHandler<ListEvaluatorsToApproveQuery, List<AvaliadorViewModel>>
  {
    private readonly IRepository _repository;

    public ListEvaluatorsToApproveQueryHandler(IRepository repository)
    {
      _repository = repository;
    }
    public async Task<List<AvaliadorViewModel>> Handle(ListEvaluatorsToApproveQuery request, CancellationToken cancellationToken)
    {
      var filters = new List<FilterDefinition<Person>>() {
        Builders<Person>.Filter.Eq(x => x.Type, EUserType.Evaluator),
        Builders<Person>.Filter.Eq(x => x.Approved, false)
      };

      var items = await _repository.GetByFilter(Builders<Person>.Filter.And(filters));

      return items.Select(x => new AvaliadorViewModel()
      {
        Id                 = x.Id,
        Name               = x.Name,
        AttendedModalities = x.AttendedModalities.ToArray(),
        Email              = x.Email,
        Institution        = x.Institution,
        IsSei              = x.IsSei,
        IsSicite           = x.IsSicite
      }).ToList();
    }
  }
}
