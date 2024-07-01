using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GuardianVault
{   
    /// <summary>
     /// Implements methods for controlling various application-specific operations such as file and folder management and master password handling.
     /// </summary>
    public class ApplicationController : IApplicationController
    {
        private readonly ISecureStorageService _storageService;
        private readonly IFileManagementService _fileManagementService;
        private readonly ILogger _logger;
        private readonly IFileEncryptionService _fileEncryptionService;
        public ApplicationController(ILogger logger, 
            IFileManagementService fileManagementService,
            IFileEncryptionService fileEncryptionService,
            ISecureStorageService secureStorageService)
        {
            this._logger = logger;
            this._fileManagementService = fileManagementService;
            this._fileEncryptionService = fileEncryptionService;
            this._storageService = secureStorageService;
        }
        /// <summary>
        /// Shows an error message box.
        /// </summary>
        /// <param name="window">The parent window.</param>
        /// <param name="msg">The message to display.</param>
        public void ShowAppErrorMessage(IWin32Window window, string msg)
        {
            MessageBox.Show(window, $"Error: {msg}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// Shows an error message box.
        /// </summary>
        /// <param name="window">The parent window.</param>
        /// <param name="msg">The message to display.</param>
        public void ShowErrorMessage(IWin32Window window, string msg)
        {
            MessageBox.Show(window, $"Error: {msg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// Retrieves the current master password details.
        /// </summary>
        /// <returns>The current MasterPasswordModel containing details of the master password.</returns>
        public MasterPasswordModel GetMasterPassword()
        {
            var userSettings = DI.Container.GetInstance<UserSettingsModel>();
            if (userSettings.PasswordModel == null)
                userSettings.PasswordModel = new MasterPasswordModel();

            if (userSettings.PasswordModel.HasKey)
                return userSettings.PasswordModel;

            UpdateMasterPassword();
            return userSettings.PasswordModel;
        }

        /// <summary>
        /// Initiates the process to update the master password used for the application.
        /// </summary>
        public void UpdateMasterPassword()
        {
            // Retrieves an instance of IView configured for SignInModel from the DI container.
            var view = DI.Container.GetInstance<IView<MasterPasswordModel>>();

            IWin32Window parent = DI.Container.GetInstance<IWin32Window>();

            var userSettings = DI.Container.GetInstance<UserSettingsModel>();
            if (userSettings.PasswordModel == null)
                userSettings.PasswordModel = new MasterPasswordModel();

            // Displays the view to the user and captures the modified settings model.
            var model= view.ShowView(parent, userSettings.PasswordModel);

            if (model != null)
            {
                //Save change
                this._storageService.SaveAppData(userSettings);
            }
        }

        /// <summary>
        /// Opens a file with its associated default application.
        /// </summary>
        /// <param name="parent">The parent window from which the operation is initiated.</param>
        /// <param name="filePath">The path of the file to open.</param>
        public void OpenFileInDefaultApplication(IWin32Window parent, string filePath)
        {
            // Ensure the file exists before trying to open it
            if (File.Exists(filePath))
            {
                try
                {
                    // Start the associated application for the file
                    Process shProcess = Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });

                    if (shProcess == null)
                    {
                        this.ShowErrorMessage(parent, "Failed to open file.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions, such as no application associated with the file type                    
                    this.ShowErrorMessage(parent, "Error opening file: " + ex.Message);
                }
            }
            else
            {
                this.ShowErrorMessage(parent, "The specified file does not exist.");
            }
        }

        /// <summary>
        /// Opens a folder in the system's file explorer.
        /// </summary>
        /// <param name="parent">The parent window from which the operation is initiated.</param>
        /// <param name="folderPath">The path of the folder to open.</param>
        public void OpenFolderInExplorer(IWin32Window parent, string folderPath)
        {
            // Ensure the folder path exists before trying to open it
            if (System.IO.Directory.Exists(folderPath))
            {
                // Start a new process for the Windows File Explorer
                Process.Start("explorer.exe", folderPath);
            }
            else
            {
                this.ShowErrorMessage(parent, "The specified folder does not exist.");
            }
        }

        public void EncryptFiles(MasterPasswordModel masterPasswordModel,
            UserSettingsModel userSettingsModel,
            FolderModel folderModel,FileModel[] files)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Initiating encryption operation...");
            string[] fileNames = files.Select(fm => fm.Path).ToArray();

            // Iterate through all selected files
            foreach (string fileName in fileNames)
            {
                // Construct the target file path in the current folder model
                string filePath = $"{folderModel.Path}\\{Path.GetFileName(fileName)}";

                if (string.Compare(filePath.ToLower(), fileName, true) != 0)
                    // Copy the file to the target location with overwrite permission
                    File.Copy(fileName, filePath, true);
                else
                    this._logger.LogWarning($"File {filePath} is in the encryption folder. Skipping file copy.");

                if (!this._fileManagementService.IsFileWritable(filePath))
                {
                    string warning = $"File {filePath} is in use. Skipping encrypting it.";
                    this._logger.LogWarning(warning);

                    stringBuilder.AppendLine(warning);

                    continue;
                }

                // Encrypt the copied file using the provided master password hash and encryption level from user settings
                _fileEncryptionService.EncryptFile(filePath, masterPasswordModel.HashValue, userSettingsModel.EncryptionLevel);

                stringBuilder.AppendLine($"File Encrypted: {filePath}");

                // Delete the original file after successful encryption
                File.Delete(filePath);
            }

            this._logger.LogInformation(stringBuilder.ToString());
        }

        public void EncryptFiles(MasterPasswordModel masterPasswordModel,
            UserSettingsModel userSettingsModel,List<FileModel> files)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Initiating encryption operation...");

            // Process each file for encryption
            foreach (FileModel fileModel in files)
            {
                string originalFile = _fileManagementService.GetOriginalFile(fileModel);
                if (!File.Exists(originalFile)) continue;

                if (_fileManagementService.IsFileWritable(originalFile))
                {
                    // Encrypt the file using the master password hash and current encryption level settings
                    _fileEncryptionService.EncryptFile(originalFile, masterPasswordModel.HashValue, userSettingsModel.EncryptionLevel);

                    stringBuilder.AppendLine($"File Encrypted: {originalFile}");
                    // Delete the original file after successful encryption
                    File.Delete(originalFile);
                }
                else
                {
                    string warning = $"File {originalFile} is in use. Skipping encrypting it.";
                    this._logger.LogWarning(warning);

                    stringBuilder.AppendLine(warning);
                }
            }
            this._logger.LogInformation(stringBuilder.ToString());
        }

        public void DecryptFiles(MasterPasswordModel masterPasswordModel,
            UserSettingsModel userSettingsModel,
            FileModel[] files,
            string selectedPath)
        {
            // Decrypt and copy each selected file to the chosen path
            foreach (FileModel fileModel in files)
            {
                // Decrypt the file and obtain the path of the decrypted file
                string decryptedFile = _fileEncryptionService.DecryptFile(fileModel.Path, masterPasswordModel.HashValue, userSettingsModel.EncryptionLevel);

                this._logger.LogWarning($"Your file was decrypted: {decryptedFile}");

                // Copy the decrypted file to the selected path
                File.Copy(decryptedFile, Path.Combine(selectedPath, Path.GetFileName(decryptedFile)), true);

                // Delete the decrypted file after copying if it's a temporary file
                File.Delete(decryptedFile);
            }
        }
    }
}