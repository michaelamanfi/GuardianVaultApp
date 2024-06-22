using System.Collections.Generic;
using System.IO;

namespace GuardianVault
{
    /// <summary>
    /// Provides methods for managing files within the system.
    /// </summary>
    public interface IFileManagementService
    {
        /// <summary>
        /// Provides validation suitable for ensuring a folder name can be used in Windows 11 and 10.
        /// </summary>
        /// <param name="parentFolder">The parent folder.</param>
        /// <param name="folderName">The name of the folder to validate</param>
        /// <returns></returns>
        bool IsValidFolderName(FolderModel parentFolder, string folderName);

        /// <summary>
        /// Checks if a file at a specified path is writable.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <returns>True if the file is writable; otherwise, false.</returns>
        bool IsFileWritable(string filePath);

        /// <summary>
        /// Retrieves a list of files pending encryption within a specified folder.
        /// </summary>
        /// <param name="folderPath">The path to the folder.</param>
        /// <returns>A list of files that are pending encryption.</returns>
        List<FileModel> GetFilesPendingEncryption(string folderPath);

        /// <summary>
        /// Determines the file type based on a file extension.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>The type of the file as a string.</returns>
        string GetFileTypeForFileExtension(string fileExtension);

        /// <summary>
        /// Changes the current working directory for a file to a new directory.
        /// </summary>
        /// <param name="newDirectory">The new directory path.</param>
        /// <param name="filePath">The file path to change the directory for.</param>
        /// <returns>The updated file path after changing the directory.</returns>
        string ChangeDirectory(string newDirectory, string filePath);

        /// <summary>
        /// Retrieves the root folder model for a given folder path.
        /// </summary>
        /// <param name="folderPath">The path to the folder.</param>
        /// <returns>The root folder model.</returns>
        FolderModel GetRootFolder(string folderPath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileModel"></param>
        /// <returns></returns>
        string GetOriginalFile(FileModel fileModel);
    }
}