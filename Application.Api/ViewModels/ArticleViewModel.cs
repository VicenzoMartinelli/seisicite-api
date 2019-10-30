using Domains.Article;
using System;

namespace Application.Api.ViewModels
{
  public class ArticleViewModel
  {
    public string Id { get; set; }
    public string SubmissionId { get; set; }
    public string Modality { get; set; }
    public string Title { get; set; }
    public string Resume { get; set; }
    public Author PrimaryAuthor { get; set; }
    public Author SecundaryAuthor { get; set; }
    public DateTime StartDate { get; set; }
    public EApresentationType Type { get; set; }
    public string Building { get; set; }
    public string Room { get; set; }
    public string LocalDetails { get; set; }
    public string EvaluatorId { get; set; }
    public string Evaluator2Id { get; set; }
    public double NotaPostura { get; set; } = 0;
    public double NotaOrganizacaoClareza { get; set; } = 0;
    public double NotaMotivacao { get; set; } = 0;
    public double NotaAdequacaoTempoConteudo { get; set; } = 0;
    public double NotaIntroducaoTrabalho { get; set; } = 0;
    public double NotaObjetivosTrabalho { get; set; } = 0;
    public double NotaMateriaisMetodo { get; set; } = 0;
    public double NotaSequenciaLogica { get; set; } = 0;
    public double NotaConhecimentoAssunto { get; set; } = 0;
    public double FinalAverage { get; set; }
  }
}
