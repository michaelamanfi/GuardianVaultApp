using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Provides methods for updating user settings in the application.
    /// </summary>
    public interface IUserSettingsController
    {
        /// <summary>
        /// Updates the user settings.
        /// </summary>
        void UpdateUserSettings();
    }
}