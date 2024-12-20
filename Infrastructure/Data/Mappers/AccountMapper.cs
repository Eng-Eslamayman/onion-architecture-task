using Core.Entities;
using Infrastructure.Data.Entities;
using System;
using System.Linq;

namespace Infrastructure.Data.Mappers
{
    public static class AccountMapper
    {
        // Map from Domain Model to Entity
        public static AccountEntity ToEntity(Account domain)
        {
            return new AccountEntity
            {
                AccountId = domain.Id.ToString(),
                MobileNumber = domain.MobileNumber.Value,
                FullName = domain.FullName?.Value,
                PhotoUrl = domain.PhotoUrl?.Value, 
                Blocked = domain.Blocked,
                JoinedDate = domain.JoinedDate.ToUniversalTime().Ticks,
                Permissions = domain.Permissions.Select(p => p.ToString()).ToList(),
                Devices = domain.Devices.Select(DeviceMapper.ToEntity).ToList()
            };
        }
  
    }
}
