// Core/Entities/FullName.cs
using System;

namespace Core.Entities
{
    public class FullName
    {
        public string Value { get; private set; }

        private FullName(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 2 || value.Length > 50)
                throw new ArgumentException("Full name must be between 2 and 50 characters.");
            Value = value;
        }

        public static FullName Create(string value)
        {
            return new FullName(value);
        }
    }
}
