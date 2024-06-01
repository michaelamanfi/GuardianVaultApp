using System.Windows.Forms;

namespace GuardianVault
{
    public interface ITreeViewUIService
    {
        void BuildTree(TreeView treeView);

        void SelectNodeOnMouseUp(TreeView treeView, ContextMenuStrip treeContextMenu, MouseEventArgs e);
    }
}