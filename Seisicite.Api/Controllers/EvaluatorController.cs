using Application.Api.Queries;
using Domain.Core.Notifications;
using Infra.Data.MongoIdentityStore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seisicite.Api.Attributes;
using Services.Seisicite.Api.Commands;
using Services.Seisicite.Api.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seisicite.Api.Controllers
{
  [Route("evaluator")]
  [ApiController]
  [Authorize]
  public class EvaluatorController : BaseApiController
  {
    public EvaluatorController(IMediator mediator, NotificationContext notificationContext) : base(mediator, notificationContext)
    { }

    [HttpGet("approveds")]
    public async Task<IActionResult> GetApprovedsEvaluators([FromQuery] string modality)
    {
      var result = await _mediator.Send(new ListApprovedsEvaluatorsQuery()
      {
        Modality = modality
      });

      return await ResponseOkAsync(result);
    }

    [HttpGet("to-approve")]
    public async Task<IActionResult> GetApprovedsEvaluators()
    {
      var result = await _mediator.Send(new ListEvaluatorsToApproveQuery());

      return await ResponseOkAsync(result);
    }

    [HttpPut("approve")]
    [ClaimRequirement("Type", nameof(EUserType.Committee))]
    public async Task<IActionResult> ApproveUser([FromBody] IEnumerable<string> ids)
    {
      var result = await _mediator.Send(new ApproveEvaluatorCommand() { UserIds = ids });

      if (!result)
      {
        return await ResponseNotificationsAsync();
      }

      var res = await _mediator.Send(new ListEvaluatorsToApproveQuery());

      return await ResponseOkAsync(res);
    }

    [HttpPut("update-event")]
    [ClaimRequirement("Type", nameof(EUserType.Committee))]
    public async Task<IActionResult> ApproveUser([FromBody] UpdateIsEventCommand command)
    {
      var result = await _mediator.Send(command);

      if (!result)
      {
        return await ResponseNotificationsAsync();
      }

      return await ResponseOkAsync();
    }

    [HttpPut("cancel")]
    [ClaimRequirement("Type", nameof(EUserType.Committee))]
    public async Task<IActionResult> CancelUsers([FromBody] IEnumerable<string> ids)
    {
      var result = await _mediator.Send(new CancelEvaluatorsCommand() { UserIds = ids });

      if (!result)
      {
        return await ResponseNotificationsAsync();
      }

      var ok = await _mediator.Send(new ListEvaluatorsToApproveQuery());

      return await ResponseOkAsync(ok);
    }
  }
}
