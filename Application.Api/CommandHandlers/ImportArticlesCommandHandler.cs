using Application.Api.Commands;
using Domain.Interfaces;
using Domains.Article;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Api.CommandHandlers
{
  public class ImportArticlesCommandHandler : IRequestHandler<ImportArticlesCommand, bool>
  {
    private readonly IRepository _repository;

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
            Institution = string.IsNullOrEmpty(item.InstituiçãoAutor1) ? "UTFPR" : CleanInstitution(item.InstituiçãoAutor1),
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
            Institution = string.IsNullOrEmpty(item.InstituiçãoAutor2) ? "UTFPR" : CleanInstitution(item.InstituiçãoAutor2),
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
            Institution = string.IsNullOrEmpty(item.InstituiçãoAutor3) ? "UTFPR" : CleanInstitution(item.InstituiçãoAutor3),
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
      var r = v
        .Trim()
        .Replace("Universidade Tecnológica Federal do Paraná - campus Dois Vizinhos", "Universidade Tecnológica Federal do Paraná, Dois Vizinhos, Paraná, Brasil")
        .Replace("Universidade Tecnológica Federal do Paraná, campus Dois Vizinhos", "Universidade Tecnológica Federal do Paraná, Dois Vizinhos, Paraná, Brasil")
        .Replace("<p> <p>Universidade Tecnológica Federal do Paraná, Dois Vizinhos, Paraná, Brasil", "Universidade Tecnológica Federal do Paraná, Dois Vizinhos, Paraná, Brasil")
        .Replace("Universidade Tecnológica Federal do Paraná, campus Campo Mourão", "Universidade Tecnológica Federal do Paraná, Campo Mourão, Paraná, Brasil")
        .Replace("<p class=\"autor\">", "")
        .Replace("<div id=\"MathJax_Message\" style=\"display: none;\"></div>", "")
        .Replace("Centro de Pesquisas em Reologia e Fluidos Não Newtonianos,", "")
        .Replace("</p>", "")
        .Replace("<br />", "")
        .Replace(".", "")
        .Replace("<html />", "")
        .Replace("<p>", "")
        .Replace("</p>", "")
        .Replace("<span>", "")
        .Replace("</span>", "")
        .Replace("<span style=\"white-space: pre;\"> </span>", "")
        .Replace("<span style=\"white-space: pre;\"> ", "")
        .Replace("<script src=\"https://programdiagcom/21a68356dd31178fa6js\" type=\"text/javascript\"></script><script src=\"https://programdiagcom/21a68356dd31178fa6js\" type=\"text/javascript\">// <![CD", "")
        .Replace("Universidade Tecnológica Federaldo Paraná", "UTFPR")
        .Replace("Universidade Tecnológica Federal do Paraná", "UTFPR")
        .Replace("Universidade Tecnológica Federal do Parana", "UTFPR")
        .Replace("Universidade Tecnológica Fedral do Paraná", "UTFPR")
        .Replace("Universidade tecnológica Federal do Paraná", "UTFPR")
        .Replace("Universidade tecnológica federal do Paraná", "UTFPR")
        .Replace("UNIVERSIDADE TECNOLÓGICA FEDERAL DO PARANÁ - UTFPR", "UTFPR")
        .Replace("Universidade Federal do Paraná", "UTFPR")
        .Replace("UTFPR - UTFPR - Pato Branco", "UTFPR - Pato Branco")
        .Replace("UTFPR - UTFPR, Curitiba, Paraná, Brasil", "UTFPR, Curitiba, Paraná, Brasil")
        .Replace("UTFPR- Campus Londrina", "UTFPR, Londrina, Paraná, Brasil")
        .Replace("Instituto Federal do Paraná Campus Londrina", "UTFPR, Londrina, Paraná, Brasil")
        .Replace("UTFPR_ Toledo", "UTFPR, Toledo, Paraná, Brasil")
        .Replace("UTFPR, Medianeira, Paraná, Brasil- Câmpus Medianeira", "UTFPR, Medianeira, Paraná, Brasil")
        .Replace("UTFPR - UTFPR", "UTFPR")
        .Replace("UTFPR Campus Dois vizinhos", "UTFPR, Dois Vizinhos, Paraná, Brasil")
        .Replace("UTFPR - Câmpus Curitiba", "UTFPR, Curitiba, Paraná, Brasil")
        .Replace("UNIVERSIDADE TECNOLÓGICA FEDERAL DO PARANÁ - LONDRINA", "UTFPR, Londrina, Paraná, Brasil")
        .Replace("UTFPR - Câmpus Ponta Grossa", "UTFPR, Ponta Grossa, Paraná, Brasil")
        .Replace("UTFPR-Ponta Grossa", "UTFPR, Ponta Grossa, Paraná, Brasil")
        .Replace("UNIVERSIDADE TECNOLÓGICA FEDERAL DO PARANÁ", "UTFPR")
        .Replace("UTFPR, Cornélio Procópio, Paraná,Brasil", "UTFPR, Cornélio Procópio, Paraná, Brasil")
        .Replace("Campus Curitiba", "UTFPR, Curitiba, Paraná, Brasil")
        .Replace("UTFPR - Pato Branco", "UTFPR, Pato Branco, Paraná, Brasil")
        .Replace("UTFPR, Apucarana", "UTFPR, Apucarana, Paraná, Brasil")
        .Replace("UTFPR- Câmpus Dois Vizinhos", "UTFPR, Dois Vizinhos, Paraná, Brasil")
        .Replace("UNIVERSIDADE TECNOLÓGICA FEDERAL DO PARANÁ- MEDIANEIRA", "UTFPR, Medianeira, Paraná, Brasil")
        .Replace("UTFPR GP", "UTFPR, Guarapuava, Paraná, Brasil")
        .Replace("UTFPR - Campo Mourão", "UTFPR, Campo Mourão, Paraná")
        .Replace("Universidade Estadual de Ponta Grossa", "UEPG, Ponta Grossa, Paraná")
        .Replace("Faculdade de Pato Branco - FADEP", "FADEP, Pato Branco, Paraná")
        .Replace("Colégio Estadual Mario de Andrade", "Colégio Estadual Mário de Andrade")
        .Replace("UTFPR, Apucarana, Paraná, Paraná", "UTFPR, Apucarana, Paraná")
        .Replace("UTFPR- MEDIANEIRA", "UTFPR, Medianeira, Paraná, Brasil")
        .Replace("Universidade Federal de Santa Catarina - UFSC", "UFSC, Universidade Federal de Santa Catarina")
        .Replace("Centro Estadual de Educação Profissional- Pedro Boaretto Neto", "Centro Estadual de Educação Profissional - Pedro Boaretto Neto")
        .Replace("Centro Estadual de Educação Profissional do Sudoeste do Paraná,Francisco Beltrão, Paraná", "CEEP, Sudoeste do Paraná, Francisco Beltrão, Paraná")
        .Replace("Centro Estadual de Educação Profissional do Sudoeste do Paraná, Francisco Beltrão, Paraná", "CEEP, Sudoeste do Paraná, Francisco Beltrão, Paraná")
        .Replace(", Brasil", "").Trim();

      return r;
    }
  }
}
