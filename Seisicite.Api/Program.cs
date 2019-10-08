using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Seisicite.Api.Extensions;

namespace Services.Seisicite.Api
{
  public class Program
  {
    public static void Main(string[] args)
    {
      WebHost
        .CreateDefaultBuilder(args)
        //.UsePort()
        .UseStartup<Startup>()
        .Build()
        .Run();
    }
  }
}
