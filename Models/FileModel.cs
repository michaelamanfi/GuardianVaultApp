using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardianVault
{
    /// <summary>
   /// Represents a file in the file system, including its name, path, file type, size in KB, and modified date.
   /// </summary>
    public class FileModel
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the path of the file.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the file type (extension) of the file.
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// Gets or sets the status of the file.
        /// </summary>
        public string Status { get; set; }
        

        /// <summary>
        /// Gets or sets the size of the file in kilobytes (KB).
        /// </summary>
        public double SizeInKB { get; set; }

        /// <summary>
        /// Gets or sets the last modified date of the file.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
