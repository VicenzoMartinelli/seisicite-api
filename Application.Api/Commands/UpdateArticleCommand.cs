using Application.Api.ViewModels;
using Domains.Article;
using Infra.CrossCuting.Shared.Models;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Api.Commands
{
  public class UpdateArticleCommand : IRequest<CommandResult<ArticleViewModel>>
  {
    [Required]
    public string Id { get; set; }
    [Required]
    public string Modality { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Resume { get; set; }
    public string LocalDetails { get; set; }
    [Required]
    public DateTime? StartDate { get; set; }
    [Required]
    public EApresentationType? Type { get; set; }
    [Required]
    public string Building { get; set; }
    [Required]
    public string Room { get; set; }
    [Required]
    public string EvaluatorId { get; set; }
    public string Evaluator2Id { get; set; }
  }
}