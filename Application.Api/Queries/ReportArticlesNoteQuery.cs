using Application.Api.ViewModels;
using Domains.Article;
using MediatR;
using System.Collections.Generic;

namespace Application.Api.Queries
{
  public class ReportArticlesNoteQuery : IRequest<List<ArticleFinalReportViewModel>>
  {
    public EEventIdentifier Event { get; set; }
    public EApresentationType? Type { get; set; }
    public string Modality { get; set; }
  }
}
