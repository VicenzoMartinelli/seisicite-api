using System;

namespace Infra.Data.MongoIdentityStore.Models
{
    public class MongoUserToken 
    {
        public string LoginProvider { get; set; }
        public string TokenName { get;  set; }
        public string TokenValue { get;  set; }
    }
}