using System;

namespace Core.Entities
{
    public class RefreshToken
    {
        public string Value { get; private set; }

        private RefreshToken(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Refresh token cannot be empty.");

            Value = value;
        }

        public static RefreshToken Create(string value)
        {
            return new RefreshToken(value);
        }
    }
}
