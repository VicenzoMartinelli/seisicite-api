using Infra.Ioc;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Seisicite.Api.Configs;
using Services.Seisicite.Api.Configurations;
using Services.Seisicite.Api.Filters;

namespace Services.Seisicite.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors();

      services.AddMvc((m) =>
      {
        m.Filters.Add(typeof(NotificationActionFilter));
      }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      services.AddMediatR(typeof(Startup));

      services.AddAutoMapperConfig();

      services.AddSwaggerConfig();

      services.AddNativeBootStrapper();
    }
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseCors(builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      //app.UseHttpsRedirection();

      app.UseAuthentication();

      app.UseMvc();

      app.UseSwagger();

      app.UseSwaggerUI(s =>
      {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "SeiSicite API v1.0");
      });
    }
  }
}
