using System.Collections.Generic;

namespace Infra.CrossCuting.Shared.Models
{
  public class PaginatedList<T>
  {
    public List<T> Items { get; set; }
    public int Page { get; set; }
  }
}
