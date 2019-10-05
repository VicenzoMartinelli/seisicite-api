using Domain.Core.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Core.Models
{
  public class OccurrencesViewModel
  {
    public IEnumerable<Notification> Occurrences { get; set; }
    
    public DateTime DateTime { get; }
    public int Count => Occurrences.Count();

    public OccurrencesViewModel(IEnumerable<Notification> errors)
    {
      Occurrences = errors;
      DateTime    = DateTime.UtcNow;
    }
  }
}
