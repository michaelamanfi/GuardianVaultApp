using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Provides a method for signing in to the application.
    /// </summary>
    public interface ISignInController
    {
        /// <summary>
        /// Performs the sign-in process and returns the sign-in model.
        /// </summary>
        /// <returns>The SignInModel containing the sign-in details.</returns>
        SignInModel SignIn();
    }
}