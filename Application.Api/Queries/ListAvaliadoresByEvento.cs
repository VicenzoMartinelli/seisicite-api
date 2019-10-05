using System.Collections.Generic;
using Application.Api.ViewModels;
using Domains.Article;
using MediatR;

namespace Application.Api.Queries
{
  public class ListAvaliadoresByEventoQuery : IRequest<List<AvaliadorViewModel>>
  {
    public EEventIdentifier Event { get; set; }
    public string Modality { get; set; }
  }
}