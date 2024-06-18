using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Represents the view for user settings.
    /// </summary>
    public partial class UserSettingsView : IView<UserSettingsModel>
    {
        /// <summary>
        /// Displays the user settings in a modal dialog and returns the possibly modified settings model.
        /// </summary>
        /// <param name="parent">The parent window for this view, used to center the dialog.</param>
        /// <param name="settingsModel">The initial user settings model to be displayed and potentially modified.</param>
        /// <returns>The updated user settings model after the dialog is closed.</returns>
        public UserSettingsModel ShowView(IWin32Window parent, UserSettingsModel settingsModel)
        {
            using (UserSettingsDlg dlg = new UserSettingsDlg(settingsModel))
            {
                // Show the dialog as a modal window
                dlg.ShowDialog(parent);
            }

            return settingsModel; // Return the model, which may be updated within the dialog
        }
    }
}
