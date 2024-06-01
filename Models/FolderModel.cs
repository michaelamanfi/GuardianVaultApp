using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardianVault
{ 
    /// <summary>
     /// Represents a folder in the file system
     /// </summary>
    public class FolderModel
    {
        /// <summary>
        /// Gets or sets the name of the folder.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the path of the folder.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the type of the folder.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the last modified date of the folder.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the folder.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the list of files contained in the folder.
        /// </summary>
        public List<FileModel> Files { get; set; }

        /// <summary>
        /// Gets or sets the list of subfolders contained in the folder.
        /// </summary>
        public List<FolderModel> Folders { get; set; }
    }
}
