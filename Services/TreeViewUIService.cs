using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    public class TreeViewUIService : ITreeViewUIService
    {
        public void BuildTree(TreeView treeView)
        {
            AppSettingsModel _settings = DI.Container.GetInstance<AppSettingsModel>();
            IFileManagementService fileManagementService = DI.Container.GetInstance<IFileManagementService>();

            FolderModel folder =
                fileManagementService.GetRootFolder(_settings.EncryptedFolderPath);

            TreeNode parentNode = new TreeNode();
            BuildTree(parentNode, folder);

            treeView.Nodes.Clear();
            treeView.Nodes.Add(parentNode);
        }

        private void BuildTree(TreeNode parentNode, FolderModel folder)
        {
            // Set the text and tag of the parent node for this folder
            parentNode.Text = folder.Name;
            parentNode.Tag = folder;
            // Image settings for folder
            parentNode.ImageIndex = 0;
            parentNode.SelectedImageIndex = 0;

            // Recursively add each sub-folder as a child node
            foreach (var subFolder in folder.Folders)
            {
                TreeNode folderNode = new TreeNode();
                BuildTree(folderNode, subFolder); // Recursive call
                parentNode.Nodes.Add(folderNode);
            }

            // Add each file as a child node
            foreach (var file in folder.Files)
            {
                TreeNode fileNode = new TreeNode
                {
                    Text = file.Name,
                    Tag = file,
                    ImageIndex = file.Name.Contains(".encrypted") ? 4 : 2,
                    SelectedImageIndex = file.Name.Contains(".encrypted") ? 4 : 2
                };
                parentNode.Nodes.Add(fileNode);
            }
        }

        public void SelectNodeOnMouseUp(TreeView treeView, ContextMenuStrip treeContextMenu, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Get the node at the position of the mouse click.
                TreeNode node = treeView.GetNodeAt(e.X, e.Y);
                if (node != null)
                {
                    // Select the node and show the context menu
                    treeView.SelectedNode = node;
                    treeContextMenu.Show(treeView, e.Location);
                }
            }
        }
    }
}
