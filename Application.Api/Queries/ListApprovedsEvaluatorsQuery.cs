using Application.Api.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace Application.Api.Queries
{
  public class ListApprovedsEvaluatorsQuery : IRequest<List<AvaliadorViewModel>>
  {
    public string Modality { get; set; }
  }
}