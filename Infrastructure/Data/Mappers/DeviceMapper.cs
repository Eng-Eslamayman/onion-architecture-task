using Core.Entities;
using Core.Entitiles.Device;
using Infrastructure.Data.Entities;

namespace Infrastructure.Data.Mappers
{
    public static class DeviceMapper
    {
        public static DeviceEntity ToEntity(Device device)
        {
            return new DeviceEntity
            {
                DeviceId = device.DeviceId.Value,
                DeviceType = device.DeviceType.Value,
                RefreshToken = device.RefreshToken.Value,
                FcmToken = device.FcmToken.Value,
                LastAccessTime = device.LastAccessTime.Ticks
            };
        }

        public static Device ToDomain(DeviceEntity entity)
        {
            return new Device
            {
                DeviceId = entity.DeviceId,
                DeviceType = entity.DeviceType,
                RefreshToken = entity.RefreshToken,
                FcmToken = entity.FcmToken,
                LastAccessTime = new DateTime(entity.LastAccessTime)
            };
        }
    }
}
