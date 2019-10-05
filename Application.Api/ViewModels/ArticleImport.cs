using Newtonsoft.Json;

namespace Application.Api.ViewModels
{
  public class ArticleImport
  {
    [JsonProperty("ID da submissão")]
    public long IdDaSubmissão { get; set; }

    [JsonProperty("Título")]
    public string Título { get; set; }

    [JsonProperty("Resumo")]
    public string Resumo { get; set; }

    [JsonProperty("Prenome (Autor 1)")]
    public string PrenomeAutor1 { get; set; }

    [JsonProperty("Nome do meio (Autor 1)")]
    public string NomeDoMeioAutor1 { get; set; }

    [JsonProperty("Sobrenome (Autor 1)")]
    public string SobrenomeAutor1 { get; set; }

    [JsonProperty("País (Autor 1)")]
    public string PaísAutor1 { get; set; }

    [JsonProperty("Instituição (Autor 1)")]
    public string InstituiçãoAutor1 { get; set; }

    [JsonProperty("E-mail (Autor 1)")]
    public string EMailAutor1 { get; set; }

    [JsonProperty("URL (Autor 1)")]
    public string UrlAutor1 { get; set; }

    [JsonProperty("Resumo da Biografia (Autor 1)")]
    public string ResumoDaBiografiaAutor1 { get; set; }

    [JsonProperty("Prenome (Autor 2)")]
    public string PrenomeAutor2 { get; set; }

    [JsonProperty("Nome do meio (Autor 2)")]
    public string NomeDoMeioAutor2 { get; set; }

    [JsonProperty("Sobrenome (Autor 2)")]
    public string SobrenomeAutor2 { get; set; }

    [JsonProperty("País (Autor 2)")]
    public string PaísAutor2 { get; set; }

    [JsonProperty("Instituição (Autor 2)")]
    public string InstituiçãoAutor2 { get; set; }

    [JsonProperty("E-mail (Autor 2)")]
    public string EMailAutor2 { get; set; }

    [JsonProperty("URL (Autor 2)")]
    public string UrlAutor2 { get; set; }

    [JsonProperty("Resumo da Biografia (Autor 2)")]
    public string ResumoDaBiografiaAutor2 { get; set; }
    [JsonProperty("Prenome (Autor 3)")]
    public string PrenomeAutor3 { get; set; }

    [JsonProperty("Nome do meio (Autor 3)")]
    public string NomeDoMeioAutor3 { get; set; }

    [JsonProperty("Sobrenome (Autor 3)")]
    public string SobrenomeAutor3 { get; set; }

    [JsonProperty("País (Autor 3)")]
    public string PaísAutor3 { get; set; }

    [JsonProperty("Instituição (Autor 3)")]
    public string InstituiçãoAutor3 { get; set; }

    [JsonProperty("E-mail (Autor 3)")]
    public string EMailAutor3 { get; set; }

    [JsonProperty("URL (Autor 3)")]
    public string UrlAutor3 { get; set; }

    [JsonProperty("Resumo da Biografia (Autor 3)")]
    public string ResumoDaBiografiaAutor3 { get; set; }

    [JsonProperty("Título da modalidade")]
    public string TítuloDaModalidade { get; set; }

    [JsonProperty("Idioma")]
    public string Idioma { get; set; }

    [JsonProperty("Decisão do diretor")]
    public string DecisãoDoDiretor { get; set; }

    [JsonProperty("Data de início")]
    public string DataDeInício { get; set; }

    [JsonProperty("Data de término")]
    public string DataDeTérmino { get; set; }

    [JsonProperty("Prédio")]
    public string Prédio { get; set; }

    [JsonProperty("Sala")]
    public string Sala { get; set; }

    [JsonProperty("Situação")]
    public string Situação { get; set; }

    [JsonProperty("Tipo de sessão")]
    public string TipoDeSessão { get; set; }

    [JsonProperty("Comentários do autor")]
    public string ComentáriosDoAutor { get; set; }
  }
}