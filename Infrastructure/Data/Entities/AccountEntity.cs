using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Entities
{
    public class AccountEntity
    {
        public string AccountId { get; set; }
        public string MobileNumber { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        public bool Blocked { get; set; }
        public long JoinedDate { get; set; }
        public long LastUpdated { get; set; }
        public List<string> Permissions { get; set; }
        public List<DeviceEntity> Devices { get; set; }

        // Map from Domain Model to Entity
        public static AccountEntity FromDomain(Account domain)
        {
            return new AccountEntity
            {
                AccountId = domain.Id.Value.ToString(),
                MobileNumber = domain.MobileNumber.Value,
                FullName = domain.FullName?.Value,
                PhotoUrl = domain.PhotoUrl?.Value,
                Blocked = domain.Blocked,
                JoinedDate = domain.JoinedDate.ToUniversalTime().Ticks,
                LastUpdated = DateTime.UtcNow.Ticks,
                Permissions = domain.Permissions.Select(p => p.ToString()).ToList(),
                Devices = domain.Devices.Select(DeviceEntity.FromDomain).ToList()
            };
        }
     
    }
}
