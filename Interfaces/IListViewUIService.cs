using System.Collections.Generic;
using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Provides methods to interact with a ListView UI component to display file information.
    /// </summary>
    public interface IListViewUIService
    {
        /// <summary>
        /// Retrieves all FileModels for files with their originals.
        /// </summary>
        /// <param name="listView">The ListView containing the items.</param>
        /// <returns>A list of FileModel instances.</returns>
        List<FileModel> GetDecryptedFiles(ListView listView);

        /// <summary>
        /// Loads files into a ListView from a specified FolderModel.
        /// </summary>
        /// <param name="listView">The ListView control to load files into.</param>
        /// <param name="folder">The FolderModel containing file data to load.</param>
        void LoadFiles(ListView listView, FolderModel folder);
    }
}