using Microsoft.AspNetCore.Builder;

namespace Services.Seisicite.Api.Configs
{
  public static class CorsConfig
  {
    public static void AddCorsConfig(this IApplicationBuilder app)
    {
      app.UseCors(c =>
      {
        c.AllowAnyHeader();
        c.AllowAnyMethod();
        c.AllowAnyOrigin();
      });
    }
  }
}
