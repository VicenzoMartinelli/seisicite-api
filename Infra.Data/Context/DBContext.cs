using MongoDB.Driver;

namespace Infra.Data.Context
{
  public class DBContext : IDBContext
  {
    private IMongoDatabase db;

    public IMongoDatabase GetDatabase(string connectionString)
    {
      if (db == null)
      {
        var mongoClient = new MongoClient(connectionString);

        db = mongoClient.GetDatabase("Seisicite");
      }

      return db;
    }
  }
}