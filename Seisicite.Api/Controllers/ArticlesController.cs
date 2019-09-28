using Application.Api.Commands;
using Application.Api.Queries;
using Application.Api.ViewModels;
using Domain.Core.Notifications;
using Domains.Article;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Seisicite.Api.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Seisicite.Api.Controllers
{
  [Route("articles")]
  [ApiController]
  [Authorize]
  public class ArticlesController : BaseApiController
  {
    public ArticlesController(IMediator mediator, NotificationContext notificationContext) : base(mediator, notificationContext)
    { }

    [HttpGet("{evento}")]
    public async Task<IActionResult> GetArticlesBySituacao(EEventIdentifier evento, [FromQuery] int page)
    {
      var value = await _mediator.Send(new ListArticlesPaginatedByEventQuery()
      {
        Event = evento,
        Page = page
      });

      return await ResponseOkAsync(value);
    }

    [HttpGet("modalities")]
    public async Task<IActionResult> GetModalities()
    {
      var value = await _mediator.Send(new ListModalitiesQuery());

      return await ResponseOkAsync(value);
    }
    [HttpGet("instituions")]
    public async Task<IActionResult> GetInstitutions()
    {
      var value = await _mediator.Send(new ListInstitutionsQuery());

      return await ResponseOkAsync(value);
    }

    [HttpGet("avaliadores/{evento}")]
    public async Task<IActionResult> GetAvaliadores(EEventIdentifier evento)
    {
      var value = await _mediator.Send(new ListAvaliadoresByEventoQuery()
      {
        Event = evento
      });

      return await ResponseOkAsync(value);
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateArticle([FromBody] UpdateArticleCommand command)
    {
      var value = await _mediator.Send(command);

      if (!value.IsSuccess)
        return await ResponseNotificationsAsync();

      return await ResponseOkAsync(value);
    }

    [HttpPut("sort/{eventId}")]
    public async Task<IActionResult> SortArticles(EEventIdentifier eventId)
    {
      var value = await _mediator.Send(new SortArticleEvaluatorsCommand(eventId));

      if (!value)
        return await ResponseNotificationsAsync();

      var newList = await _mediator.Send(new ListArticlesPaginatedByEventQuery {
        Event = eventId
      });

      return await ResponseOkAsync(newList);
    }

    [HttpPut("evaluate/{id}")]
    public async Task<IActionResult> SortArticles([FromBody] EvaluateArticleCommand command, [FromRoute] string id)
    {
      var value = await _mediator.Send(command.SetId(id));

      if (!value)
        return await ResponseNotificationsAsync();

      return await ResponseOkAsync();
    }

    [HttpGet("articles-evaluate/{eevent}/{evaluatorId}/{type}")]
    public async Task<IActionResult> ArticlesEvaluate(
      [FromRoute] string evaluatorId,
      [FromRoute] QueryToEvaluateType type,
      [FromRoute] EEventIdentifier eevent)
    {
      var value = await _mediator.Send(new ListArticlesToEvaluateQuery() {
        EvaluatorId = evaluatorId,
        Type = type,
        EEvent = eevent
      });

      return await ResponseOkAsync(value);
    }

    [HttpGet("articles-report/{eevent}/{modality}")]
    public async Task<IActionResult> ReportArticlesNote(
      [FromRoute] EEventIdentifier eevent,
      [FromRoute] string modality)
    {
      var value = await _mediator.Send(new ReportArticlesNoteQuery() {
        Modality = modality,
        Event = eevent
      });

      return await ResponseOkAsync(value);
    }
  }
}
