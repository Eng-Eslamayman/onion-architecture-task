using System;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    public class RefreshTokenPayload
    {
        public string Uuid { get; }
        public string MobileNumber { get; }
        public string DeviceId { get; }

        public RefreshTokenPayload(
            string audience,
            string jwtId,
            DateTime expireAt,
            DateTime issuedAt,
            string uuid,
            string mobileNumber,
            string deviceId
        )
        {
            Uuid = uuid;
            MobileNumber = mobileNumber;
            DeviceId = deviceId;
        }

        public Dictionary<string, object> ToMap()
        {
            return new Dictionary<string, object>
            {
                { "uuid", Uuid },
                { "type", "refreshToken" },
                { "mobileNumber", MobileNumber },
                { "device_id", DeviceId }
            };
        }

        public static RefreshTokenPayload FromPayload(IDictionary<string, object> claims)
        {
            return new RefreshTokenPayload(
                (string)claims["aud"],
                (string)claims["jti"],
                DateTime.Parse(claims["exp"].ToString()),
                DateTime.Parse(claims["iat"].ToString()),
                (string)claims["uuid"],
                (string)claims["mobileNumber"],
                (string)claims["device_id"]
            );
        }
    }
}
