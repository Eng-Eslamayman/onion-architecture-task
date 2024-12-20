using System;

namespace Core.Entities
{
    public class DeviceId
    {
        private const int MinLength = 15;
        private const int MaxLength = 50;

        public string Value { get; private set; }

        private DeviceId(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < MinLength || value.Length > MaxLength)
                throw new ArgumentException($"DeviceId must be between {MinLength} and {MaxLength} characters.");

            Value = value;
        }

        public static DeviceId Create(string value)
        {
            return new DeviceId(value);
        }
    }
}
