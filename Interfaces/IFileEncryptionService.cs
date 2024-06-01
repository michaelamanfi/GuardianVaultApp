namespace GuardianVault
{
    public interface IFileEncryptionService
    {
        string DecryptFile(string encryptedFilePath, string password);
        string EncryptFile(string filePath, string password);
    }
}