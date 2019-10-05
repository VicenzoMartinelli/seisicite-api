namespace Infra.Data.MongoIdentityStore.Models
{
    public class MongoUserPhoneNumber : MongoUserContactRecord
    {
        public MongoUserPhoneNumber(string phoneNumber) : base(phoneNumber)
        {
        }
    }
}
