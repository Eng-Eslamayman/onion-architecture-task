using Core.Entities;
using System;

namespace Infrastructure.Data.Entities
{
    public class DeviceEntity
    {
        public DeviceId DeviceId { get; set; }
        public DeviceType DeviceType { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public FCMToken FcmToken { get; set; }
        public long LastAccessTime { get; set; }

        public static DeviceEntity FromDomain(Device domain)
        {
            return new DeviceEntity
            {
                DeviceId = domain.DeviceId,
                DeviceType = domain.DeviceType,
                RefreshToken = domain.RefreshToken,
                FcmToken = domain.FcmToken,
                LastAccessTime = domain.LastAccessTime.ToUniversalTime().Ticks
            };
        }

        public Device ToDomain()
        {
            return Device.Create(
                DeviceId.Value,
                DeviceType.Value,
                RefreshToken.Value,
                FcmToken.Value,
                LastAccessTime
            );
        }
    }
}
