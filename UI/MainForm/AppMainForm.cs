using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// The main application form for the Guardian Vault application.
    /// </summary>
    public partial class AppMainForm : Form
    {
        private readonly IFileManagementService fileManagementService;
        private readonly IFileEncryptionService fileEncryptionService;
        private readonly UserSettingsModel userSettingsModel;
        private readonly ITreeViewUIService treeViewUIService;
        private readonly ILogger logger;
        private readonly ISignInController signIn;
        private readonly IApplicationController app;
        private readonly IUserSettingsController appSettings;
        private readonly IListViewUIService listViewUIService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppMainForm"/> class.
        /// </summary>
        /// <param name="fileManagementService">The file management service.</param>
        /// <param name="fileEncryptionService">The file encryption service.</param>
        /// <param name="userSettingsModel">The user settings model.</param>
        /// <param name="treeViewUIService">The tree view UI service.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="signIn">The sign-in controller.</param>
        /// <param name="app">The application controller.</param>
        /// <param name="appSettings">The user settings controller.</param>
        /// <param name="listViewUIService">The list view UI service.</param>
        public AppMainForm(IFileManagementService fileManagementService,
              IFileEncryptionService fileEncryptionService,
              UserSettingsModel userSettingsModel,
              ITreeViewUIService treeViewUIService,
              ILogger logger,
              ISignInController signIn,
              IApplicationController app,
              IUserSettingsController appSettings,
              IListViewUIService listViewUIService)
        {
            this.listViewUIService = listViewUIService;
            this.appSettings = appSettings;
            this.app = app;
            this.logger = logger;
            this.signIn = signIn;
            this.treeViewUIService = treeViewUIService;
            this.fileManagementService = fileManagementService;
            this.fileEncryptionService = fileEncryptionService;
            this.userSettingsModel = userSettingsModel;

            //Intialize the form components
            InitializeComponent();

            //Initialize default properties
            this.treeFiles.ShowLines = true;
            this.decryptedFileMonitor.Enabled = false;
        }

        /// <summary>
        /// Handles the Loading event of the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Form_Loading(object sender, EventArgs e)
        {
            // Add implementation here
        }

        /// <summary>
        /// Handles the Closing event of the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            // Add implementation here
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            // Call the SignIn method to display the sign-in dialog and capture the result.
            var login = signIn.SignIn();

            // Check if the user was authenticated successfully.
            if (login.Authenticated)
            {
                logger.LogInformation($"{login.UserName} authenticated successfuly.");

                // Resolve the application controller and retrieve the master password model
                MasterPasswordModel masterPasswordModel = (DI.Container.GetInstance<IApplicationController>()).GetMasterPassword();

                // Check if the master password has a key; if not, display a warning message and exit the method
                if (!masterPasswordModel.HasKey)
                {
                    this.Close();
                    return;
                }

                // If authenticated, update the window title to reflect the user's login status.
                this.Text = $"Guardian Vault Application - Logged in as {login.UserName}";
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    //Load the folder tree
                    treeViewUIService.BuildTree(this.treeFiles);
                    this.treeFiles.Nodes[0].Expand();
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                logger.LogInformation($"Authentication failed or canceled.");

                // If authentication failed, close the application.
                this.Close();
            }
        }

        private void masterPasswordToolStripButton_Click(object sender, EventArgs e)
        {
            //Update the master password, if necessary.
            app.UpdateMasterPassword();
        }

        private void settingsToolStripButton_Click(object sender, EventArgs e)
        {
            //Update the app settings.
            appSettings.UpdateUserSettings();

        }
        private void EnableDisableFolderMenu(bool value)
        {
            this.addNewFolderToolStripMenuItem.Enabled = value;
            this.addFilesToolStripMenuItem.Enabled = value;
            this.deleteFolderToolStripMenuItem.Enabled = value;
            this.downloadFolderToolStripMenuItem.Enabled = value;
            this.exploreFolderToolStripMenuItem.Enabled = value;
            this.refreshToolStripMenuItem.Enabled = true;
        }
        private void treeContextMenu_Opening(object sender, CancelEventArgs e)
        {
            EnableDisableFolderMenu(false);

            var selectedNode = this.treeFiles.SelectedNode;
            if (selectedNode == null)
                return;

            var selectedItem = this.treeFiles.SelectedNode.Tag;
            if (selectedItem as FolderModel != null)
            {
                EnableDisableFolderMenu(true);
            }
        }

        private void treeFolder_MouseUp(object sender, MouseEventArgs e)
        {
            //Select the node on mouse up
            this.treeViewUIService.SelectNodeOnMouseUp(this.treeFiles, this.treeContextMenu, e);
        }

        private void treeFolder_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var selectedNode = e.Node;
            if (selectedNode.Tag as FolderModel != null)
            {
                listViewUIService.LoadFiles(this.lstFiles, selectedNode.Tag as FolderModel);
            }
        }
        private void EnableDisableFileMenu(bool value)
        {
            this.refreshFilesMenuItem.Enabled = value;
            this.addFilesMenuItem.Enabled = value;
            this.downloadFileMenuItem.Enabled = value;
            this.deleteFileMenuItem.Enabled = value;
            this.openEditFileMenuItem.Enabled = value;
            this.encryptFileToolStripMenuItem.Enabled = value;
            this.exploreFolderFileToolStripMenuItem.Enabled= value;
        }
        private void lstFilesContextMenu_Opening(object sender, CancelEventArgs e)
        {
            EnableDisableFileMenu(false);
            if (this.lstFiles.SelectedItems.Count > 0 && this.lstFiles.SelectedItems[0].Tag as FileModel != null)
            {
                EnableDisableFileMenu(true);
            }
            else if (this.lstFiles.Items.Count > 0)
            {
                EnableDisableFileMenu(false);
                FolderModel folderModel = this.lstFiles.Tag as FolderModel;
                if (folderModel != null)
                {
                    this.addFilesMenuItem.Enabled = true;
                    this.refreshFilesMenuItem.Enabled = true;
                }
            }
            else
            {
                EnableDisableFileMenu(false);
                FolderModel folderModel = this.lstFiles.Tag as FolderModel;
                if (folderModel != null)
                {
                    this.addFilesMenuItem.Enabled = true;
                    this.exploreFolderFileToolStripMenuItem.Enabled = true;
                }
            }

            //Update the menu item label.
            this.deleteFileMenuItem.Text = 
                this.lstFiles.SelectedItems.Count > 1 ? "Delete Files" : "Delete File";
            this.downloadFileMenuItem.Text = 
                this.lstFiles.SelectedItems.Count > 1 ? "Download Files" : "Download File";
            this.encryptFileToolStripMenuItem.Text = 
                this.lstFiles.SelectedItems.Count > 1 ? "Encrypt Files" : "Encrypt File";
        }

        /// <summary>
        /// Handles the Click event of the addFilesMenuItem. This method is responsible for initiating the file
        /// addition and encryption process based on the selected files from an OpenFileDialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An EventArgs that contains no event data.</param>
        private void addFilesMenuItem_Click(object sender, EventArgs e)
        {
            // Resolve the application controller and retrieve the master password model
            MasterPasswordModel masterPasswordModel = (DI.Container.GetInstance<IApplicationController>()).GetMasterPassword();

            // Check if the master password has a key; if not, display a warning message and exit the method
            if (!masterPasswordModel.HasKey)
            {
                MessageBox.Show(this, "No files were encrypted.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Retrieve the folder model from the tag property of lstFiles
            FolderModel folderModel = this.lstFiles.Tag as FolderModel;

            if (folderModel == null)
            {
                this.app.ShowErrorMessage(this, "No folder has been selected.");
                return;
            }

            //Bring up the file selection view.
            var selectFilesView = DI.Container.GetInstance<IView<FileModel[]>>();
            var files = selectFilesView.ShowView(this, null);

            if (files != null)
            {
                try
                {
                    // Set the cursor to the wait cursor to indicate processing
                    this.Cursor = Cursors.WaitCursor;
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
                            this.logger.LogWarning($"File {filePath} is in the encryption folder. Skipping file copy.");

                        if (!fileManagementService.IsFileWritable(filePath))
                        {
                            this.logger.LogWarning($"File {filePath} is in use. Skipping encrypting it.");

                            continue;
                        }

                        // Encrypt the copied file using the provided master password hash and encryption level from user settings
                        fileEncryptionService.EncryptFile(filePath, masterPasswordModel.HashValue, userSettingsModel.EncryptionLevel);
                    }

                    // Reload the list of files in the lstFiles control to reflect the added files
                    listViewUIService.LoadFiles(this.lstFiles, folderModel);
                }
                finally
                {
                    // Reset the cursor to the default cursor after processing is complete
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void downloadFileMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lstFiles.SelectedItems.Count == 0)
            {
                this.app.ShowErrorMessage(this, "No file has been selected. Please select the files you wish to decrypt.");

                return;
            }

            // Retrieve the master password model
            MasterPasswordModel masterPasswordModel = (DI.Container.GetInstance<IApplicationController>()).GetMasterPassword();

            // Check if the master password has a key; if not, display a warning message and exit the method
            if (!masterPasswordModel.HasKey)
            {
                MessageBox.Show(this, "No files were downloaded.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var browseFolderView = DI.Container.GetInstance<IView<string>>();
            var selectedPath = browseFolderView.ShowView(this, "Please select the folder where you wish to put the decrypted files.");

            if (!string.IsNullOrWhiteSpace(selectedPath))
            {
                try
                {
                    // Set the cursor to the wait cursor to indicate processing
                    this.Cursor = Cursors.WaitCursor;

                    foreach (ListViewItem listViewItem in this.lstFiles.SelectedItems)
                    {
                        FileModel fileModel = (FileModel)listViewItem.Tag;

                        string decryptedFile = fileEncryptionService.DecryptFile(fileModel.Path, masterPasswordModel.HashValue, userSettingsModel.EncryptionLevel);

                        File.Copy(decryptedFile, $"{selectedPath}\\" + Path.GetFileName(decryptedFile), true);

                        File.Delete(decryptedFile);
                    }

                    app.OpenFolderInExplorer(this, selectedPath);
                }
                finally
                {
                    // Reset the cursor to the default cursor after processing is complete
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void openEditFileMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lstFiles.SelectedItems.Count == 0)
            {
                this.app.ShowErrorMessage(this, "No file has been selected. Please select the files you wish to decrypt.");

                return;
            }
            // Resolve the application controller and retrieve the master password model
            MasterPasswordModel masterPasswordModel = (DI.Container.GetInstance<IApplicationController>()).GetMasterPassword();

            // Check if the master password has a key; if not, display a warning message and exit the method
            if (!masterPasswordModel.HasKey)
            {
                MessageBox.Show(this, "No files were downloaded.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Set the cursor to the wait cursor to indicate processing
                this.Cursor = Cursors.WaitCursor;

                FileModel fileModel = (FileModel)this.lstFiles.SelectedItems[0].Tag;

                string fileToOpen = fileManagementService.GetOriginalFile(fileModel);

                if (!File.Exists(fileToOpen))
                {
                    string decryptedFile = fileEncryptionService.DecryptFile(fileModel.Path, masterPasswordModel.HashValue, userSettingsModel.EncryptionLevel);

                    var app =
                        DI.Container.GetInstance<IApplicationController>();

                    app.OpenFileInDefaultApplication(this, decryptedFile);
                }
                else
                {
                    app.OpenFileInDefaultApplication(this, fileToOpen);
                }

                this.refreshFilesMenuItem_Click(null, null);
            }
            finally
            {
                // Reset the cursor to the default cursor after processing is complete
                this.Cursor = Cursors.Default;
            }
        }

        private void lstFiles_DoubleClick(object sender, EventArgs e)
        {
            if (this.lstFiles.Items.Count == 0 || this.lstFiles.SelectedItems.Count == 0)
                return;

            this.openEditFileMenuItem_Click(sender, e);
        }

        private void deleteFileMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lstFiles.SelectedItems.Count == 0)
            {
                this.app.ShowErrorMessage(this, "No file has been selected. Please select the files you wish to decrypt.");

                return;
            }

            if (MessageBox.Show(this, "Are you sure you wish to delete the selected files?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                // Set the cursor to the wait cursor to indicate processing
                this.Cursor = Cursors.WaitCursor;

                foreach (ListViewItem listViewItem in this.lstFiles.SelectedItems)
                {
                    FileModel fileModel = (FileModel)listViewItem.Tag;
                    File.Delete(fileModel.Path);
                }

                // Reload the list of files in the lstFiles control to reflect the added files
                listViewUIService.LoadFiles(this.lstFiles, (FolderModel)this.lstFiles.Tag);
            }
            finally
            {
                // Reset the cursor to the default cursor after processing is complete
                this.Cursor = Cursors.Default;
            }
        }

        private void refreshFilesMenuItem_Click(object sender, EventArgs e)
        {
            // Reload the list of files in the lstFiles control to reflect the added files
            listViewUIService.LoadFiles(this.lstFiles, (FolderModel)this.lstFiles.Tag);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                //Load the folder tree
                treeViewUIService.BuildTree(this.treeFiles);
                this.treeFiles.Nodes[0].Expand();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void deleteFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = this.treeFiles.SelectedNode;
            if (selectedNode == null || selectedNode.Tag as FolderModel == null)
            {
                this.app.ShowErrorMessage(this, "No folder has been selected. Please select the folder you wish to delete.");

                return;
            }

            FolderModel folder = (FolderModel)selectedNode.Tag;

            if (folder.IsRoot)
            {
                this.app.ShowErrorMessage(this, "You canont delete the root folder in this application.");
                return;
            }

            if (MessageBox.Show(this, $"Are you sure you wish to delete the selected folder '{folder.Path}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                Directory.Delete(folder.Path, true);

                this.refreshToolStripMenuItem_Click(sender, e);

            }
            catch (Exception ex)
            {
                this.app.ShowErrorMessage(this, "An error occured while attempting to delete the folder.");

                this.logger.LogError("Error deleting a folder: \n" + ex.ToString());
            }
        }

        private static object timerRunning = new object();
        private void decryptedFileMonitor_Tick(object sender, EventArgs e)
        {
            if (!Monitor.TryEnter(timerRunning))
                return;

            try
            {
                // Resolve the application controller and retrieve the master password model
                MasterPasswordModel masterPasswordModel = (DI.Container.GetInstance<IApplicationController>()).GetMasterPassword();

                // Check if the master password has a key
                if (!masterPasswordModel.HasKey)
                    return;

                var files = fileManagementService.GetFilesPendingEncryption(userSettingsModel.EncryptedFolderPath);
                foreach (var file in files)
                {
                    if (fileManagementService.IsFileWritable(file.Path))
                    {
                        //File is writable so encrypt it
                        fileEncryptionService.EncryptFile(file.Path, masterPasswordModel.HashValue, userSettingsModel.EncryptionLevel);

                        //Delete the original file
                        File.Delete(file.Path);
                    }
                }
            }
            finally
            {
                Monitor.Exit(timerRunning);
            }
        }

        private void encryptFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lstFiles.SelectedItems.Count == 0)
            {
                this.app.ShowErrorMessage(this, "No file has been selected. Please select the files you wish to decrypt.");

                return;
            }

            var list = listViewUIService.GetDecryptedFiles(this.lstFiles);
            if (list.Count == 0)
                return;

            if (MessageBox.Show(this, "The original source files will be removed after encryption. Do you wish to continue?", "Confirm Encryption", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {

                // Resolve the application controller and retrieve the master password model
                MasterPasswordModel masterPasswordModel = (DI.Container.GetInstance<IApplicationController>()).GetMasterPassword();

                // Check if the master password has a key
                if (!masterPasswordModel.HasKey)
                    return;

                // Set the cursor to the wait cursor to indicate processing
                this.Cursor = Cursors.WaitCursor;

                foreach (FileModel fileModel in list)
                {
                    string originalFile = fileManagementService.GetOriginalFile(fileModel);

                    if (!File.Exists(originalFile))
                        continue;

                    if (fileManagementService.IsFileWritable(originalFile))
                    {
                        //File is writable so encrypt it
                        fileEncryptionService.EncryptFile(originalFile, masterPasswordModel.HashValue, userSettingsModel.EncryptionLevel);

                        //Delete the original file
                        File.Delete(originalFile);
                    }
                }

                // Reload the list of files in the lstFiles control to reflect the added files
                listViewUIService.LoadFiles(this.lstFiles, (FolderModel)this.lstFiles.Tag);
            }
            finally
            {
                // Reset the cursor to the default cursor after processing is complete
                this.Cursor = Cursors.Default;
            }
        }

        private void exploreFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = this.treeFiles.SelectedNode;
            if (selectedNode == null || selectedNode.Tag as FolderModel == null)
            {
                this.app.ShowErrorMessage(this, "No folder has been selected. Please select the folder you wish to explore.");

                return;
            }

            FolderModel folder = (FolderModel)selectedNode.Tag;
            app.OpenFolderInExplorer(this, folder.Path);
        }

        private void addNewFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = this.treeFiles.SelectedNode;
            if (selectedNode == null || selectedNode.Tag as FolderModel == null)
            {
                this.app.ShowErrorMessage(this, "No folder has been selected. Please select the folder you wish to explore.");

                return;
            }

            FolderModel folder = (FolderModel)selectedNode.Tag;

            var view = DI.Container.GetInstance<IView<FolderModel>>();
            var newFolder = view.ShowView(this, folder);
            if (newFolder != null)
            {
                if (Directory.Exists(newFolder.Path))
                    return;

                try
                {
                    Directory.CreateDirectory(newFolder.Path);

                    //Refresh to folders
                    this.refreshToolStripMenuItem_Click(null, null);
                }
                catch (Exception ex)
                {
                    this.app.ShowErrorMessage(this, "An error occured while attempting to create the folder.");

                    this.logger.LogError("Error creating folder: \n" + ex.ToString());
                }
            }
        }

        private void exploreFolderFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Retrieve the folder model from the tag property of lstFiles
            FolderModel folder = this.lstFiles.Tag as FolderModel;
            if (folder == null)
            {
                this.app.ShowErrorMessage(this, "No folder has been selected.");
                return;
            }

            app.OpenFolderInExplorer(this, folder.Path);
        }
    }
}