using Newtonsoft.Json;
using System;

namespace Application.Api.ViewModels
{
  public class ArticleComissionNote
  {
    [JsonProperty("ID da submissão")]
    public long IdDaSubmissão { get; set; }
    [JsonProperty("Média")]
    public string Media { get; set; }

    public override bool Equals(object obj)
    {
      return obj is ArticleComissionNote note &&
             IdDaSubmissão == note.IdDaSubmissão &&
             Media == note.Media;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(IdDaSubmissão, Media);
    }
  }
}