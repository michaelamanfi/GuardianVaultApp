using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    public class NormalTreeNodeComparer : IEqualityComparer<TreeNode>
    {
        public bool Equals(TreeNode x, TreeNode y)
        {
            return string.Compare((x.Tag as FolderModel).Path, (y.Tag as FolderModel).Path, StringComparison.OrdinalIgnoreCase) == 0;
        }

        public int GetHashCode(TreeNode obj)
        {
            return (obj.Tag as FolderModel).Path.ToUpperInvariant().GetHashCode();
        }
    }
}