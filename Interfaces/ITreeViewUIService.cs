using System.Collections.Generic;
using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Provides methods to build and interact with a TreeView UI component.
    /// </summary>
    public interface ITreeViewUIService
    {
        /// <summary>
        /// Builds a tree structure in a TreeView control.
        /// </summary>
        /// <param name="treeView">The TreeView to build.</param>
        void BuildTree(TreeView treeView);

        /// <summary>
        /// Handles node selection based on a mouse-up event within a TreeView.
        /// </summary>
        /// <param name="treeView">The TreeView containing the node.</param>
        /// <param name="treeContextMenu">The context menu to show.</param>
        /// <param name="e">Mouse event arguments.</param>
        void SelectNodeOnMouseUp(TreeView treeView, ContextMenuStrip treeContextMenu, MouseEventArgs e);

        /// <summary>
        /// Retrieves a list of all expanded nodes within a TreeView.
        /// </summary>
        /// <param name="treeView">The TreeView from which to collect expanded nodes.</param>
        /// <returns>A list containing all expanded TreeNode objects from the TreeView.</returns>
        List<TreeNode> GetExpandedNodes(TreeView treeView);

        /// <summary>
        /// Expands all TreeNodes in a TreeView that are specified in a list.
        /// </summary>
        /// <param name="treeView">The TreeView in which nodes will be expanded.</param>
        /// <param name="nodesToExpand">A list of TreeNode objects to be expanded in the TreeView.</param>
        void ExpandNodes(TreeView treeView, List<TreeNode> nodesToExpand);
    }
}