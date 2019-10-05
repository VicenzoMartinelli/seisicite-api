using Domain.Core.Notifications;
using Infra.Data.MongoIdentityStore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Seisicite.Api.Attributes;
using Services.Seisicite.Api.Commands;
using System.Threading.Tasks;

namespace Services.Seisicite.Api.Controllers
{
  [Route("auth")]
  public class AuthController : BaseApiController
  {
    public AuthController(IMediator mediator, NotificationContext notificationContext) : base(mediator, notificationContext)
    {
    }

    [HttpGet]
    public Task<IActionResult> Get()  
      => Task.FromResult((IActionResult) Ok("nidasduiasdhiasudhas"));

    [HttpPost("register-comission-user")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserCommand registerUser)
    {
      var result = await _mediator.Send(registerUser);

      if (result == null)
      {
        return await ResponseNotificationsAsync();
      }

      return await ResponseOkAsync(result);
    }

    [HttpPost("register-evaluator")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserEvaluatorCommand registerUser)
    {
      var result = await _mediator.Send(registerUser);

      if (result == null)
      {
        return await ResponseNotificationsAsync();
      }

      return await ResponseOkAsync(result);
    }

    /// <summary>
    /// Método responsável pela autenticação do usuário e retorno do token.
    /// </summary>
    /// <param name="login">Dados do login do usuário</param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> LogInAsync([FromBody] LoginCommand login)
    {
      var result = await _mediator.Send(login);

      if (result == null)
      {
        return await ResponseNotificationsAsync();
      }

      return await ResponseOkAsync(result);
    }
  }
}
