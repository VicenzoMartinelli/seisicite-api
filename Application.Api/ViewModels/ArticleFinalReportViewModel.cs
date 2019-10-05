namespace Application.Api.ViewModels
{
  public class ArticleFinalReportViewModel
  {
    public string SubmissionId { get; set; }
    public string Title { get; set; }
    public double CommissionNote { get; set; }
    public string Evaluator1Name { get; set; }
    public double Evaluator1Average { get; set; }
    public string Evaluator2Name { get; set; }
    public double Evaluator2Average { get; set; }
    public double FinalAverage { get; set; }
  }
}
