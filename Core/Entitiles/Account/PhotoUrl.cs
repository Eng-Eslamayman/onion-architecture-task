using System;
using System.Text.RegularExpressions;

namespace Core.Entities
{
    public class PhotoUrl
    {
        private static readonly Regex UrlPattern = new(@"https?://.+\..+");

        public string Value { get; private set; }

        private PhotoUrl(string value)
        {
            if (!UrlPattern.IsMatch(value))
                throw new ArgumentException("Invalid photo URL.");
            Value = value;
        }

        public static PhotoUrl Create(string value)
        {
            return new PhotoUrl(value);
        }
    }
}
