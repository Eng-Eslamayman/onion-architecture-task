using System;

namespace Core.Entities
{
    public class FCMToken
    {
        public string Value { get; private set; }

        private FCMToken(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("FCM token cannot be empty.");

            Value = value;
        }

        public static FCMToken Create(string value)
        {
            return new FCMToken(value);
        }
    }
}
