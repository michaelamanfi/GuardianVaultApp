using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Represents the view for managing the master password.
    /// </summary>
    public partial class MasterPasswordView : IView<MasterPasswordModel>
    {
        /// <summary>
        /// Displays the master password dialog as a modal window and updates the master password model based on user input.
        /// </summary>
        /// <param name="parent">The parent window for this view, used to center the dialog.</param>
        /// <param name="masterPasswordModel">The master password model to be displayed and potentially updated in the dialog.</param>
        /// <returns>An updated <see cref="MasterPasswordModel"/> reflecting any changes made by the user.</returns>
        public MasterPasswordModel ShowView(IWin32Window parent, MasterPasswordModel masterPasswordModel)
        {
            using (MasterPasswordDlg dlg = new MasterPasswordDlg())
            {
                // Set dialog controls from the model
                dlg.txtPassword.Text = masterPasswordModel.HashValue;
                dlg.chRememberKey.Checked = masterPasswordModel.RememberSecret;

                // Show the dialog as a modal window and check if the user clicked OK
                if (dlg.ShowDialog(parent) == DialogResult.OK)
                {
                    // Update the model based on the dialog's user inputs
                    masterPasswordModel.RememberSecret = dlg.chRememberKey.Checked;
                    masterPasswordModel.HashValue = dlg.txtPassword.Text.Trim();

                    // Log the update operation
                    var logger = DI.Container.GetInstance<ILogger>();
                    logger.LogInformation("Master password updated.");
                    return masterPasswordModel;
                }
                else
                {
                    return null;
                }
            }            
        }
    }
}
