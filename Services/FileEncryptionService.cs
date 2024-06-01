using System;
using System.IO;
using System.Text;

namespace GuardianVault
{
    public class FileEncryptionService : IFileEncryptionService
    {
        private readonly IEncryptionService _encryptionService;
        public FileEncryptionService(IEncryptionService encryptionService)
        {
            this._encryptionService = encryptionService;
        }
        /// <summary>
        /// Reads an unencrypted file, encrypts its contents, and saves the encrypted data
        /// to a new file with the same name but with a ".encrypted" extension.
        /// </summary>
        /// <param name="filePath">The path of the file to encrypt.</param>
        /// <param name="password">The password used to generate the encryption key.</param>
        public string EncryptFile(string filePath, string password)
        {
            // Check if the file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified file does not exist.");
            }

            // Read the file contents as bytes
            byte[] fileContents = File.ReadAllBytes(filePath);

            // Encrypt the file contents
            byte[] encryptedContents = this._encryptionService.EncryptWithPassword(fileContents, password);

            // Create the encrypted file path
            string encryptedFilePath = filePath + ".encrypted";

            // Save the encrypted contents to the new file
            File.WriteAllBytes(encryptedFilePath, encryptedContents);

            Console.WriteLine($"File encrypted and saved as: {encryptedFilePath}");
            return encryptedFilePath;
        }

        /// <summary>
        /// Reads an encrypted file, decrypts its contents, and saves the decrypted data
        /// to a new file with the original file name before encryption.
        /// </summary>
        /// <param name="encryptedFilePath">The path of the encrypted file.</param>
        /// <param name="password">The password used to generate the decryption key.</param>
        public string DecryptFile(string encryptedFilePath, string password)
        {
            // Check if the file exists
            if (!File.Exists(encryptedFilePath))
            {
                throw new FileNotFoundException("The specified encrypted file does not exist.");
            }

            // Read the encrypted file contents as bytes
            byte[] encryptedContents = File.ReadAllBytes(encryptedFilePath);

            // Decrypt the file contents
            byte[] decryptedContents = this._encryptionService.DecryptWithPassword(encryptedContents, password);

            // Create the decrypted file path by removing the ".encrypted" extension
            string decryptedFilePath = encryptedFilePath.Replace(".encrypted", "");

            // Save the decrypted contents to the new file
            File.WriteAllBytes(decryptedFilePath, decryptedContents);

            Console.WriteLine($"File decrypted and saved as: {decryptedFilePath}");
            return decryptedFilePath;
        }
    }
}
