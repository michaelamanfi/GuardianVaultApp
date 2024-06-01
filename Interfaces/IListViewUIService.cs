using System.Windows.Forms;

namespace GuardianVault
{
    public interface IListViewUIService
    {
        void LoadFiles(ListView listView, FolderModel folder);
    }
}