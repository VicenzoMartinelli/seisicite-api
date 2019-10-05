using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Infra.Data.MongoIdentityStore.Tests")]
namespace Infra.Data.MongoIdentityStore
{
    internal static class Constants
    {
        public const string DefaultCollectionName = "IdentityUser";
    }
}