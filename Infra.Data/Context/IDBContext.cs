using MongoDB.Driver;

namespace Infra.Data.Context
{
  public interface IDBContext
  {
    IMongoDatabase GetDatabase(string connectionString);
  }
}
