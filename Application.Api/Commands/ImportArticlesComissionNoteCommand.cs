using System.Collections.Generic;
using Application.Api.ViewModels;
using Domains.Article;
using MediatR;
   
namespace Application.Api.Commands
{
    public class ImportArticlesComissionNoteCommand : IRequest<bool>
    {
    	public List<ArticleComissionNote> Notes { get; set; }
    	public EEventIdentifier Event { get; set; }
    }
}