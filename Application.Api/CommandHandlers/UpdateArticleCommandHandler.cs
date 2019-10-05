using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Api.Commands;
using Application.Api.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domains.Article;
using Infra.CrossCuting.Shared.Models;
using MediatR;

namespace Application.Api.CommandHandlers
{
  public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, CommandResult<ArticleViewModel>>
  {
    private IRepository _repository;
    private readonly IMapper _mapper;

    public UpdateArticleCommandHandler(IRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper     = mapper;
    }
    public async Task<CommandResult<ArticleViewModel>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
      var article = await _repository.GetByIdAsync<Article>(request.Id);

      _mapper.Map(request, article);

      await _repository.SaveOrUpdateAsync(article, article.Id);

      return new CommandResult<ArticleViewModel>(true){
        Result = _mapper.Map<ArticleViewModel>(article)
      };
    }
  }
}
