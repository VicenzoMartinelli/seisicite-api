using Application.Api.ViewModels;
using Domains.Article;
using MediatR;
using System.Collections.Generic;

namespace Application.Api.Queries
{
  public class ListEvaluatorsToApproveQuery : IRequest<List<AvaliadorViewModel>>
  {

  }
}