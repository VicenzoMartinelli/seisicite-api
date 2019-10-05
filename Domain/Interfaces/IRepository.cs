using System.Linq;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
  public interface IRepository
  {
    Task<IQueryable<T>> QueryAsync<T>() where T : class;
    Task<IQueryable<T>> QueryAsync<T>(string collectionName) where T : class;
    Task<T> GetByIdAsync<T>(string id) where T : class;
    Task<List<T>> GetAll<T>() where T : class;
    Task<List<T>> GetByFilter<T>(FilterDefinition<T> filter) where T : class;
    Task<List<T>> GetByFilter<T>(FilterDefinition<T> filter, string collectionName) where T : class;
    Task<T> AddAsync<T>(T source) where T : class;
    Task<T> UpdateAsync<T>(T source, string id) where T : class;
    Task<T> SaveOrUpdateAsync<T>(T source, string id) where T : class;
    Task<bool> DeleteAsync<T>(string id) where T : class;
    IQueryable<T> Query<T>() where T : class;
    IQueryable<T> Query<T>(string collectionName) where T : class;
    Task<T> GetFirstOrDefaultByFilter<T>(FilterDefinition<T> filter) where T : class;
    Task<IList<T>> AddManyAsync<T>(IList<T> source) where T : class;
  }
}
