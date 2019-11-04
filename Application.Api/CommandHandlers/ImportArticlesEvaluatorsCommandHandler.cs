using Application.Api.Commands;
using Application.Api.Services;
using Domain.Domains.Article;
using Domain.Interfaces;
using Domains.Article;
using Infra.Data.MongoIdentityStore;
using Infra.Data.MongoIdentityStore.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Api.CommandHandlers
{
  public class ImportArticlesEvaluatorsCommandHandler : IRequestHandler<ImportArticlesEvaluatorsCommand, bool>
  {
    private readonly IRepository _repository;
    private readonly UserManager<MongoIdentityUser> _userManager;

    public ImportArticlesEvaluatorsCommandHandler(
      IRepository repository,
      UserManager<MongoIdentityUser> userManager)
    {
      _repository = repository;
      _userManager = userManager;
    }

    public async Task<bool> Handle(ImportArticlesEvaluatorsCommand request, CancellationToken cancellationToken)
    {
      var evaluators = new List<EvaluatorsImportAux>();

      List<dynamic> evaluatorsDistinct = request.Articles.SelectMany(x =>
      {
        var ls = new List<object>
        {
          new
          {
            Email = x.Email1.Trim(),
            Nome = x.Avaliador1.Trim()
          }
        };

        if (!string.IsNullOrEmpty(x.Email2))
        {
          ls.Add(new
          {
            Email = x.Email2.Trim(),
            Nome = x.Avaliador2.Trim()
          });
        }

        return ls;
      }).Distinct().ToList();
      var gen = new PassowordGenerator();

      foreach (var avaliador in evaluatorsDistinct)
      {
        MongoIdentityUser avaliadorSalvo = await _userManager.FindByEmailAsync(avaliador.Email);

        if (avaliadorSalvo == null)
        {
          var identityUser = new MongoIdentityUser(avaliador.Email, avaliador.Email);

          identityUser.AddClaim(new MongoUserClaim("Email", avaliador.Email));
          identityUser.AddClaim(new MongoUserClaim("Type", EUserType.Evaluator.ToString()));

          var senha = gen.Generate();
          var result = await _userManager.CreateAsync(identityUser, senha);

          if (!result.Succeeded)
          {
            continue;
          }

          var person = new Person()
          {
            IdentityUserId = identityUser.Id,
            Name = avaliador.Nome,
            IsSei = true,
            Email = avaliador.Email,
            IsSicite = true,
            AttendedModalities = new List<string>(),
            Institution = "UTFPR",
            Type = EUserType.Evaluator,
            Approved = true,
            ArticlesCount = 0
          };

          await _repository.AddAsync(person);

          evaluators.Add(new EvaluatorsImportAux()
          {
            Email = person.Email,
            Id = person.Id,
            Modalidades = new HashSet<string>(),
            Nome = person.Name
          });

          Debug.WriteLine($"User: {person.Id} - {senha}");

          await Task.Delay(100);
        }
        else
        {
          var person = (await _repository.GetByFilter(Builders<Person>.Filter.Eq(x => x.IdentityUserId, avaliadorSalvo.Id))).SingleOrDefault();

          evaluators.Add(new EvaluatorsImportAux()
          {
            Email = person.Email,
            Id = person.Id,
            Modalidades = new HashSet<string>(),
            Nome = person.Name
          });
        }
      }

      foreach (var artImport in request.Articles.Distinct())
      {
        var article = await _repository.GetFirstOrDefaultByFilter(
            Builders<Article>.Filter.And(
              Builders<Article>.Filter.Eq(x => x.SubmissionId, artImport.SubmissionId.ToString()),
              Builders<Article>.Filter.Eq(x => x.Event, request.Event)
            )
        );

        if (article == null)
        {
          Debug.WriteLine($"Artigo: {artImport.SubmissionId} - Não Encontrado no Evento {request.Event.ToString()} - Possível divergência nos arquivos");
          continue;
        }

        article.LocalDetails = artImport.Local;
        article.Room = artImport.Sala;

        var dataDecomposed = artImport.Data.Trim().Split("/").Select(x => Convert.ToInt32(x)).ToArray();
        var horaDecomposed = artImport.Hora.Trim().Split(":").Select(x => Convert.ToInt32(x)).ToArray();
        article.StartDate = new DateTime(dataDecomposed[2], dataDecomposed[0], dataDecomposed[1], horaDecomposed[0], horaDecomposed[1], 0);

        var evaluator1 = evaluators.FirstOrDefault(x => x.Email == artImport.Email1.Trim());
        if (evaluator1 != null)
        {
          article.EvaluatorId = evaluator1.Id;
          evaluator1.Modalidades.Add(article.Modality);
        }

        if (!string.IsNullOrEmpty(artImport.Email2))
        {
          var evaluator2 = evaluators.FirstOrDefault(x => x.Email == artImport.Email2.Trim());

          if (evaluator2 != null)
          {
            article.Evaluator2Id = evaluator2.Id;

            evaluator2.Modalidades.Add(article.Modality);
          }
        }

        await _repository.SaveOrUpdateAsync(article, article.Id);

        await Task.Delay(100);
      }

      foreach (var evaluat in evaluators)
      {
        var eval = await _repository.GetByIdAsync<Person>(evaluat.Id);

        var modalities = new List<string>(eval.AttendedModalities);

        modalities.AddRange(evaluat.Modalidades);

        eval.AttendedModalities = modalities.Distinct().ToList();

        await _repository.SaveOrUpdateAsync(eval, eval.Id);

        await Task.Delay(100);
      }

      return true;
    }
  }

  public class EvaluatorsImportAux
  {
    public string Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public HashSet<string> Modalidades { get; set; }
  }
}
