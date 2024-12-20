// Core/Entities/AccountId.cs
using System;

namespace Core.Entities
{
    public class AccountId
    {
        public Guid Value { get; private set; }

        private AccountId(Guid value)
        {
            Value = value;
        }

        public static AccountId Generate()
        {
            return new AccountId(Guid.NewGuid());
        }
    }
}
