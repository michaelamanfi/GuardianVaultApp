namespace GuardianVault
{
    /// <summary>
    /// Provides methods to encrypt and decrypt files with a password.
    /// </summary>
    public interface IFileEncryptionService
    {
        /// <summary>
        /// Decrypts a file at a specified path using a password and encryption level.
        /// </summary>
        /// <param name="encryptedFilePath">The path to the encrypted file.</param>
        /// <param name="password">The password used for decryption.</param>
        /// <param name="encryptionLevels">The level of encryption used.</param>
        /// <returns>The path to the decrypted file.</returns>
        string DecryptFile(string encryptedFilePath, string password, EncryptionLevels encryptionLevels);

        /// <summary>
        /// Encrypts a file at a specified path using a password and encryption level.
        /// </summary>
        /// <param name="filePath">The path to the file to encrypt.</param>
        /// <param name="password">The password used for encryption.</param>
        /// <param name="encryptionLevels">The level of encryption to apply.</param>
        /// <returns>The path to the encrypted file.</returns>
        string EncryptFile(string filePath, string password, EncryptionLevels encryptionLevels);
    }
}