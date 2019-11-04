using Application.Api.CommandHandlers;
using Application.Api.Commands;
using Application.Api.ViewModels;
using Infra.CrossCuting.Shared.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Services.Seisicite.Api.CommandHandlers;
using Services.Seisicite.Api.Commands;

namespace Infra.Ioc.Configs
{
  public static class CommandsProvider
  {
    public static void AddCommandsServices(this IServiceCollection services)
    {
      services.AddScoped<IRequestHandler<ImportArticlesCommand, bool>, ImportArticlesCommandHandler>();
      services.AddScoped<IRequestHandler<ImportArticlesComissionNoteCommand, bool>, ImportArticlesComissionNoteCommandHandler>();
      services.AddScoped<IRequestHandler<ImportArticlesEvaluatorsCommand, bool>, ImportArticlesEvaluatorsCommandHandler>();
      services.AddScoped<IRequestHandler<UpdateArticleCommand, CommandResult<ArticleViewModel>>, UpdateArticleCommandHandler>();
      services.AddScoped<IRequestHandler<ApproveEvaluatorCommand, bool>, ApproveEvaluatorCommandHandler>();
      services.AddScoped<IRequestHandler<UpdateIsEventCommand, bool>, UpdateIsEventCommandHandler>();
      services.AddScoped<IRequestHandler<SortArticleEvaluatorsCommand, bool>, SortArticleEvaluatorsCommandHandler>();
      services.AddScoped<IRequestHandler<CancelEvaluatorsCommand, bool>, CancelEvaluatorsCommandHandler>();
      services.AddScoped<IRequestHandler<EvaluateArticleCommand, bool>, EvaluateArticleCommandHandler>();
    }
  }
}
