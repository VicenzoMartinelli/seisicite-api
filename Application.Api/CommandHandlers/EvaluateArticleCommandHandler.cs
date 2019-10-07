using Application.Api.Commands;
using Domain.Core.Enumerators;
using Domain.Core.Notifications;
using Domain.Domains.Article;
using Domain.Interfaces;
using Domains.Article;
using MediatR;
using Services.Seisicite.Api.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Seisicite.Api.CommandHandlers
{
  public class EvaluateArticleCommandHandler : IRequestHandler<EvaluateArticleCommand, bool>
  {
    private readonly IRepository _repository;
    private readonly NotificationContext _notificationContext;

    public EvaluateArticleCommandHandler(
      IRepository repository,
      NotificationContext notificationContext)
    {
      _repository = repository;
      _notificationContext = notificationContext;
    }

    public async Task<bool> Handle(EvaluateArticleCommand request, CancellationToken cancellationToken)
    {
      var article = await _repository.GetByIdAsync<Article>(request.Id);

      if (article == null)
      {
        return false;
      }

      var turnoAtual = GetTurno(DateTime.Now);
      var turnoArtigo = GetTurno(article.StartDate);

      if (DateTime.Now.Date != article.StartDate.Date || turnoAtual == -1 || turnoAtual != turnoArtigo)
      {
        _notificationContext.PushNotification(ReturnCode.NaoEhPossivelEditarAvaliacaoArtigo);
        return false;
      }

      if (article.EvaluatorId == request.EvaluatorId)
      {

        article.NotaPostura = request.NotaPostura;
        article.NotaOrganizacaoClareza = request.NotaOrganizacaoClareza;
        article.NotaMotivacao = request.NotaMotivacao;
        article.NotaAdequacaoTempoConteudo = request.NotaAdequacaoTempoConteudo;
        article.NotaIntroducaoTrabalho = request.NotaIntroducaoTrabalho;
        article.NotaObjetivosTrabalho = request.NotaObjetivosTrabalho;
        article.NotaMateriaisMetodo = request.NotaMateriaisMetodo;
        article.NotaSequenciaLogica = request.NotaSequenciaLogica;
        article.NotaConhecimentoAssunto = request.NotaConhecimentoAssunto;


        await _repository.SaveOrUpdateAsync(article.CalculateFinalAverage(), article.Id);

        return true;
      }
      else if (article.Evaluator2Id == request.EvaluatorId)
      {

        article.NotaPostura2 = request.NotaPostura;
        article.NotaOrganizacaoClareza2 = request.NotaOrganizacaoClareza;
        article.NotaMotivacao2 = request.NotaMotivacao;
        article.NotaAdequacaoTempoConteudo2 = request.NotaAdequacaoTempoConteudo;
        article.NotaIntroducaoTrabalho2 = request.NotaIntroducaoTrabalho;
        article.NotaObjetivosTrabalho2 = request.NotaObjetivosTrabalho;
        article.NotaMateriaisMetodo2 = request.NotaMateriaisMetodo;
        article.NotaSequenciaLogica2 = request.NotaSequenciaLogica;
        article.NotaConhecimentoAssunto2 = request.NotaConhecimentoAssunto;

        await _repository.SaveOrUpdateAsync(article.CalculateFinalAverage2(), article.Id);

        return true;
      }

      return false;
    }

    private int GetTurno(DateTime time)
    {
      if (time.Hour > 8)
      {
        if (time.Hour < 13)
        {
          return 0;
        }
        else if (time.Hour < 19)
        {
          return 1;
        }
      }

      return -1;
    }
  }
}
