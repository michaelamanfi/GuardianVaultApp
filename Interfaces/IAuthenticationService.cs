namespace GuardianVault
{
    /// <summary>
    /// Provides methods for authenticating users.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticates a user based on the username and password.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>True if authentication is successful; otherwise, false.</returns>
        bool AuthenticateUser(string username, string password);
    }
}