using System;

namespace Core.Entities
{
    public class DeviceType
    {
        public string Value { get; private set; }

        private DeviceType(string value)
        {
            if (!IsValid(value))
                throw new ArgumentException("Invalid device type.");

            Value = value;
        }

        public static DeviceType Create(string value)
        {
            var normalizedValue = value.ToLowerInvariant();
            if (!IsValid(normalizedValue))
                throw new ArgumentException("Invalid device type.");

            return new DeviceType(normalizedValue);
        }

        private static bool IsValid(string value)
        {
            return value == "android" || value == "ios" || value == "web";
        }
    }
}
