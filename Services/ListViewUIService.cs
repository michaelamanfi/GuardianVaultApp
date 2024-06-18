using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Provides services to manage ListView UI components, specifically for loading files into a ListView.
    /// </summary>
    public class ListViewUIService : IListViewUIService
    {
        private readonly IFileManagementService fileManagementService;

        /// <summary>
        /// Initializes a new instance of the ListViewUIService class.
        /// </summary>
        /// <param name="fileManagementService">The service used for managing files.</param>
        public ListViewUIService(IFileManagementService fileManagementService)
        {
            this.fileManagementService = fileManagementService;
        }

        /// <summary>
        /// Loads files into a ListView control based on the contents of a specified folder model.
        /// This method specifically loads files with an '.encrypted' extension.
        /// </summary>
        /// <param name="listView">The ListView control to load files into.</param>
        /// <param name="folder">The folder model containing the path where files are located.</param>
        public void LoadFiles(ListView listView, FolderModel folder)
        {
            listView.Tag = folder;
            listView.Items.Clear();
            foreach (var file in Directory.GetFiles(folder.Path, "*.encrypted"))
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

                string newFilePath = fileManagementService.GetOriginalFile(fileModel);

                ListViewItem listViewItem = new ListViewItem(fileModel.Name);
                listViewItem.Tag = fileModel;
                listViewItem.ImageIndex = File.Exists(newFilePath) ? 1 : 0;
                listViewItem.SubItems.Add(fileModel.ModifiedDate.ToString());
                listViewItem.SubItems.Add(fileModel.FileType);
                listViewItem.SubItems.Add(fileModel.Status);
                listView.Items.Add(listViewItem);
            }
        }
        /// <summary>
        /// Retrieves all FileModels for files with their originals.
        /// </summary>
        /// <param name="listView">The ListView containing the items.</param>
        /// <returns>A list of FileModel instances.</returns>
        public List<FileModel> GetDecryptedFiles(ListView listView)
        {
            return RetrieveFileModelsByImageIndex(listView, 1);
        }

        /// <summary>
        /// Retrieves all FileModels from selected ListViewItems with a specific ImageIndex.
        /// </summary>
        /// <param name="listView">The ListView containing the items.</param>
        /// <param name="imageIndex">The ImageIndex to filter by.</param>
        /// <returns>A list of FileModel instances matching the specified ImageIndex.</returns>
        private List<FileModel> RetrieveFileModelsByImageIndex(ListView listView, int imageIndex)
        {
            List<FileModel> fileModels = new List<FileModel>();

            // Iterate over selected items in the ListView
            foreach (ListViewItem item in listView.SelectedItems)
            {
                // Check if the ImageIndex of the item matches the specified value
                if (item.ImageIndex == imageIndex)
                {
                    // Retrieve the FileModel from the Tag property
                    FileModel model = item.Tag as FileModel;
                    if (model != null)
                    {
                        fileModels.Add(model);
                    }
                }
            }

            return fileModels;
        }
    }
}
