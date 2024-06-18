using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Represents the view for user sign-in.
    /// </summary>
    public partial class SignInView : IView<SignInModel>
    {
        /// <summary>
        /// Displays the sign-in dialog as a modal window and returns the sign-in model with the authentication result.
        /// </summary>
        /// <param name="parent">The parent window for this view, used to center the dialog.</param>
        /// <param name="model">The sign-in model to be used and potentially modified in the dialog.</param>
        /// <returns>An updated <see cref="SignInModel"/> reflecting the outcome of the sign-in attempt.</returns>
        public SignInModel ShowView(IWin32Window parent, SignInModel model)
        {
            using (SignInDlg dlg = new SignInDlg())
            {
                // Show the dialog as a modal window and check if the user clicked OK
                if (dlg.ShowDialog(parent) == DialogResult.OK)
                {
                    // If sign-in is successful, update the user name in upper case and set Authenticated to true
                    return new SignInModel { UserName = dlg.UserName.ToUpper(), Authenticated = true };
                }
                else
                {
                    // Return false if the dialog was canceled or closed without confirmation
                    return new SignInModel { Authenticated = false };
                }
            }
        }
    }
}
