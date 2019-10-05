using Domain.Interfaces;
using Infra.Data.Config;
using Infra.Data.Context;
using Infra.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using System;

namespace Infra.Ioc.Configs
{
  public static class DataServiceProvider
  {
    public static void AddDataServices(this IServiceCollection services)
    {

      BsonSerializer.RegisterSerializer(typeof(DateTime), new BsonUtcDateTimeSerializer());

      services.AddScoped<IDBContext, DBContext>();

      services.AddScoped((x) =>
      {
        return x.GetService<IDBContext>().GetDatabase(x.GetService<IConfiguration>().GetConnectionString("MongoServer"));
      });

      services.AddScoped<IRepository, Repository>();

    }
  }
}
