﻿using Application.Api.Commands;
using Application.Api.ViewModels;
using Domain.Core.Notifications;
using Domain.Interfaces;
using Domains.Article;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Seisicite.Api.Controllers
{
  [Route("import-articles")]
  [ApiController]
  [Authorize]
  public class ImportArticlesController : BaseApiController
  {
    public ImportArticlesController(IMediator mediator, NotificationContext notificationContext) : base(mediator, notificationContext)
    { }

    [HttpPost("{evvent}")]
    public async Task<IActionResult> ImportArticles(
        IFormFile file,
        EEventIdentifier evvent)
    {
      List<ArticleImport> items;

      using (StreamReader r = new StreamReader(file.OpenReadStream()))
      {
        string json = r.ReadToEnd();
        items = JsonConvert.DeserializeObject<List<ArticleImport>>(json);
      }

      var res = await _mediator.Send(new ImportArticlesCommand()
      {
        Articles = items,
        Event = evvent
      });

      return res ? await ResponseOkAsync() : await ResponseNotificationsAsync();
    }

    [HttpPost("notes/{evvent}")]
    public async Task<IActionResult> ImportNotes(
            IFormFile file,
            EEventIdentifier evvent)
    {
      List<ArticleComissionNote> items;

      using (StreamReader r = new StreamReader(file.OpenReadStream()))
      {
        string json = r.ReadToEnd();
        items = JsonConvert.DeserializeObject<List<ArticleComissionNote>>(json);
      }

      var res = await _mediator.Send(new ImportArticlesComissionNoteCommand()
      {
        Notes = items,
        Event = evvent
      });

      return res ? await ResponseOkAsync() : await ResponseNotificationsAsync();
    }

    [HttpPost("evaluators/{evvent}")]
    public async Task<IActionResult> ImportEvaluators(
            IFormFile file,
            EEventIdentifier evvent)
    {
      List<ArticleEvaluatorsImport> items;

      using (StreamReader r = new StreamReader(file.OpenReadStream()))
      {
        string json = r.ReadToEnd();
        items = JsonConvert.DeserializeObject<List<ArticleEvaluatorsImport>>(json);
      }

      var res = await _mediator.Send(new ImportArticlesEvaluatorsCommand()
      {
        Articles = items,
        Event = evvent
      });

      return res ? await ResponseOkAsync() : await ResponseNotificationsAsync();
    }

    [HttpGet("MEUDEUS")]
    [AllowAnonymous]
    public async Task<IActionResult> DADA([FromServices] IRepository repository)
    {
      repository.Query<Article>().Where(x => x.NotaConhecimentoAssunto != 0).ToList().ForEach(x =>
      {
        if (x.FinalAverage > 0 || x.FinalAverage2 > 0)
        {
          if (x.FinalAverage > 0)
          {
            x.CalculateFinalAverage();
          }

          if (x.FinalAverage2 > 0)
          {
            x.CalculateFinalAverage2();
          }

          repository.SaveOrUpdateAsync(x, x.Id);
        }

      });

      return Ok();
    }
  }
}
