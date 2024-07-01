using System;
using System.Collections;
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
        private bool disableExistConfirmation = false;

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
            this.FormClosing += Form_Closing;
        }

        /// <summary>
        /// Handles the Closing event of the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (disableExistConfirmation)
                return;

            // Ask if the user really wants to exit the application
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.No)
            {
                // If the user clicks "No", cancel the closing operation
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Handles the 'Shown' event of the form. This event is triggered after the form is first displayed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    disableExistConfirmation = true;
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

                disableExistConfirmation = true;
                // If authentication failed, close the application.
                this.Close();
            }
        }

        /// <summary>
        /// This event is triggered when the user clicks the master password button, used to set or change the master password.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void masterPasswordToolStripButton_Click(object sender, EventArgs e)
        {
            //Update the master password, if necessary.
            app.UpdateMasterPassword();
        }

        /// <summary>
        /// This event is triggered when the user clicks the settings button, used to open the user settings dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsToolStripButton_Click(object sender, EventArgs e)
        {
            //Update the app settings.
            appSettings.UpdateUserSettings();

        }
        /// <summary>
        /// Enables or disables the folder-related menu items based on the specified boolean value.
        /// </summary>
        /// <param name="value">Boolean value indicating whether the folder menu should be enabled (true) or disabled (false).</param>
        private void EnableDisableFolderMenu(bool value)
        {

            this.addNewFolderToolStripMenuItem.Enabled = value;
            this.addFilesToolStripMenuItem.Enabled = value;
            this.deleteFolderToolStripMenuItem.Enabled = value;
            this.exploreFolderToolStripMenuItem.Enabled = value;
            this.refreshToolStripMenuItem.Enabled = true;
        }
        /// <summary>
        /// Handles the 'Opening' event of a context menu for a tree view. This event is triggered right before the context menu is displayed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handles the MouseUp event for a tree view control. This method is called when a mouse button is released over the tree view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeFolder_MouseUp(object sender, MouseEventArgs e)
        {
            //Select the node on mouse up
            this.treeViewUIService.SelectNodeOnMouseUp(this.treeFiles, this.treeContextMenu, e);
        }

        /// <summary>
        /// This method is invoked when a node in the tree view is clicked with the mouse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeFolder_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var selectedNode = e.Node;
            if (selectedNode.Tag as FolderModel != null)
            {
                listViewUIService.LoadFiles(this.lstFiles, selectedNode.Tag as FolderModel);
            }
        }
        /// <summary>
        /// Enables or disables file-related menu items based on the specified boolean value.
        /// </summary>
        /// <param name="value">Boolean value indicating whether the file menu should be enabled (true) or disabled (false).</param>
        private void EnableDisableFileMenu(bool value)
        {
            this.refreshFilesMenuItem.Enabled = value;
            this.addFilesMenuItem.Enabled = value;
            this.downloadFileMenuItem.Enabled = value;
            this.deleteFileMenuItem.Enabled = value;
            this.openEditFileMenuItem.Enabled = value;
            this.encryptFileToolStripMenuItem.Enabled = value;
            this.exploreFolderFileToolStripMenuItem.Enabled = value;
        }
        /// <summary>
        /// Handles the 'Opening' event of the context menu for the file list in a ListView, enabling or disabling menu items and adjusting their labels accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">.</param>
        private void lstFilesContextMenu_Opening(object sender, CancelEventArgs e)
        {
            // Initially disable all file-related menu options
            EnableDisableFileMenu(false);
            
            if (this.lstFiles.SelectedItems.Count > 0 && this.lstFiles.SelectedItems[0].Tag as FileModel != null)
            {
                // Enable file-related menu items since a file is selected
                EnableDisableFileMenu(true);
            }
            else if (this.lstFiles.Items.Count > 0)
            {
                EnableDisableFileMenu(false);
                FolderModel folderModel = this.lstFiles.Tag as FolderModel;
                if (folderModel != null)
                {
                    // Enable menu items related to folder operations like adding files or refreshing the file list
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
                    // Enable menu items specific to folder exploration
                    this.addFilesMenuItem.Enabled = true;
                    this.exploreFolderFileToolStripMenuItem.Enabled = true;
                }
            }

            // Update the label of the delete, download, and encrypt menu items based on the number of selected items. This helps provide context-sensitive labels to the user
            this.deleteFileMenuItem.Text = this.lstFiles.SelectedItems.Count > 1 ? "Delete Files" : "Delete File";
            this.downloadFileMenuItem.Text = this.lstFiles.SelectedItems.Count > 1 ? "Download Files" : "Download File";
            this.encryptFileToolStripMenuItem.Text = this.lstFiles.SelectedItems.Count > 1 ? "Encrypt Files" : "Encrypt File";
        }

        /// <summary>
        /// This method is responsible for initiating the file addition and encryption process based on the selected files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                    //Entrypt the files.
                    app.EncryptFiles(masterPasswordModel, 
                        userSettingsModel, 
                        folderModel, 
                        files);

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

        /// <summary>
        /// This function is designed to download and decrypt files selected in a ListView. It checks if files are selected, retrieves a master password, and processes file decryption and copying to a user-selected directory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downloadFileMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lstFiles.SelectedItems.Count == 0)
            {
                this.app.ShowErrorMessage(this, "No file has been selected. Please select the files you wish to decrypt.");
                return;
            }

            // Retrieve the master password
            MasterPasswordModel masterPasswordModel = (DI.Container.GetInstance<IApplicationController>()).GetMasterPassword();

            // Ensure that a master password has been set before proceeding
            if (!masterPasswordModel.HasKey)
            {
                MessageBox.Show(this, "No files were downloaded because a master password is not available.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prompt user to select a folder to save the decrypted files
            var browseFolderView = DI.Container.GetInstance<IView<string>>();
            var selectedPath = browseFolderView.ShowView(this, "Please select the folder where you wish to put the decrypted files.");

            if (!string.IsNullOrWhiteSpace(selectedPath))
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    // Filter and cast selected items to FileModel
                    IEnumerable<ListViewItem> selectedItems = this.lstFiles.SelectedItems.Cast<ListViewItem>().Where(item => item.Tag as FileModel != null);
                    var files = selectedItems.Select(item => item.Tag as FileModel).ToList();

                    //Decrypt selected files.
                    this.app.DecryptFiles(masterPasswordModel, userSettingsModel, files.ToArray(), selectedPath);

                    // Open the destination folder in Windows Explorer
                    app.OpenFolderInExplorer(this, selectedPath);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Handles the click event for the "Open/Edit" menu item. This method attempts to open the selected file for viewing or editing, decrypting it if necessary.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openEditFileMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lstFiles.SelectedItems.Count == 0)
            {
                this.app.ShowErrorMessage(this, "No file has been selected. Please select the file you wish to open.");
                return;
            }

            // Retrieve the master password model to access decryption keys
            MasterPasswordModel masterPasswordModel = DI.Container.GetInstance<IApplicationController>().GetMasterPassword();

            // Verify that a master password key is available
            if (!masterPasswordModel.HasKey)
            {
                MessageBox.Show(this, "Master password is required to decrypt files.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                FileModel fileModel = (FileModel)this.lstFiles.SelectedItems[0].Tag;

                // Determine the path for the file to open
                string fileToOpen = fileManagementService.GetOriginalFile(fileModel);

                // Decrypt and open the file if it does not exist in its original location
                if (!File.Exists(fileToOpen))
                {
                    string decryptedFile = fileEncryptionService.DecryptFile(fileModel.Path, masterPasswordModel.HashValue, userSettingsModel.EncryptionLevel);

                    this.logger.LogWarning($"Your file was decrypted: {decryptedFile}");

                    app.OpenFileInDefaultApplication(this, decryptedFile);
                }
                else
                {
                    app.OpenFileInDefaultApplication(this, fileToOpen);
                }

                // Refresh file listing to reflect changes, if any
                this.refreshFilesMenuItem_Click(null, null);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the double-click event on the file list view, effectively shortcutting the file open/edit action.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstFiles_DoubleClick(object sender, EventArgs e)
        {
            if (this.lstFiles.Items.Count == 0 || this.lstFiles.SelectedItems.Count == 0)
                return;

            // Trigger the open/edit menu item click event
            this.openEditFileMenuItem_Click(sender, e);
        }

        /// <summary>
        /// This method deletes the selected files from the file system after a confirmation from the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteFileMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lstFiles.SelectedItems.Count == 0)
            {
                this.app.ShowErrorMessage(this, "No file has been selected. Please select the files you wish to delete.");
                return;
            }

            // Confirm deletion from the user
            if (MessageBox.Show(this, "Are you sure you wish to delete the selected files?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                foreach (ListViewItem listViewItem in this.lstFiles.SelectedItems)
                {
                    FileModel fileModel = (FileModel)listViewItem.Tag;
                    File.Delete(fileModel.Path);
                }

                // Reload file list to reflect changes
                listViewUIService.LoadFiles(this.lstFiles, (FolderModel)this.lstFiles.Tag);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Reloads the list of files in the lstFiles control to reflect any changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshFilesMenuItem_Click(object sender, EventArgs e)
        {
            listViewUIService.LoadFiles(this.lstFiles, (FolderModel)this.lstFiles.Tag);
        }

        /// <summary>
        /// Reloads the list of folders in the treeFiles control to reflect any changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handles the click event for the "Delete Folder" menu item by attempting to delete the selected folder after user confirmation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = this.treeFiles.SelectedNode;

            if (selectedNode == null || selectedNode.Tag as FolderModel == null)
            {
                // Show error message if no folder is selected
                this.app.ShowErrorMessage(this, "No folder has been selected. Please select the folder you wish to delete.");
                return;
            }

            FolderModel folder = (FolderModel)selectedNode.Tag;

            // Prevent deletion of the root folder
            if (folder.IsRoot)
            {
                this.app.ShowErrorMessage(this, "You cannot delete the root folder in this application.");
                return;
            }

            // Confirm with the user
            if (MessageBox.Show(this, $"Are you sure you wish to delete the selected folder '{folder.Path}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                // Attempt to delete the folder and all its contents
                Directory.Delete(folder.Path, true);

                this.refreshFilesMenuItem_Click(sender, e);
                // Refresh the tree
                this.refreshToolStripMenuItem_Click(sender, e);
            }
            catch (Exception ex)
            {
                this.app.ShowErrorMessage(this, "An error occurred while attempting to delete the folder.");
                // Log the exception details
                this.logger.LogError("Error deleting a folder: \n" + ex.ToString());
            }
        }

        /// <summary>
        /// This handler encrypts selected files after user confirmation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void encryptFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lstFiles.SelectedItems.Count == 0)
            {
                this.app.ShowErrorMessage(this, "No file has been selected. Please select the files you wish to encrypt.");
                return;
            }

            // Get the list of decrypted files from the selected items in the list view
            var list = listViewUIService.GetDecryptedFiles(this.lstFiles);
            if (list.Count == 0) return;

            // Confirm with the user before proceeding with encryption and deletion of original files
            if (MessageBox.Show(this, "The original source files will be removed after encryption. Do you wish to continue?", "Confirm Encryption", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {

                this.Cursor = Cursors.WaitCursor;

                // Retrieve the master password
                MasterPasswordModel masterPasswordModel = DI.Container.GetInstance<IApplicationController>().GetMasterPassword();
                if (!masterPasswordModel.HasKey)
                    return;

                // Encrypt selected files
                this.app.EncryptFiles(masterPasswordModel, userSettingsModel, list);

                // Refresh the file list view to reflect the changes
                listViewUIService.LoadFiles(this.lstFiles, (FolderModel)this.lstFiles.Tag);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the click event for the "Explore Folder" menu item. Opens the selected folder in Windows Explorer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exploreFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = this.treeFiles.SelectedNode;

            if (selectedNode == null || selectedNode.Tag as FolderModel == null)
            {
                // Display error message if no folder is selected
                this.app.ShowErrorMessage(this, "No folder has been selected. Please select the folder you wish to explore.");
                return;
            }

            FolderModel folder = (FolderModel)selectedNode.Tag;

            // Open the selected folder in Windows Explorer
            app.OpenFolderInExplorer(this, folder.Path);
        }

        /// <summary>
        /// Prompts the user to create a new folder within the selected folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNewFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = this.treeFiles.SelectedNode;

            if (selectedNode == null || selectedNode.Tag as FolderModel == null)
            {
                // Display error message if no folder is selected
                this.app.ShowErrorMessage(this, "No folder has been selected. Please select the folder where you wish to add a new folder.");
                return;
            }

            FolderModel folder = (FolderModel)selectedNode.Tag;
            var view = DI.Container.GetInstance<IView<FolderModel>>();

            // Display the new folder dialog to get new folder name
            var newFolder = view.ShowView(this, folder);

            if (newFolder != null)
            {
                if (Directory.Exists(newFolder.Path))
                    return;

                try
                {
                    // Attempt to create the new folder in the file system
                    Directory.CreateDirectory(newFolder.Path);

                    // Refresh the tree view to show the new folder
                    this.refreshToolStripMenuItem_Click(null, null);
                }
                catch (Exception ex)
                {
                    this.app.ShowErrorMessage(this, "An error occurred while attempting to create the folder.");
                    // Log the exception details
                    this.logger.LogError("Error creating folder: \n" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Opens the folder associated with the list view in Windows Explorer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exploreFolderFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Retrieve the FolderModel associated with the ListView
            FolderModel folder = this.lstFiles.Tag as FolderModel;

            if (folder == null)
            {
                // Display error message if no folder is associated with the list view
                this.app.ShowErrorMessage(this, "No folder has been selected. Please select a folder to explore.");
                return;
            }

            // Open the folder in Windows Explorer
            app.OpenFolderInExplorer(this, folder.Path);
        }

        /// <summary>
        /// Opens the help file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            string helpFilePath = System.IO.Path.Combine(Application.StartupPath, @"Documentation\Guardian Vault Application.chm");

            try
            {
                // Attempt to open the help file
                System.Diagnostics.Process.Start(helpFilePath);
            }
            catch
            {
                this.app.ShowErrorMessage(this, "Unable to open the help file.");
            }
        }
    }
}