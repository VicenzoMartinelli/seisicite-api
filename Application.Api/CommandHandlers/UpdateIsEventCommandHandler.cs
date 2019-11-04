using Domain.Core.Enumerators;
using Domain.Core.Notifications;
using Domain.Domains.Article;
using Domain.Interfaces;
using MediatR;
using Services.Seisicite.Api.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Seisicite.Api.CommandHandlers
{
  public class UpdateIsEventCommandHandler : IRequestHandler<UpdateIsEventCommand, bool>
  {
    private readonly IRepository _repository;
    private readonly NotificationContext _notificationContext;

    public UpdateIsEventCommandHandler(
      IRepository repository,
      NotificationContext notificationContext)
    {
      _repository = repository;
      _notificationContext = notificationContext;
    }

    public async Task<bool> Handle(UpdateIsEventCommand request, CancellationToken cancellationToken)
    {
      var user = await _repository.GetByIdAsync<Person>(request.UserId);

      if (user == null)
      {
        _notificationContext.PushNotification(ReturnCode.RegistroNaoEncontrado);
        return false;
      }

      if (request.Event == Domains.Article.EEventIdentifier.Sei)
      {
        user.IsSei = !user.IsSei;
      }
      else
      {
        user.IsSicite = !user.IsSicite;
      }

      await _repository.UpdateAsync(user, user.Id);

      return true;
    }

  }
}
