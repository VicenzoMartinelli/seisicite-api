using Domain.Core.Enumerators;
using Domain.Core.Notifications;
using Domain.Domains.Article;
using Domain.Interfaces;
using Infra.Data.MongoIdentityStore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using Services.Seisicite.Api.Commands;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Seisicite.Api.CommandHandlers
{
  public class CancelEvaluatorsCommandHandler : IRequestHandler<CancelEvaluatorsCommand, bool>
  {
    private readonly IRepository _repository;
    private readonly UserManager<MongoIdentityUser> _manager;
    private readonly NotificationContext _notificationContext;

    public CancelEvaluatorsCommandHandler(IRepository repository, UserManager<MongoIdentityUser> manager, NotificationContext notificationContext)
    {
      _repository = repository;
      _manager = manager;
      _notificationContext = notificationContext;
    }

    public async Task<bool> Handle(CancelEvaluatorsCommand request, CancellationToken cancellationToken)
    {
      foreach (var id in request.UserIds)
      {
        var person = await _repository.GetByIdAsync<Person>(id);

        if(!person.Approved)
        {
          await _repository.DeleteAsync<Person>(person.Id);

          var user = (await _repository.GetByFilter(
              Builders<MongoIdentityUser>.Filter.Eq(x => x.Id, person.IdentityUserId), "IdentityUser"
          )).FirstOrDefault();

          await _manager.DeleteAsync(user);
        }
      }

      return true;
    }

  }
}
