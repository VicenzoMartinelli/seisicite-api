using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Domains.Article
{
  public class Article
  {
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string EvaluatorId { get; set; }
    public string Evaluator2Id { get; set; }
    public EEventIdentifier Event { get; set; }
    public EAssessmentStatus AssessmentStatus { get; set; }
    public EApresentationType ApresentationType { get; set; }
    public string SubmissionId { get; set; }
    public string Title { get; set; }
    public string Resume { get; set; }
    public Author PrimaryAuthor { get; set; }
    public Author SecundaryAuthor { get; set; }
    public Author TertiaryAuthor { get; set; }
    public string Modality { get; set; }
    public string Language { get; set; }
    public DateTime StartDate { get; set; }
    public string Building { get; set; }
    public string Room { get; set; }
    public string LocalDetails { get; set; }
    public string DirectorDecision { get; set; }
    public string Situation { get; set; }
    public double CommissionNote { get; set; } = 0;

    // AVALIAÇÃO 1
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

    // AVALIAÇÃO 2
    public double NotaPostura2 { get; set; } = 0;
    public double NotaOrganizacaoClareza2 { get; set; } = 0;
    public double NotaMotivacao2 { get; set; } = 0;
    public double NotaAdequacaoTempoConteudo2 { get; set; } = 0;
    public double NotaIntroducaoTrabalho2 { get; set; } = 0;
    public double NotaObjetivosTrabalho2 { get; set; } = 0;
    public double NotaMateriaisMetodo2 { get; set; } = 0;
    public double NotaSequenciaLogica2 { get; set; } = 0;
    public double NotaConhecimentoAssunto2 { get; set; } = 0;
    public double FinalAverage2 { get; set; }
    public Article CalculateFinalAverage()
    {
      FinalAverage = Math.Round((
        (NotaPostura * 0.1) +
        (NotaOrganizacaoClareza * 0.1) +
        (NotaMotivacao * 0.1) +
        (NotaAdequacaoTempoConteudo * 0.1) +
        (NotaIntroducaoTrabalho * 0.1) +
        (NotaMateriaisMetodo * 0.1) +
        (NotaObjetivosTrabalho * 0.1) +
        (NotaSequenciaLogica * 0.1) +
        (NotaConhecimentoAssunto * 0.2)) / 10 * 100, 2);

      return this;
    }
    public Article CalculateFinalAverage2()
    {
      FinalAverage2 = Math.Round((
        (NotaPostura2 * 0.1) +
        (NotaOrganizacaoClareza2 * 0.1) +
        (NotaMotivacao2 * 0.1) +
        (NotaAdequacaoTempoConteudo2 * 0.1) +
        (NotaIntroducaoTrabalho2 * 0.1) +
        (NotaMateriaisMetodo2 * 0.1) +
        (NotaObjetivosTrabalho2 * 0.1) +
        (NotaSequenciaLogica2 * 0.1) +
        (NotaConhecimentoAssunto2 * 0.2)) / 10 * 100, 2);

      return this;
    }


    internal TimeSpan Duration = TimeSpan.FromMinutes(15);
  }
}
