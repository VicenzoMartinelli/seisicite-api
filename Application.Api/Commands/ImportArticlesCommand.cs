using System.Collections.Generic;
using Application.Api.ViewModels;
using Domains.Article;
using MediatR;
   
namespace Application.Api.Commands
{
    public class ImportArticlesCommand : IRequest<bool>
    {
    	public List<ArticleImport> Articles { get; set; }
    	public EEventIdentifier Event { get; set; }
    }
}