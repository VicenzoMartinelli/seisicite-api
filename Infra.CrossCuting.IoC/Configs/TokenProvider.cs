using Application.Api.Messages;
using Application.Api.ViewModels;
using Domain.Domains.Article;
using Infra.Data.MongoIdentityStore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Services.Seisicite.Api.CommandHandlers;
using Services.Seisicite.Api.Commands;
using System;
using System.Text;

namespace Infra.Ioc.Configs
{
  public static class TokenProvider
  {
    public static void AddIdentityTokenProvider(this IServiceCollection services)
    {
      IConfiguration conf = services.BuildServiceProvider().GetService<IConfiguration>();

      services.AddSingleton<IUserStore<MongoIdentityUser>>(provider =>
      {
        MongoClient client = new MongoClient(conf.GetConnectionString("MongoServer"));
        IMongoDatabase database = client.GetDatabase("Seisicite");

        return MongoUserStore<MongoIdentityUser>.CreateAsync(database).GetAwaiter().GetResult();
      });

      services.AddIdentity<MongoIdentityUser>()
          .AddDefaultTokenProviders();

      services.AddScoped<IdentityErrorDescriber, PersonalIdentityErrorDescriber>();

      services.Configure<IdentityOptions>(options =>
      {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Lockout.MaxFailedAccessAttempts = 10;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(2);
      });

      IConfigurationSection secSettings = conf.GetSection("SecuritySettings");
      services.Configure<SecuritySettings>(secSettings);

      SecuritySettings secObj = secSettings.Get<SecuritySettings>();
      byte[] key = Encoding.ASCII.GetBytes(secObj.Secret);

      services.AddSingleton(secObj);

      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(x =>
      {
        x.RequireHttpsMetadata = true;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = true,
          ValidateAudience = false,
          ValidIssuer = secObj.Issuer
        };
      });

      services.AddScoped<IRequestHandler<RegisterUserCommand, Token>, AuthCommandHandler>();
      services.AddScoped<IRequestHandler<RegisterUserEvaluatorCommand, Token>, AuthCommandHandler>();
      services.AddScoped<IRequestHandler<LoginCommand, Token>, AuthCommandHandler>();
    }
  }
}
