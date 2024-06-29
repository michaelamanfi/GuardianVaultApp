using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    internal static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Handle exceptions on Windows Forms threads
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            // Set the unhandled exception mode to force all Windows Forms errors to go through one handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Handle exceptions on non-UI threads
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


            // Run the main form
            Application.Run(DI.Container.GetInstance<AppMainForm>());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var mainForm = DI.Container.GetInstance<AppMainForm>();
            var app = DI.Container.GetInstance<IApplicationController>();
            if (e.Exception as CryptographicException != null)
            {
                DI.Container.GetInstance<ILogger>().LogError($"File encryption/decryption operation failed: {e.Exception.Message}");

                app.ShowAppErrorMessage(mainForm, $"File encryption/decryption operation failed. Please verify that your master password is correct.");
            }
            else
            {
                DI.Container.GetInstance<ILogger>().LogError($"An error has occured: {e.Exception}");

                app.ShowAppErrorMessage(mainForm, $"An error has occured: {e.Exception.Message}");
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Application_ThreadException(sender, new System.Threading.ThreadExceptionEventArgs(e.ExceptionObject as Exception));
        }
    }
}
