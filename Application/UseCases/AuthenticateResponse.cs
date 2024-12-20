namespace Application.UseCases
{
    public class AuthenticateResponse
    {
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public AuthenticateResponse(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
