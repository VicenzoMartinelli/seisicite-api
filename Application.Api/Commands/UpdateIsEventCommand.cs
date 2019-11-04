using Domain.Core.Commands;
using Domains.Article;

namespace Services.Seisicite.Api.Commands
{
  public class UpdateIsEventCommand : Command<bool>
  {
    public string UserId { get; set; }
    public EEventIdentifier Event { get; set; }
  }
}
