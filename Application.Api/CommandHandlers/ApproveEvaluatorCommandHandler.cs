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
  public class ApproveEvaluatorCommandHandler : IRequestHandler<ApproveEvaluatorCommand, bool>
  {
    private readonly IRepository _repository;
    private readonly NotificationContext _notificationContext;

    public ApproveEvaluatorCommandHandler(
      IRepository repository,
      NotificationContext notificationContext)
    {
      _repository = repository;
      _notificationContext = notificationContext;
    }

    public async Task<bool> Handle(ApproveEvaluatorCommand request, CancellationToken cancellationToken)
    {
      foreach (var id in request.UserIds)
      {
        var user = await _repository.GetByIdAsync<Person>(id);

        if (user == null)
        {
          _notificationContext.PushNotification(ReturnCode.RegistroNaoEncontrado);
          continue;
        }

        user.Approved = true;

        await _repository.UpdateAsync(user, user.Id);
      }

      return true;
    }

  }
}
