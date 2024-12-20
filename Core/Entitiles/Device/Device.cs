// Core/Entities/Device.cs
using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Device
    {
        public DeviceId DeviceId { get; private set; }
        public DeviceType DeviceType { get; private set; }
        public RefreshToken RefreshToken { get; private set; }
        public FCMToken FcmToken { get; private set; }
        public DateTime LastAccessTime { get; private set; }

        private Device(
            DeviceId deviceId,
            DeviceType deviceType,
            DateTime lastAccessTime,
            RefreshToken refreshToken,
            FCMToken fcmToken)
        {
            DeviceId = deviceId;
            DeviceType = deviceType;
            LastAccessTime = lastAccessTime;
            RefreshToken = refreshToken;
            FcmToken = fcmToken;
        }

        public static Device Create(string deviceId, string deviceType, string refreshToken = null, string fcmToken = null, long? lastAccessTime = null)
        {
            return new Device(
                DeviceId.Create(deviceId),
                DeviceType.Create(deviceType),
                lastAccessTime.HasValue ? DateTimeOffset.FromUnixTimeMilliseconds(lastAccessTime.Value).UtcDateTime : DateTime.UtcNow,
                refreshToken != null ? RefreshToken.Create(refreshToken) : null,
                fcmToken != null ? FCMToken.Create(fcmToken) : null
            );
        }

        public void UpdateRefreshToken(string newRefreshToken)
        {
            RefreshToken = RefreshToken.Create(newRefreshToken);
            LastAccessTime = DateTime.UtcNow;
        }

        public void UpdateFcmToken(string newFcmToken)
        {
            FcmToken = FCMToken.Create(newFcmToken);
            LastAccessTime = DateTime.UtcNow;
        }
    }
}
