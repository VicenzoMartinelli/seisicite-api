using Domain.Core.Commands;
using Domains.Article;

namespace Services.Seisicite.Api.Commands
{
  public class SortArticleEvaluatorsCommand : Command<bool>
  {
    public SortArticleEvaluatorsCommand(EEventIdentifier @event)
    {
      Event = @event;
    }

    public EEventIdentifier Event { get; set; }
  }
}
