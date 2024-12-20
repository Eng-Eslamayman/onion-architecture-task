namespace Application.UseCases
{
    public class AuthenticateCommand
    {
        public string IdToken { get; }
        public string DeviceId { get; }
        public string DeviceType { get; }

        public AuthenticateCommand(string idToken, string deviceId, string deviceType)
        {
            IdToken = idToken;
            DeviceId = deviceId;
            DeviceType = deviceType;
        }
    }
}
