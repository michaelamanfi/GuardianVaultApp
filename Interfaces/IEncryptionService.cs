namespace GuardianVault
{
    /// <summary>
    /// Provides methods to encrypt and decrypt data with a password.
    /// </summary>
    public interface IEncryptionService
    {
        /// <summary>
        /// Decrypts data using a specified password and encryption level.
        /// </summary>
        /// <param name="data">The encrypted data as a byte array.</param>
        /// <param name="password">The password used for decryption.</param>
        /// <param name="encryptionLevels">The level of encryption used.</param>
        /// <returns>The decrypted data as a byte array.</returns>
        byte[] DecryptWithPassword(byte[] data, string password, EncryptionLevels encryptionLevels);

        /// <summary>
        /// Encrypts data using a specified password and encryption level.
        /// </summary>
        /// <param name="data">The plain data as a byte array.</param>
        /// <param name="password">The password used for encryption.</param>
        /// <param name="encryptionLevels">The level of encryption to apply.</param>
        /// <returns>The encrypted data as a byte array.</returns>
        byte[] EncryptWithPassword(byte[] data, string password, EncryptionLevels encryptionLevels);
    }
}