﻿using System;

namespace Infra.Data.MongoIdentityStore.Models
{
    public class MongoUserEmail : MongoUserContactRecord
    {
        public MongoUserEmail(string email) : base(email)
        {
        }

        public string NormalizedValue { get; private set; }

        public virtual void SetNormalizedEmail(string normalizedEmail)
        {
            if (normalizedEmail == null)
            {
                throw new ArgumentNullException(nameof(normalizedEmail));
            }

            NormalizedValue = normalizedEmail;
        }
    }
}
