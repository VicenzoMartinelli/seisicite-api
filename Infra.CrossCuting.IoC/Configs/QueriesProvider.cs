using Application.Api.Queries;
using Application.Api.QueryHandlers;
using Application.Api.ViewModels;
using Domains.Article;
using Infra.CrossCuting.Shared.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Ioc.Configs
{
  public static class QueriesProvider
  {
    public static void AddQueriesServices(this IServiceCollection services)
    {
      services.AddScoped<IRequestHandler<ListArticlesPaginatedByEventQuery, PaginatedList<ArticleViewModel>>, ListArticlesPaginatedByEventQueryHandler>();
      services.AddScoped<IRequestHandler<ListAvaliadoresByEventoQuery, List<AvaliadorViewModel>>, ListAvaliadoresByEventoQueryHandler>();
      services.AddScoped<IRequestHandler<ListModalitiesQuery, List<string>>, ListModalitiesQueryHandler>();
      services.AddScoped<IRequestHandler<ListInstitutionsQuery, List<string>>, ListInstitutionsQueryHandler>();

      services.AddScoped<IRequestHandler<ListEvaluatorsToApproveQuery, List<AvaliadorViewModel>>, ListEvaluatorsToApproveQueryHandler>();
      services.AddScoped<IRequestHandler<ListApprovedsEvaluatorsQuery, List<AvaliadorViewModel>>, ListApprovedsEvaluatorsQueryHandler>();

      services.AddScoped<IRequestHandler<ListArticlesToEvaluateQuery, List<ArticleViewModel>>, ListArticlesToEvaluateQueryHandler>();
      services.AddScoped<IRequestHandler<ReportArticlesNoteQuery, List<ArticleFinalReportViewModel>>, ReportArticlesNoteQueryHandler>();

      services.AddScoped<IRequestHandler<CanEvaluateArticleQuery, bool>, CanEvaluateArticleQueryHandler>();
    }
  }
}
