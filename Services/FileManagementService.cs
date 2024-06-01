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
    /// Provides methods for managing and retrieving information about file system directories and files.
    /// </summary>
    public class FileManagementService : IFileManagementService
    {   
        // Dictionary mapping common file extensions to their associated file types
        private static readonly Dictionary<string, string> fileExtensionToFileTypeMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { ".txt", "Text File" },
            { ".doc", "Document" },
            { ".docx", "Document" },
            { ".xls", "Spreadsheet" },
            { ".xlsx", "Spreadsheet" },
            { ".ppt", "Presentation" },
            { ".pptx", "Presentation" },
            { ".pdf", "PDF Document" },
            { ".jpg", "Image" },
            { ".jpeg", "Image" },
            { ".png", "Image" },
            { ".gif", "Image" },
            { ".mp3", "Audio" },
            { ".mp4", "Video" },
            { ".avi", "Video" },
            { ".mkv", "Video" },
            { ".zip", "Compressed Archive" },
            { ".rar", "Compressed Archive" },
            { ".html", "Web Page" },
            { ".htm", "Web Page" },
            { ".css", "Cascading Style Sheet" },
            { ".js", "JavaScript File" },
            { ".json", "JSON File" },
            { ".xml", "XML File" }
        };

        /// <summary>
        /// Retrieves the root folder and its contents, including all subfolders and files, as a <see cref="FolderModel"/>.
        /// </summary>
        /// <param name="folderPath">The path of the root folder to retrieve.</param>
        /// <returns>A <see cref="FolderModel"/> representing the root folder and its contents.</returns>
        /// <exception cref="ArgumentException">Thrown when the provided folder path is null, empty, or does not exist.</exception>
        public FolderModel GetRootFolder(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            {
                throw new ArgumentException("The provided folder path is invalid.");
            }

            return GetFolder(folderPath);
        }

        /// <summary>
        /// Recursively retrieves a folder and its contents, including all subfolders and files, as a <see cref="FolderModel"/>.
        /// </summary>
        /// <param name="folderPath">The path of the folder to retrieve.</param>
        /// <returns>A <see cref="FolderModel"/> representing the folder and its contents.</returns>
        private FolderModel GetFolder(string folderPath)
        {
            var folderInfo = new DirectoryInfo(folderPath);
            var folder = new FolderModel
            {
                Name = folderInfo.Name,
                Path = folderPath,
                Type = "Folder",
                ModifiedDate = folderInfo.LastWriteTime,
                Status = folderInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ? "Read-Only" : "Writable",
                Files = new List<FileModel>(),
                Folders = new List<FolderModel>()
            };


            foreach (var subFolder in Directory.GetDirectories(folderPath))
            {
                folder.Folders.Add(GetFolder(subFolder));
            }

            return folder;
        }

        /// <summary>
        /// Gets the file type for the specified file extension.
        /// </summary>
        /// <param name="fileExtension">The file extension to look up.</param>
        /// <returns>The file type for the file extension, or "Unknown" if the extension is not found.</returns>
        public string GetFileTypeForFileExtension(string fileExtension)
        {
            if (fileExtensionToFileTypeMap.TryGetValue(fileExtension, out var fileType))
            {
                return fileType;
            }

            return $"{fileExtension.TrimStart('.').ToUpper()} File";
        }

        /// <summary>
        /// Changes the directory part of the given file path to match the provided directory.
        /// </summary>
        /// <param name="newDirectory">The new directory path.</param>
        /// <param name="filePath">The original file path.</param>
        /// <returns>The file path with the new directory.</returns>
        public string ChangeDirectory(string newDirectory, string filePath)
        {
            if (string.IsNullOrEmpty(newDirectory))
            {
                throw new ArgumentNullException(nameof(newDirectory), "New directory cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "File path cannot be null or empty.");
            }

            // Ensure newDirectory ends with a directory separator
            if (!newDirectory.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                newDirectory += Path.DirectorySeparatorChar;
            }

            // Get the file name from the original file path
            string fileName = Path.GetFileName(filePath);

            // Combine the new directory with the file name to create the new file path
            string newFilePath = Path.Combine(newDirectory, fileName);

            return newFilePath;
        }
    }
}
