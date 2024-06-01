namespace GuardianVault
{
    public interface IFileManagementService
    {
        string GetFileTypeForFileExtension(string fileExtension);
        string ChangeDirectory(string newDirectory, string filePath);
        FolderModel GetRootFolder(string folderPath);
    }
}