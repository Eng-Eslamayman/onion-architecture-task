// Core/Entities/MobileNumber.cs
using System;
using System.Text.RegularExpressions;

namespace Core.Entities
{
    public class MobileNumber
    {
        private static readonly Regex MobilePattern = new(@"^[0-9]{7,15}$");

        public string Value { get; private set; }

        private MobileNumber(string value)
        {
            if (!MobilePattern.IsMatch(value))
                throw new ArgumentException("Invalid mobile number format.");
            Value = value;
        }

        public static MobileNumber Create(string value)
        {
            if (value.StartsWith("00"))
                value = value[2..];
            else if (value.StartsWith("+"))
                value = value[1..];
            return new MobileNumber(value);
        }
    }
}
