using SimpleInjector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Responsible for setting up and managing dependency injection using Simple Injector.
    /// This class centralizes the configuration of services and their dependencies.
    /// </summary>
    public class DI
    {
        private static Container _container;

        /// <summary>
        /// Configures services for the application's dependency injection container.
        /// Each service is registered with a singleton lifestyle ensuring a single instance throughout the application.
        /// </summary>
        /// <param name="container">The dependency injection container to configure.</param>
        private static void ConfigureServices(Container container)
        {
            // Register the ApplicationModel with a factory delegate
            container.Register(typeof(AppSettingsModel), () =>
            {
                ISecureStorageService secureStorageManager = new SecureStorageService();

                AppSettingsModel applicationModel;
                // Conditionally creating or retrieving the ApplicationModel based on persisted data
                if (secureStorageManager.DoesAppDataExist())
                {
                    applicationModel = secureStorageManager.RetrieveAppData();
                }
                else
                {
                    applicationModel = new AppSettingsModel
                    {
                        EncryptedFolderPath = $"{Application.StartupPath}\\MyFiles"
                    };

                    //Create the root folder if it doesn't already exist.
                    if (!Directory.Exists(applicationModel.EncryptedFolderPath))
                        Directory.CreateDirectory(applicationModel.EncryptedFolderPath);

                    secureStorageManager.SaveAppData(applicationModel);
                }
                return applicationModel;
            });

            //Register the Main Application Form
            container.Register<AppMainForm, AppMainForm>(Lifestyle.Singleton);
            container.Register<IWin32Window, AppMainForm>(Lifestyle.Singleton);

            //Controllers
            container.Register<ISignInController, SignInController>(Lifestyle.Singleton);
            container.Register<IAppSettingsController, AppSettingsController>(Lifestyle.Singleton);
            container.Register<IApplicationController, ApplicationController>(Lifestyle.Singleton);
            

            //Services
            container.Register<ISecureStorageService, SecureStorageService>(Lifestyle.Singleton);
            container.Register<IAuthenticationService, AuthenticationService>(Lifestyle.Singleton);
            container.Register<IFileManagementService, FileManagementService>(Lifestyle.Singleton);
            container.Register<IFileEncryptionService, FileEncryptionService>(Lifestyle.Singleton);
            container.Register<IEncryptionService, EncryptionService>(Lifestyle.Singleton);
            container.Register<IListViewUIService, ListViewUIService>(Lifestyle.Singleton);

            //Views
            container.Register<IView<AppSettingsModel>, AppSettingsView>(Lifestyle.Singleton);
            container.Register<IView<SignInModel>, SignInView>(Lifestyle.Singleton);
            container.Register<IView<MasterPasswordModel>, MasterPasswordView>(Lifestyle.Singleton);
            container.Register<ITreeViewUIService, TreeViewUIService>(Lifestyle.Singleton);
                      
        }

        /// <summary>
        /// Provides a globally accessible Simple Injector container.
        /// Utilizes lazy initialization to ensure the container is created only when needed.
        /// </summary>
        public static Container Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new Container();
                    ConfigureServices(_container);
                }
                return _container;
            }
        }
    }
}
