using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Controller responsible for managing user settings.
    /// </summary>
    public class UserSettingsController : IUserSettingsController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSettingsController"/> class.
        /// </summary>
        public UserSettingsController()
        {
        }

        /// <summary>
        /// Updates the user settings by displaying a settings view to the user, allowing them to modify settings,
        /// and saving these settings if modified.
        /// </summary>
        /// <param name="parent">The parent window which will host the settings dialog.</param>
        public void UpdateUserSettings()
        {
            IWin32Window parent = DI.Container.GetInstance<IWin32Window>();

            // Retrieves an instance of ISecureStorageService from the DI container to handle data storage.
            ISecureStorageService secureStorageManager = DI.Container.GetInstance<ISecureStorageService>();

            // Retrieves the current UserSettingsModel instance from the DI container.
            UserSettingsModel userSettingsModel = DI.Container.GetInstance<UserSettingsModel>();

            // Retrieves an instance of IView configured for AppSettingsModel from the DI container.
            var view = DI.Container.GetInstance<IView<UserSettingsModel>>();

            // Displays the view to the user and captures the modified settings model.
            var model = view.ShowView(parent, userSettingsModel);

            // If the model is modified and returned, saves the data using the secure storage manager.
            if (model != null)
                secureStorageManager.SaveAppData(model);
        }
    }
}
