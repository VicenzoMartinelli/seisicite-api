using System;

namespace Infra.Data.MongoIdentityStore.Models
{
    public class FutureOccurrence : Occurrence
    {
        public FutureOccurrence()
        {
        }

        public FutureOccurrence(DateTime willOccurOn) : base(willOccurOn)
        {
        }
    }
}
