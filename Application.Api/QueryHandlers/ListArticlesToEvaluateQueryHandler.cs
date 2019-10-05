using Application.Api.Queries;
using Application.Api.ViewModels;
using Domain.Interfaces;
using Domains.Article;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Api.QueryHandlers
{
  public class ListArticlesToEvaluateQueryHandler : IRequestHandler<ListArticlesToEvaluateQuery, List<ArticleViewModel>>
  {
    private readonly IRepository _repository;

    public ListArticlesToEvaluateQueryHandler(IRepository repository)
    {
      _repository = repository;
    }
    public Task<List<ArticleViewModel>> Handle(ListArticlesToEvaluateQuery request, CancellationToken cancellationToken)
    {
      var articles = _repository.Query<Article>()
        .Where(x => x.EvaluatorId == request.EvaluatorId || x.Evaluator2Id == request.EvaluatorId)
        .Where(x => x.Event == request.EEvent)
        .OrderBy(x => x.StartDate)
        .ToList();

      var artReturn = new List<ArticleViewModel>();

      foreach (var art in articles)
      {
        var av1 = false;
        var turnoAtual = GetTurno(DateTime.Now);
        var turnoArtigo = GetTurno(art.StartDate);
        var status = QueryToEvaluateType.ToEvaluate;

        if (DateTime.Now.Date == art.StartDate.Date && turnoAtual != -1 && turnoAtual == turnoArtigo)
        {
          if (art.EvaluatorId == request.EvaluatorId)
          {
            status = art.NotaConhecimentoAssunto == 0 ? QueryToEvaluateType.ToEvaluate : QueryToEvaluateType.Opened;
            av1 = true;
          }
          else
          {
            status = art.NotaConhecimentoAssunto2 == 0 ? QueryToEvaluateType.ToEvaluate : QueryToEvaluateType.Opened;
          }
        }
        else
        {
          if (art.EvaluatorId == request.EvaluatorId)
          {
            av1 = true;
            status = art.NotaConhecimentoAssunto == 0 ? QueryToEvaluateType.ToEvaluate : QueryToEvaluateType.Closed;
          }
          else
          {
            status = art.NotaConhecimentoAssunto2 == 0 ? QueryToEvaluateType.ToEvaluate : QueryToEvaluateType.Closed;
          }
        }

        if (request.Type == status)
        {
          artReturn.Add(new ArticleViewModel
          {
            Id                         = art.Id,
            SubmissionId               = art.SubmissionId,
            Title                      = art.Title,
            Resume                     = art.Resume,
            Building                   = art.Building,
            Modality                   = art.Modality,
            Room                       = art.Room,
            PrimaryAuthor              = art.PrimaryAuthor,
            StartDate                  = art.StartDate.ToLocalTime(),
            Type                       = art.ApresentationType,
            Evaluator2Id               = art.Evaluator2Id,
            EvaluatorId                = art.EvaluatorId,
            LocalDetails               = art.LocalDetails,
            FinalAverage               = av1 ? art.FinalAverage : art.FinalAverage2,
            NotaAdequacaoTempoConteudo = av1 ? art.NotaAdequacaoTempoConteudo : art.NotaAdequacaoTempoConteudo2,
            NotaConhecimentoAssunto    = av1 ? art.NotaConhecimentoAssunto : art.NotaConhecimentoAssunto2,
            NotaIntroducaoTrabalho     = av1 ? art.NotaIntroducaoTrabalho : art.NotaIntroducaoTrabalho2,
            NotaMateriaisMetodo        = av1 ? art.NotaMateriaisMetodo : art.NotaMateriaisMetodo2,
            NotaMotivacao              = av1 ? art.NotaMotivacao : art.NotaMotivacao,
            NotaObjetivosTrabalho      = av1 ? art.NotaObjetivosTrabalho : art.NotaObjetivosTrabalho2,
            NotaOrganizacaoClareza     = av1 ? art.NotaOrganizacaoClareza : art.NotaOrganizacaoClareza2,
            NotaPostura                = av1 ? art.NotaPostura : art.NotaPostura2,
            NotaSequenciaLogica        = av1 ? art.NotaSequenciaLogica : art.NotaSequenciaLogica2
          });
        }
      }

      return Task.FromResult(artReturn);
    }

    private int GetTurno(DateTime time)
    {
      if (time.Hour > 8)
      {
        if (time.Hour < 13)
        {
          return 0;
        }
        else if (time.Hour < 18)
        {
          return 1;
        }
      }

      return -1;
    }
  }
}
