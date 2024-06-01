namespace GuardianVault
{
    public interface IAuthenticationService
    {
        bool AuthenticateUser(string username, string password);
    }
}