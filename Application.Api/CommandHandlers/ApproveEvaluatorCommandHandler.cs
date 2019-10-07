using Application.Api.Services;
using Domain.Core.Enumerators;
using Domain.Core.Notifications;
using Domain.Domains.Article;
using Domain.Interfaces;
using MediatR;
using Services.Seisicite.Api.Commands;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Seisicite.Api.CommandHandlers
{
  public class ApproveEvaluatorCommandHandler : IRequestHandler<ApproveEvaluatorCommand, bool>
  {
    private readonly IRepository _repository;
    private readonly EmailSender _emailSender;
    private readonly NotificationContext _notificationContext;

    public ApproveEvaluatorCommandHandler(
      IRepository repository,
      EmailSender emailSender,
      NotificationContext notificationContext)
    {
      _repository = repository;
      _emailSender = emailSender;
      _notificationContext = notificationContext;
    }

    public async Task<bool> Handle(ApproveEvaluatorCommand request, CancellationToken cancellationToken)
    {
      var emails = new List<string>();

      foreach (var id in request.UserIds)
      {
        var user = await _repository.GetByIdAsync<Person>(id);

        if (user == null)
        {
          _notificationContext.PushNotification(ReturnCode.RegistroNaoEncontrado);
          continue;
        }

        user.Approved = true;
        emails.Add(user.Email);

        await _repository.UpdateAsync(user, user.Id);
      }

      try
      {
        await _emailSender.SendEmailAsync("Aprovação", "<h3>Parabéns, seu usuário foi confirmado</h3><br /> <p>Acesse o SeiSicite Avaliador para conferir os artigos a serem avaliados", emails.ToArray());
      }
      catch (System.Exception e)
      {
        Debug.WriteLine(e);
      }
      
      return true;
    }

  }
}
