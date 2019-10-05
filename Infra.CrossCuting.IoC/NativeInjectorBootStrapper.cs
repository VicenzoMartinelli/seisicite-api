using Domain.Core.Notifications;
using Infra.Ioc.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Ioc
{
  public static class NativeInjectorBootStrapper
  {
    public static void AddNativeBootStrapper(this IServiceCollection services)
    {
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      services.AddCommandsServices();

      services.AddQueriesServices();

      services.AddDataServices();
      
      services.AddIdentityTokenProvider();

      services.AddScoped<NotificationContext>();
    }
  }
}
