using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    public partial class AppMainForm : Form
    {
        public AppMainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            // Obtain an instance of ISignInController from the dependency injection container.
            var signIn = DI.Container.GetInstance<ISignInController>();

            // Call the SignIn method to display the sign-in dialog and capture the result.
            var login = signIn.SignIn();

            // Check if the user was authenticated successfully.
            if (login.Authenticated)
            {
                // If authenticated, update the window title to reflect the user's login status.
                this.Text = $"Guardian Vault Application - Logged in as {login.UserName}";
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    //Load the folder tree
                    var treeViewUIService = DI.Container.GetInstance<ITreeViewUIService>();
                    treeViewUIService.BuildTree(this.treeFolder);
                    this.treeFolder.Nodes[0].Expand();
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                // If authentication failed, close the application.
                this.Close();
            }
        }

        private void masterPasswordToolStripButton_Click(object sender, EventArgs e)
        {
            // Obtain an instance of ISignInController from the dependency injection container.
            var app = DI.Container.GetInstance<IApplicationController>();

            //Update the master password, if necessary.
            app.UpdateMasterPassword();
        }

        private void settingsToolStripButton_Click(object sender, EventArgs e)
        {
            // Obtain an instance of ISignInController from the dependency injection container.
            var appSettings = DI.Container.GetInstance<IAppSettingsController>();

            //Update the app settings.
            appSettings.UpdateAppSettings();

        }

        private void treeContextMenu_Opening(object sender, CancelEventArgs e)
        {
            this.addNewFolderToolStripMenuItem.Enabled = false;
            this.addFilesToolStripMenuItem.Enabled = false;
            this.downloadFileToolStripMenuItem.Enabled = false;
            this.deleteFileToolStripMenuItem.Enabled = false;
            this.deleteFolderToolStripMenuItem.Enabled = false; 
            this.downloadFolderToolStripMenuItem.Enabled = false;   
            this.refreshToolStripMenuItem.Enabled = false;
            this.openToolStripMenuItem.Enabled = false;

            var selectedNode = this.treeFolder.SelectedNode;
            if (selectedNode == null)
                return;
            
            var selectedItem = this.treeFolder.SelectedNode.Tag;
            if(selectedItem as FolderModel != null)
            {

            }
        }

        private void treeFolder_MouseUp(object sender, MouseEventArgs e)
        {
            var treeService = DI.Container.GetInstance<ITreeViewUIService>();

            //Select the node on mouse up
            treeService.SelectNodeOnMouseUp(this.treeFolder, this.treeContextMenu, e);
        }

        private void treeFolder_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var selectedNode = e.Node;
            if(selectedNode.Tag as FolderModel != null)
            {
                IListViewUIService listViewUIService = DI.Container.GetInstance<IListViewUIService>();
                listViewUIService.LoadFiles(this.lstFiles, selectedNode.Tag as FolderModel);
            }
        }
    }
}
