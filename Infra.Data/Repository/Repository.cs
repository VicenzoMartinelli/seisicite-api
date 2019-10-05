using Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
  public class Repository : IRepository
  {
    private readonly IMongoDatabase db;

    public Repository(IMongoDatabase db)
    {
      this.db = db;
    }

    public async Task<T> GetByIdAsync<T>(string id) where T : class
    {
      if (!id.Length.Equals(24))
      {
        return default(T);
      }

      var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));

      var x = await db.GetCollection<T>(typeof(T).Name)
          .Find(filter).FirstOrDefaultAsync();
      return x;
    }

    public async Task<List<T>> GetAll<T>() where T : class
    {
      var lst = await db.GetCollection<T>(typeof(T).Name).FindAsync("{}");
      return lst.ToList();
    }

    public async Task<List<T>> GetByFilter<T>(FilterDefinition<T> filter) where T : class
    {
      var lst = await db.GetCollection<T>(typeof(T).Name).FindAsync(filter);
      return lst.ToList();
    }

    public Task<T> GetFirstOrDefaultByFilter<T>(FilterDefinition<T> filter) where T : class
    {
      return db.GetCollection<T>(typeof(T).Name).Find(filter).FirstOrDefaultAsync();
    }
    public async Task<List<T>> GetByFilter<T>(FilterDefinition<T> filter, string collectionName) where T : class
    {
      var lst = await db.GetCollection<T>(collectionName).FindAsync(filter);
      return lst.ToList();
    }
    public async Task<T> AddAsync<T>(T source) where T : class
    {
      var collection = db.GetCollection<T>(typeof(T).Name);

      try
      {
        await collection.InsertOneAsync(source);

        return source;
      }
      catch (Exception e)
      {
        throw e;
      }
    }
    public async Task<IList<T>> AddManyAsync<T>(IList<T> source) where T : class
    {
      var collection = db.GetCollection<T>(typeof(T).Name);

      try
      {
        await collection.InsertManyAsync(source);

        return source;
      }
      catch (Exception e)
      {
        throw e;
      }
    }

    public async Task<T> UpdateAsync<T>(T source, string id) where T : class
    {
      var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));

      await db.GetCollection<T>(typeof(T).Name).ReplaceOneAsync(filter, source);
      return source;
    }

    public async Task<bool> DeleteAsync<T>(string id) where T : class
    {
      try
      {
        var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
        await db.GetCollection<T>(typeof(T).Name).DeleteOneAsync(filter);

        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    public Task<T> SaveOrUpdateAsync<T>(T source, string id) where T : class
    {
      if (string.IsNullOrEmpty(id))
      {
        return AddAsync(source);
      }
      else
      {
        return UpdateAsync(source, id);
      }
    }
    public IQueryable<T> Query<T>() where T : class => db.GetCollection<T>(typeof(T).Name).AsQueryable();
    public IQueryable<T> Query<T>(string collectionName) where T : class => db.GetCollection<T>(collectionName).AsQueryable();
    public Task<IQueryable<T>> QueryAsync<T>() where T : class
    {
      return Task.FromResult((IQueryable<T>)db.GetCollection<T>(typeof(T).Name).AsQueryable());
    }

    public Task<IQueryable<T>> QueryAsync<T>(string collectionName) where T : class
    {
      return Task.FromResult((IQueryable<T>)db.GetCollection<T>(collectionName).AsQueryable());
    }
  }
}
