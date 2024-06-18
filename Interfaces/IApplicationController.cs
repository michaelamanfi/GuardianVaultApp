using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Provides methods for controlling various application-specific operations such as file and folder management and master password handling.
    /// </summary>
    public interface IApplicationController
    {
        /// <summary>
        /// Shows an application error message box.
        /// </summary>
        /// <param name="window">The parent window.</param>
        /// <param name="msg">The message to display.</param>
        void ShowAppErrorMessage(IWin32Window window, string msg);

        /// <summary>
        /// Shows an error message box.
        /// </summary>
        /// <param name="window">The parent window.</param>
        /// <param name="msg">The message to display.</param>
        void ShowErrorMessage(IWin32Window window, string msg);

        /// <summary>
        /// Opens a file with its associated default application.
        /// </summary>
        /// <param name="parent">The parent window from which the operation is initiated.</param>
        /// <param name="filePath">The path of the file to open.</param>
        void OpenFileInDefaultApplication(IWin32Window parent, string filePath);

        /// <summary>
        /// Opens a folder in the system's file explorer.
        /// </summary>
        /// <param name="parent">The parent window from which the operation is initiated.</param>
        /// <param name="folderPath">The path of the folder to open.</param>
        void OpenFolderInExplorer(IWin32Window parent, string folderPath);

        /// <summary>
        /// Initiates the process to update the master password used for the application.
        /// </summary>
        void UpdateMasterPassword();

        /// <summary>
        /// Retrieves the current master password details.
        /// </summary>
        /// <returns>The current MasterPasswordModel containing details of the master password.</returns>
        MasterPasswordModel GetMasterPassword();
    }
}
