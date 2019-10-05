using Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.Seisicite.Api.Commands
{
  public class ApproveEvaluatorCommand : Command<bool>
  {
    public IEnumerable<string> UserIds { get; set; }
  }
}
