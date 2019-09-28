using Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Services.Seisicite.Api.Controllers
{
  public abstract class BaseApiController : ControllerBase
  {
    protected readonly IMediator _mediator;
    protected readonly NotificationContext _notificationContext;

    protected BaseApiController(IMediator mediator, NotificationContext notificationContext)
    {
      _mediator = mediator;
      _notificationContext = notificationContext;
    }

    protected async Task<IActionResult> ResponseCreatedAsync(string action, object routeValues, object value = null)
    {
      return await Task.FromResult(CreatedAtAction(action, routeValues, value));
    }

    protected async Task<IActionResult> ResponseOkAsync(object resultado = null)
    {
      return await Task.FromResult(Ok(resultado));
    }

    protected async Task<IActionResult> ResponseNotFoundAsync(string codigo = null, string mensagem = null)
    {
      if (!string.IsNullOrEmpty(mensagem))
      {
        _notificationContext.NotFound(codigo, mensagem);
      }
      return await Task.FromResult(NotFound());
    }

    protected async Task<IActionResult> ResponseBadRequestAsync(string codigo, string message)
    {
      if (!string.IsNullOrEmpty(message))
      {
        _notificationContext.BadRequest(codigo, message);
      }
      return await Task.FromResult(BadRequest());
    }

    protected async Task<IActionResult> ResponseBadRequestAsync(string message = null)
    {
      if (message != null)
      {
        _notificationContext.BadRequest(string.Empty, message);
      }
      return await Task.FromResult(BadRequest());
    }

    protected async Task<IActionResult> ResponseNoContentAsync()
    {
      return await Task.FromResult(NoContent());
    }

    protected async Task<IActionResult> ResponseNotificationsAsync()
    {
      return await Task.FromResult(NoContent());
    }
  }
}
