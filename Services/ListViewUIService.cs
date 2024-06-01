using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    public class ListViewUIService : IListViewUIService
    {
        public void LoadFiles(ListView listView, FolderModel folder)
        {
            listView.Tag = folder;
            listView.Items.Clear();
            IFileManagementService fileManagementService = DI.Container.GetInstance<IFileManagementService>();
            foreach (var file in Directory.GetFiles(folder.Path))
            {
                var fileInfo = new FileInfo(file);
                var fileModel = new FileModel
                {
                    Name = fileInfo.Name,
                    Path = fileInfo.FullName,
                    FileType = fileManagementService.GetFileTypeForFileExtension(fileInfo.Extension),
                    ModifiedDate = fileInfo.LastWriteTime,
                    Status = File.GetAttributes(file).HasFlag(FileAttributes.ReadOnly) ? "Read-Only" : "Writable"
                };

                ListViewItem listViewItem = new ListViewItem(fileModel.Name);
                listViewItem.Tag = fileModel;
                listViewItem.SubItems.Add(fileModel.ModifiedDate.ToString());
                listViewItem.SubItems.Add(fileModel.FileType);
                listViewItem.SubItems.Add(fileModel.Status);
                listView.Items.Add(listViewItem);
            }
        }
    }
}
