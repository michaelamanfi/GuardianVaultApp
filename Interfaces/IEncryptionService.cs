namespace GuardianVault
{
    public interface IEncryptionService
    {
        byte[] DecryptWithPassword(byte[] data, string password);
        string DecryptWithPassword(string encryptedText, string password);
        byte[] EncryptWithPassword(byte[] data, string password);
        string EncryptWithPassword(string plainText, string password);
    }
}