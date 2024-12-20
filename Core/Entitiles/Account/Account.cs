// Core/Entities/Account.cs
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Security;

namespace Core.Entities
{
    public class Account
    {
        public AccountId Id { get; private set; }
        public MobileNumber MobileNumber { get; private set; }
        public List<Permission> Permissions { get; private set; }
        public FullName FullName { get; private set; }
        public PhotoUrl PhotoUrl { get; private set; }
        public DateTime JoinedDate { get; private set; }
        public List<Device> Devices { get; private set; } = new();
        public bool Blocked { get; private set; }

        public Account(
            AccountId id,
            MobileNumber mobileNumber,
            List<Device> devices,
            List<Permission> permissions,
            FullName fullName,
            PhotoUrl photoUrl,
            bool blocked,
            DateTime joinedDate)
        {
            Id = id;
            MobileNumber = mobileNumber;
            Devices = new List<Device>(devices);
            Permissions = permissions ?? new List<Permission> { Permission.View };
            FullName = fullName;
            PhotoUrl = photoUrl;
            Blocked = blocked;
            JoinedDate = joinedDate == default ? DateTime.UtcNow : joinedDate;
        }

        public static Account NewAccount(string mobileNumber, bool isAdmin, string deviceType, string deviceId)
        {
            var device = Device.Create(deviceId, deviceType);
            return new Account(
                AccountId.Generate(),
                MobileNumber.Create(mobileNumber),
            new List<Device> { device },
            new List<Permission> { Permission.View },
                null,
                null,
                false,
            DateTime.UtcNow);
        }

        public Tokens Authenticate(IJWTGenerator jwtGenerator, string deviceId, string deviceType)
        {
            var usedDevice = GetOrCreateDevice(deviceId, deviceType);
            var refreshToken = jwtGenerator.GenerateRefreshToken(this, usedDevice);
            usedDevice.UpdateRefreshToken(refreshToken);
            var accessToken = jwtGenerator.GenerateAccessToken(this, usedDevice);
            return new Tokens(refreshToken, accessToken);
        }

        private Device GetOrCreateDevice(string deviceId, string deviceType)
        {
            var existingDevice = Devices.Find(d => d.DeviceId.ToString() == deviceId);
            if (existingDevice != null)
                return existingDevice;

            var newDevice = Device.Create(deviceId, deviceType);
            Devices.Add(newDevice);
            return newDevice;
        }
    }
}
