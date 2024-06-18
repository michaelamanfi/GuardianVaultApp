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
        /// Retrieves all files in the specified folder path and its subfolders that have an associated '.encrypted' version.
        /// This method only adds the non-encrypted version of each file to the list if both the '.encrypted' and the original file exist.
        /// </summary>
        /// <param name="folderPath">The path of the root folder to start searching from.</param>
        /// <returns>A list of FileModel objects representing the files pending encryption.</returns>
        public List<FileModel> GetFilesPendingEncryption(string folderPath)
        {
            List<FileModel> pendingFiles = new List<FileModel>();
            // Check if the directory exists before attempting to read files
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Specified folder does not exist.");
                return pendingFiles; // Return an empty list if the folder doesn't exist
            }

            // Start the recursive traversal of folders starting from the given path
            TraverseFolders(folderPath, pendingFiles);
            return pendingFiles;
        }

        /// <summary>
        /// Recursively traverses through all subfolders starting from the given folder path, adding files to the list
        /// that have a corresponding '.encrypted' file in the same folder and ensuring both files exist.
        /// </summary>
        /// <param name="folderPath">The path of the folder to traverse.</param>
        /// <param name="pendingFiles">The list where pending files are collected.</param>
        private void TraverseFolders(string folderPath, List<FileModel> pendingFiles)
        {
            // Create a DirectoryInfo object to represent the current directory
            DirectoryInfo directory = new DirectoryInfo(folderPath);
            // Retrieve all files in the current directory (non-recursively)
            FileInfo[] files = directory.GetFiles("*.*", SearchOption.TopDirectoryOnly);

            // Iterate through each file in the current directory
            foreach (FileInfo file in files)
            {
                // Check if the file has a '.encrypted' extension
                if (file.Extension.Equals(".encrypted", StringComparison.OrdinalIgnoreCase))
                {
                    // Construct the path to the corresponding original file
                    string originalFilePath = file.FullName.Substring(0, file.FullName.Length - file.Extension.Length);
                    // Check if the original file exists alongside the encrypted version
                    if (File.Exists(originalFilePath))
                    {
                        // Create a FileModel for the original file and add it to the list
                        FileInfo originalFile = new FileInfo(originalFilePath);
                        pendingFiles.Add(CreateFileModelFromFileInfo(originalFile));
                    }
                }
            }

            // Recursively process each subdirectory in the current directory
            foreach (DirectoryInfo subDir in directory.GetDirectories())
            {
                TraverseFolders(subDir.FullName, pendingFiles);
            }
        }

        /// <summary>
        /// Creates a FileModel object from a FileInfo instance, capturing essential details about the file.
        /// </summary>
        /// <param name="fileInfo">The FileInfo object containing file details.</param>
        /// <returns>A FileModel object populated with details from the FileInfo.</returns>
        private FileModel CreateFileModelFromFileInfo(FileInfo fileInfo)
        {
            // Populate a new FileModel with information from the FileInfo
            return new FileModel
            {
                Name = fileInfo.Name, // The name of the file
                Path = fileInfo.FullName, // Full path of the file
                FileType = fileInfo.Extension, // File extension
                SizeInKB = fileInfo.Length / 1024.0, // File size in kilobytes
                ModifiedDate = fileInfo.LastWriteTime, // Last modified date
                Status = "Pending" // Status of the file (e.g., pending encryption)
            };
        }

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

            return GetFolder(folderPath,true);
        }

        /// <summary>
        /// Recursively retrieves a folder and its contents, including all subfolders and files, as a <see cref="FolderModel"/>.
        /// </summary>
        /// <param name="folderPath">The path of the folder to retrieve.</param>
        /// <returns>A <see cref="FolderModel"/> representing the folder and its contents.</returns>
        private FolderModel GetFolder(string folderPath, bool rootFolder)
        {
            var folderInfo = new DirectoryInfo(folderPath);
            var folder = new FolderModel
            {
                Name = folderInfo.Name,
                Path = folderPath,
                IsRoot = rootFolder,
                Type = "Folder",
                ModifiedDate = folderInfo.LastWriteTime,
                Status = folderInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ? "Read-Only" : "Writable",
                Files = new List<FileModel>(),
                Folders = new List<FolderModel>()
            };


            foreach (var subFolder in Directory.GetDirectories(folderPath))
            {
                folder.Folders.Add(GetFolder(subFolder,false));
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

        /// <summary>
        /// Check if file can be written to.
        /// </summary>
        /// <param name="filePath">File</param>
        /// <returns></returns>
        public bool IsFileWritable(string filePath)
        {
            FileStream stream = null;

            // Check if the file exists since trying to open a non-existent file will also throw an exception.
            if (!File.Exists(filePath))
            {
                return false;
            }

            try
            {
                // Attempt to open the file exclusively for writing.
                stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                return true; // The file is not in use and is writable.
            }
            catch (IOException)
            {
                return false;
            }
            finally
            {
                // Ensure that the file stream is closed if it was opened successfully.
                if (stream != null)
                    stream.Close();
            }
        }
        /// <summary>
        /// Returns the original file name of a presumed encrypted file.
        /// </summary>
        /// <param name="fileModel">The encrypted file.</param>
        /// <returns>The orginal file name.</returns>
        public string GetOriginalFile(FileModel fileModel)
        {
            string directory = Path.GetDirectoryName(fileModel.Path);
            string filenameWithoutExtension = Path.GetFileNameWithoutExtension(fileModel.Path);

            string newFilePath = Path.Combine(directory, filenameWithoutExtension);
            return newFilePath;
        }
        /// <summary>
        /// Provides validation suitable for ensuring a folder name can be used in Windows 11 and 10.
        /// </summary>
        /// /// <param name="parentFolder">The parent folder.</param>
        /// <param name="folderName">The name of the folder to validate</param>
        /// <returns></returns>
        public bool IsValidFolderName(FolderModel parentFolder, string folderName)
        {
            if(parentFolder == null)
                throw new ArgumentNullException(nameof(parentFolder));

            // Folder name should not be empty or consist only of white space.
            if (string.IsNullOrWhiteSpace(folderName))
            {
                return false;
            }
            // List of invalid characters for file/folder names on Windows.
            char[] invalidChars = Path.GetInvalidFileNameChars();

            // Check for invalid characters.
            if (folderName.IndexOfAny(invalidChars) >= 0)
            {
                return false;
            }

            // Reserved names that are not allowed for files or folders.
            // Source: https://learn.microsoft.com/en-us/windows/win32/fileio/naming-a-file
            string[] reservedNames = new string[]
            {
            "CON", "PRN", "AUX", "NUL",
            "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
            "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
            };

            // Check against reserved names, ignoring case and without any extensions.
            string folderNameStripped = Path.GetFileNameWithoutExtension(folderName);
            foreach (string reserved in reservedNames)
            {
                if (string.Equals(folderNameStripped, reserved, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            // Ensure the folder name is not too long (generally the max path length is 260 characters, adjust as necessary).
            string newFolderPath = Path.Combine(parentFolder.Path, folderName);
            if (newFolderPath.Length > 255) // Allow some room for path prefixes
            {
                return false;
            }

            return true;
        }
    }
}
