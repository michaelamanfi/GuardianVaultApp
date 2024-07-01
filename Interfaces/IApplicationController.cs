using System.Collections.Generic;
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

        /// <summary>
        /// Encrypts an array files and a folder with the given encryption settings and master password.
        /// </summary>
        /// <param name="masterPasswordModel">The master password model containing the encryption key.</param>
        /// <param name="userSettingsModel">User-defined settings that may affect encryption, such as encryption level.</param>
        /// <param name="folderModel">The folder model containing information about the folder to encrypt.</param>
        /// <param name="files">An array of file models representing the files to be encrypted.</param>
        void EncryptFiles(MasterPasswordModel masterPasswordModel,
                                 UserSettingsModel userSettingsModel,
                                 FolderModel folderModel, FileModel[] files);

        /// <summary>
        /// Encrypts a list of files with the given encryption settings and master password.
        /// </summary>
        /// <param name="masterPasswordModel">The master password model containing the encryption key.</param>
        /// <param name="userSettingsModel">User-defined settings that may affect encryption, such as encryption level.</param>
        /// <param name="files">A list of file models representing the files to be encrypted.</param>
        void EncryptFiles(MasterPasswordModel masterPasswordModel,
                                 UserSettingsModel userSettingsModel, List<FileModel> files);

        /// <summary>
        /// Decrypts an array of files to the specified path.
        /// </summary>
        /// <param name="masterPasswordModel">The model containing the master password used for decryption.</param>
        /// <param name="userSettingsModel">User settings that affect decryption processes and configurations.</param>
        /// <param name="files">An array of FileModel which contains the files to be decrypted.</param>
        /// <param name="path">The target path where the decrypted files will be saved.</param>
        void DecryptFiles(MasterPasswordModel masterPasswordModel,
                                 UserSettingsModel userSettingsModel,
                                 FileModel[] files, string path);
    }
}
