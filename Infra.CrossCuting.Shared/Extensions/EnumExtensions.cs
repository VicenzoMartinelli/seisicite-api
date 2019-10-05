using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.CrossCuting.Shared.Extensions
{
  public static class EnumExtensions
  {
    public static string AsString(this Enum value)
    {
      return value.ToString();
    }
  }
}
