using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Api.Commands;
using Domain.Interfaces;
using Domains.Article;
using MediatR;

namespace Application.Api.CommandHandlers
{
  public class ImportArticlesCommandHandler : IRequestHandler<ImportArticlesCommand, bool>
  {
    private IRepository _repository;

    public ImportArticlesCommandHandler(IRepository repository)
    {
      _repository = repository;
    }
    public async Task<bool> Handle(ImportArticlesCommand request, CancellationToken cancellationToken)
    {
      var articles = new List<Article>();

      foreach (var item in request.Articles)
      {
        articles.Add(new Article
        {
          SubmissionId = item.IdDaSubmissão.ToString(),
          ApresentationType = EApresentationType.Poster,
          AssessmentStatus = EAssessmentStatus.Opened,
          Event = request.Event,
          Building = item.Prédio,
          StartDate = string.IsNullOrEmpty(item.DataDeInício) ? new DateTime(2019, 11, 11) : Convert.ToDateTime(item.DataDeInício),
          DirectorDecision = item.DecisãoDoDiretor,
          Language = item.Idioma,
          Modality = item.TítuloDaModalidade,
          Resume = item.Resumo,
          Room = item.Sala,
          Situation = item.Situação,
          Title = item.Título,
          PrimaryAuthor = new Author()
          {
            BibliographySummary = item.ResumoDaBiografiaAutor1,
            Country = item.PaísAutor1,
            FirstName = item.PrenomeAutor1,
            MiddleName = item.NomeDoMeioAutor1,
            LastName = item.SobrenomeAutor1,
            Email = item.EMailAutor1,
            Institution = CleanInstitution(item.InstituiçãoAutor1),
            PageUrl = item.UrlAutor1
          },
          SecundaryAuthor = string.IsNullOrEmpty(item.SobrenomeAutor2) ? null : new Author
          {
            BibliographySummary = item.ResumoDaBiografiaAutor2,
            Country = item.PaísAutor2,
            FirstName = item.PrenomeAutor2,
            MiddleName = item.NomeDoMeioAutor2,
            LastName = item.SobrenomeAutor2,
            Email = item.EMailAutor2,
            Institution = CleanInstitution(item.InstituiçãoAutor2),
            PageUrl = item.UrlAutor2
          },
          TertiaryAuthor = string.IsNullOrEmpty(item.SobrenomeAutor3) ? null : new Author
          {
            BibliographySummary = item.ResumoDaBiografiaAutor3,
            Country = item.PaísAutor3,
            FirstName = item.PrenomeAutor3,
            MiddleName = item.NomeDoMeioAutor3,
            LastName = item.SobrenomeAutor3,
            Email = item.EMailAutor3,
            Institution = CleanInstitution(item.InstituiçãoAutor3),
            PageUrl = item.UrlAutor3
          }
        });
      }

      if (articles.Any())
      {
        await _repository.AddManyAsync(articles);
      }

      return true;
    }

    private static string CleanInstitution(string v)
    {
      return v
        .Replace("Universidade Tecnológica Federal do Paraná - campus Dois Vizinhos", "Universidade Tecnológica Federal do Paraná, Dois Vizinhos, Paraná, Brasil")
        .Replace("Universidade Tecnológica Federal do Paraná, campus Dois Vizinhos", "Universidade Tecnológica Federal do Paraná, Dois Vizinhos, Paraná, Brasil")
        .Replace("<p class=\"autor\">", "").Replace("</p>", "").Replace("<br />", "").Replace(".", "").Replace("<html />", "").Trim();
    }
  }
}
