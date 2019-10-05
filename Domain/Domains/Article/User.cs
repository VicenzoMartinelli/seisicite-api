using Infra.Data.MongoIdentityStore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Domain.Domains.Article
{
  public class Person
  {
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string IdentityUserId { get; set; }
    public EUserType Type { get; set; }
    public string Institution { get; set; }
    public bool IsSei { get; set; }
    public bool IsSicite { get; set; }
    public bool Approved { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int ArticlesCount { get; set; }
    public List<string> AttendedModalities { get; set; }

    public Person IncrementArticleCount()
    {
      ArticlesCount++;

      return this;
    }
  }
}
