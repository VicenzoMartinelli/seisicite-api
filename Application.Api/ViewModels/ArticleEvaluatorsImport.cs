using System;

namespace Application.Api.ViewModels
{
  public class ArticleEvaluatorsImport
  {
    public int SubmissionId { get; set; }
    public string Sala { get; set; }
    public string Hora { get; set; }
    public string Data { get; set; }
    public string Local { get; set; }
    public string Avaliador1 { get; set; }
    public string Email1 { get; set; }
    public string Avaliador2 { get; set; }
    public string Email2 { get; set; }

    public override bool Equals(object obj)
    {
      return obj is ArticleEvaluatorsImport valueIMport &&
             SubmissionId == valueIMport.SubmissionId &&
             Sala == valueIMport.Sala &&
             Hora == valueIMport.Hora &&
             Data == valueIMport.Data &&
             Local == valueIMport.Local &&
             Avaliador1 == valueIMport.Avaliador1 &&
             Email1 == valueIMport.Email1 &&
             Avaliador2 == valueIMport.Avaliador2 &&
             Email2 == valueIMport.Email2;
    }

    public override int GetHashCode()
    {
      var hash = new HashCode();
      hash.Add(SubmissionId);
      hash.Add(Sala);
      hash.Add(Hora);
      hash.Add(Data);
      hash.Add(Local);
      hash.Add(Avaliador1);
      hash.Add(Email1);
      hash.Add(Avaliador2);
      hash.Add(Email2);
      return hash.ToHashCode();
    }
  }
}
