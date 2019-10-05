using MediatR;

namespace Application.Api.Commands
{
  public class EvaluateArticleCommand : IRequest<bool>
  {
    public string Id { get; private set; }
    public string EvaluatorId { get; set; }
    public double NotaPostura { get; set; }
    public double NotaOrganizacaoClareza { get; set; }
    public double NotaMotivacao { get; set; }
    public double NotaAdequacaoTempoConteudo { get; set; }
    public double NotaIntroducaoTrabalho { get; set; }
    public double NotaObjetivosTrabalho { get; set; }
    public double NotaMateriaisMetodo { get; set; }
    public double NotaSequenciaLogica { get; set; }
    public double NotaConhecimentoAssunto { get; set; }

    public EvaluateArticleCommand SetId(string id)
    {
      Id = id;

      return this;
    }
  }
}
