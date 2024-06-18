using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Provides services to manage a TreeView UI component, specifically for building and interacting with the tree structure that displays files and folders.
    /// </summary>
    public class TreeViewUIService : ITreeViewUIService
    {
        private readonly NormalTreeNodeComparer normalTreeNodeComparer;
        private readonly UserSettingsModel _settings;
        private readonly IFileManagementService fileManagementService;

        /// <summary>
        /// Initializes a new instance of the TreeViewUIService class.
        /// </summary>
        /// <param name="fileManagementService">The service used to manage files and folders.</param>
        /// <param name="_settings">The user settings that determine the root folder to be displayed.</param>
        public TreeViewUIService(IFileManagementService fileManagementService, UserSettingsModel _settings)
        {
            this._settings = _settings;
            this.fileManagementService = fileManagementService;
            this.normalTreeNodeComparer = new NormalTreeNodeComparer();
        }

        /// <summary>
        /// Builds the tree structure for a TreeView control using the encrypted folder path specified in user settings.
        /// </summary>
        /// <param name="treeView">The TreeView control to build the tree in.</param>
        public void BuildTree(TreeView treeView)
        {
            var expandedNodes = this.GetExpandedNodes(treeView);
            try
            {
                FolderModel folder = fileManagementService.GetRootFolder(_settings.EncryptedFolderPath);


                TreeNode parentNode = new TreeNode();
                BuildTree(parentNode, folder);

                treeView.Nodes.Clear();
                treeView.Nodes.Add(parentNode);
            }
            finally
            {
                this.ExpandNodes(treeView, expandedNodes);
            }
        }

        /// <summary>
        /// Recursive method to build the tree structure, adding folders and files as nodes.
        /// </summary>
        /// <param name="parentNode">The parent node to add child nodes to.</param>
        /// <param name="folder">The folder model containing subfolders and files.</param>
        private void BuildTree(TreeNode parentNode, FolderModel folder)
        {
            parentNode.Text = folder.Name;
            parentNode.Tag = folder;
            parentNode.ImageIndex = 0;
            parentNode.SelectedImageIndex = 0;

            foreach (var subFolder in folder.Folders)
            {
                TreeNode folderNode = new TreeNode();
                BuildTree(folderNode, subFolder); // Recursive call
                parentNode.Nodes.Add(folderNode);
            }

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

        /// <summary>
        /// Handles the mouse-up event on the TreeView, allowing for right-click context menu operations on nodes.
        /// </summary>
        /// <param name="treeView">The TreeView control where the event occurs.</param>
        /// <param name="treeContextMenu">The context menu to display on right-click.</param>
        /// <param name="e">Mouse event arguments.</param>
        public void SelectNodeOnMouseUp(TreeView treeView, ContextMenuStrip treeContextMenu, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = treeView.GetNodeAt(e.X, e.Y);
                if (node != null)
                {
                    treeView.SelectedNode = node;
                    treeContextMenu.Show(treeView, e.Location);
                }
            }
        }
        /// <summary>
        /// Retrieves a list of all expanded nodes within a TreeView.
        /// </summary>
        /// <param name="treeView">The TreeView from which to collect expanded nodes.</param>
        /// <returns>A list containing all expanded TreeNode objects from the TreeView.</returns>
        public List<TreeNode> GetExpandedNodes(TreeView treeView)
        {
            List<TreeNode> expandedNodes = new List<TreeNode>();

            foreach (TreeNode node in treeView.Nodes)
            {
                GetExpandedNodes(node, expandedNodes);
            }

            return expandedNodes;
        }

        /// <summary>
        /// Expands all TreeNodes in a TreeView that are specified in a list.
        /// </summary>
        /// <param name="treeView">The TreeView in which nodes will be expanded.</param>
        /// <param name="nodesToExpand">A list of TreeNode objects to be expanded in the TreeView.</param>
        public void ExpandNodes(TreeView treeView, List<TreeNode> nodesToExpand)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                ExpandNodes(node, nodesToExpand);
            }
        }

        /// <summary>
        /// Recursive helper method to expand nodes in a TreeView that are found in a specified list.
        /// </summary>
        /// <param name="node">The current TreeNode being checked and potentially expanded.</param>
        /// <param name="nodesToExpand">A list of TreeNode objects that should be expanded if found.</param>
        private void ExpandNodes(TreeNode node, List<TreeNode> nodesToExpand)
        {
            if (nodesToExpand.Contains(node, this.normalTreeNodeComparer))
            {
                node.Expand();
            }

            foreach (TreeNode childNode in node.Nodes)
            {
                ExpandNodes(childNode, nodesToExpand);
            }
        }

        /// <summary>
        /// Recursive helper method to collect all expanded nodes within a TreeView.
        /// </summary>
        /// <param name="node">The TreeNode to check if expanded, and collect if so.</param>
        /// <param name="expandedNodes">A list to store TreeNode objects that are expanded.</param>
        private void GetExpandedNodes(TreeNode node, List<TreeNode> expandedNodes)
        {
            if (node.IsExpanded)
            {
                expandedNodes.Add(node);
            }

            foreach (TreeNode childNode in node.Nodes)
            {
                GetExpandedNodes(childNode, expandedNodes);
            }
        }

    }
}
