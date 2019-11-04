using Application.Api.Commands;
using Domain.Interfaces;
using Domains.Article;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Api.CommandHandlers
{
  public class ImportArticlesComissionNoteCommandHandler : IRequestHandler<ImportArticlesComissionNoteCommand, bool>
  {
    private readonly IRepository _repository;

    public ImportArticlesComissionNoteCommandHandler(IRepository repository)
    {
      _repository = repository;
    }
    public async Task<bool> Handle(ImportArticlesComissionNoteCommand request, CancellationToken cancellationToken)
    {
      var tasks = new List<Task>();

      foreach (var item in request.Notes.Distinct())
      {

        if (!string.IsNullOrEmpty(item.Media) && !item.Media.Equals("-") && !item.Media.Equals("—"))
        {
          var article = await _repository.GetFirstOrDefaultByFilter(
            Builders<Article>.Filter.And(
              Builders<Article>.Filter.Eq(x => x.SubmissionId, item.IdDaSubmissão.ToString()),
              Builders<Article>.Filter.Eq(x => x.CommissionNote, 0)
            )
          );

          if (article != null)
          {
            try
            {
              var nota = Convert.ToDouble(item.Media.Replace(",", "."));

              article.CommissionNote = request.Event == EEventIdentifier.Sei ? nota : nota * 2;

              await _repository.UpdateAsync(article, article.Id);
              await Task.Delay(100);
            }
            catch (Exception)
            {
              Debug.WriteLine("sub: {0}, media: {1}", item.IdDaSubmissão, item.Media);
            }
          }
        }
      }

      return true;
    }
  }
}
