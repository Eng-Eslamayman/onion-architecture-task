namespace Core.Interfaces
{
    public interface IAuthProvider
    {
        Task<string> GetVerifiedMobileNumberAsync(string idToken);
    }
}
