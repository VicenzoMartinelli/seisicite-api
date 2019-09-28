using Domain.Core.Enumerators;
using Domain.Core.Models;
using Domain.Core.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Seisicite.Api.Filters
{
  public class NotificationActionFilter : IActionFilter
  {
    private readonly NotificationContext _notificationContext;

    public NotificationActionFilter(NotificationContext context)
    {
      _notificationContext = context;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
      if (!context.ModelState.IsValid || _notificationContext.AnyNotification)
      {
        ExtractModelStateErrors(context.ModelState);

        context.Result = InterceptResponse();
      }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
      if (!context.ModelState.IsValid || _notificationContext.AnyNotification|| context.Exception != null)
      {
        ExtractModelStateErrors(context.ModelState);

        if (context.Exception != null)
          _notificationContext.PushNotification(ReturnCode.ImpossivelFinalizarAcao);

        context.Result = InterceptResponse();
      }
    }

    private void ExtractModelStateErrors(ModelStateDictionary modelState)
    {
      _notificationContext.UnionNotifications(
          modelState.SelectMany(x => x.Value.Errors.Select(e => new Notification(
            x.Key,
            ReturnCode.PreenchimentoInvalido.GetNumberStringEnumValue(),
            string.Empty,
            e.ErrorMessage,
            400
          )))
      );
    }

    private IActionResult InterceptResponse()
    {
      var @return = new OccurrencesViewModel(_notificationContext.GetNotifications());

      if (ExisteNotFound(@return))
      {
        return new NotFoundObjectResult(@return)
        {
          ContentTypes = { "application/problem+json" },
          DeclaredType = typeof(OccurrencesViewModel)
        };
      }
      else
      {
        return new BadRequestObjectResult(@return)
        {
          ContentTypes = { "application/problem+json" },
          DeclaredType = typeof(OccurrencesViewModel)
        };
      }
    }

    private bool ExisteNotFound(OccurrencesViewModel response)
    {
      return response.Occurrences.Any(e => e.StatusCode == StatusCodes.Status404NotFound);
    }
  }
}
