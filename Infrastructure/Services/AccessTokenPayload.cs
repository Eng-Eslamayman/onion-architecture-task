using System;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    public class AccessTokenPayload
    {
        public string Uuid { get; }
        public string FullName { get; }
        public string PhotoUrl { get; }
        public bool Blocked { get; }
        public string MobileNumber { get; }
        public string DeviceId { get; }
        public List<string> Permissions { get; }

        public AccessTokenPayload(
            string audience,
            string jwtId,
            DateTime expireAt,
            DateTime issuedAt,
            string uuid,
            string fullName,
            string photoUrl,
            bool blocked,
            string mobileNumber,
            string deviceId,
            List<string> permissions
        )
        {
            Uuid = uuid;
            FullName = fullName;
            PhotoUrl = photoUrl;
            Blocked = blocked;
            MobileNumber = mobileNumber;
            DeviceId = deviceId;
            Permissions = permissions ?? new List<string>();
        }

        public Dictionary<string, object> ToMap()
        {
            var userData = new Dictionary<string, object>
            {
                { "uuid", Uuid },
                { "blocked", Blocked },
                { "type", "accessToken" },
                { "device_id", DeviceId },
                { "mobile_number", MobileNumber },
                { "permissions", Permissions }
            };

            if (!string.IsNullOrEmpty(FullName))
                userData["full_name"] = FullName;
            if (!string.IsNullOrEmpty(PhotoUrl))
                userData["photo_url"] = PhotoUrl;

            return userData;
        }

        public static AccessTokenPayload FromPayload(IDictionary<string, object> claims)
        {
            return new AccessTokenPayload(
                (string)claims["aud"],
                (string)claims["jti"],
                DateTime.Parse(claims["exp"].ToString()),
                DateTime.Parse(claims["iat"].ToString()),
                (string)claims["uuid"],
                claims.ContainsKey("full_name") ? (string)claims["full_name"] : null,
                claims.ContainsKey("photo_url") ? (string)claims["photo_url"] : null,
                bool.Parse(claims["blocked"].ToString()),
                (string)claims["mobile_number"],
                (string)claims["device_id"],
                (List<string>)claims["permissions"]
            );
        }
    }
}
