using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Controller responsible for managing application settings.
    /// </summary>
    public class AppSettingsController : IAppSettingsController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsController"/> class.
        /// </summary>
        public AppSettingsController()
        {
        }

        /// <summary>
        /// Updates the application settings by displaying a settings view to the user, allowing them to modify settings,
        /// and saving these settings if modified.
        /// </summary>
        /// <param name="parent">The parent window which will host the settings dialog.</param>
        public void UpdateAppSettings()
        {
            IWin32Window parent = DI.Container.GetInstance<IWin32Window>();

            // Retrieves an instance of ISecureStorageService from the DI container to handle data storage.
            ISecureStorageService secureStorageManager = DI.Container.GetInstance<ISecureStorageService>();

            // Retrieves the current AppSettingsModel instance from the DI container.
            AppSettingsModel appSettingsModel = DI.Container.GetInstance<AppSettingsModel>();

            // Retrieves an instance of IView configured for AppSettingsModel from the DI container.
            var view = DI.Container.GetInstance<IView<AppSettingsModel>>();

            // Displays the view to the user and captures the modified settings model.
            var model = view.ShowView(parent, appSettingsModel);

            // If the model is modified and returned, saves the data using the secure storage manager.
            if (model != null)
                secureStorageManager.SaveAppData(model);
        }
    }
}
