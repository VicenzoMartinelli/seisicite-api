using Domain.Core.Commands;
using System.Collections.Generic;

namespace Services.Seisicite.Api.Commands
{
  public class CancelEvaluatorsCommand : Command<bool>
  {
    public IEnumerable<string> UserIds { get; set; }
  }
}
