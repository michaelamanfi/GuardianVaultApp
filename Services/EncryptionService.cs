using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace GuardianVault
{
    /// <summary>
    /// Provides encryption and decryption utilities using AES algorithm.
    /// Note: Encryption/Decryption code taken from
    /// https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-8.0
    /// </summary>
    public class EncryptionService : IEncryptionService
    {
        private readonly ISystemIdentifierService systemIdentifierService;
        public EncryptionService(ISystemIdentifierService systemIdentifierService)
        {
            this.systemIdentifierService = systemIdentifierService;
        }

        // Constant to ensure the password is long enough
        public const string KEY_PART = "04CAADA9-2899-4EF9-BDEE-85351F282058";       

        /// <summary>
        /// Encrypts the given data using the specified password.
        /// </summary>
        /// <param name="password">The password used to generate the encryption key.</param>
        /// <param name="data">The data to be encrypted.</param>
        /// <returns>The encrypted data as a byte array.</returns>
        public byte[] EncryptWithPassword(byte[] data, string password, EncryptionLevels encryptionLevels)
        {
            if (data == null || data.Length <= 0)
                throw new ArgumentNullException(nameof(data));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            string keySource = $"{KEY_PART}{password}";
            if(encryptionLevels != EncryptionLevels.LEVEL2)
            {
                string cpuId = systemIdentifierService.GetCpuId();
                keySource = $"{KEY_PART}{cpuId}{password}";
            }
            else if (encryptionLevels != EncryptionLevels.LEVEL3)
            {
                string cpuId = systemIdentifierService.GetCpuId();
                string motherBoardId = systemIdentifierService.GetMotherboardSerialNumber();
                keySource = $"{KEY_PART}{cpuId}{motherBoardId}{password}";
            }

            // Compute hash from the key source
            byte[] hashKey = EncryptionService.ComputeHash(keySource);

            // Use the first 32 bytes of the hash for the AES-256 key
            byte[] aesKey = new byte[32];
            Array.Copy(hashKey, aesKey, 32);

            using (Aes myAes = Aes.Create())
            {
                myAes.Key = aesKey;
                myAes.GenerateIV(); // Generate a new IV for each encryption

                // Encrypt the data to an array of bytes.
                byte[] encrypted = Encrypt(data, myAes.Key, myAes.IV);

                // Prepend IV to the encrypted data for use during decryption
                byte[] result = new byte[myAes.IV.Length + encrypted.Length];
                Array.Copy(myAes.IV, 0, result, 0, myAes.IV.Length);
                Array.Copy(encrypted, 0, result, myAes.IV.Length, encrypted.Length);

                return result;
            }
        }

        /// <summary>
        /// Decrypts the given data using the specified password.
        /// </summary>
        /// <param name="password">The password used to generate the decryption key.</param>
        /// <param name="data">The data to be decrypted.</param>
        /// <returns>The decrypted data as a byte array.</returns>
        public byte[] DecryptWithPassword(byte[] data, string password, EncryptionLevels encryptionLevels)
        {
            if (data == null || data.Length <= 0)
                throw new ArgumentNullException(nameof(data));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            string keySource = $"{KEY_PART}{password}";
            if (encryptionLevels != EncryptionLevels.LEVEL2)
            {
                string cpuId = systemIdentifierService.GetCpuId();
                keySource = $"{KEY_PART}{cpuId}{password}";
            }
            else if (encryptionLevels != EncryptionLevels.LEVEL3)
            {
                string cpuId = systemIdentifierService.GetCpuId();
                string motherBoardId = systemIdentifierService.GetMotherboardSerialNumber();
                keySource = $"{KEY_PART}{cpuId}{motherBoardId}{password}";
            }

            // Compute hash from the key source
            byte[] hashKey = EncryptionService.ComputeHash(keySource);

            // Use the first 32 bytes of the hash for the AES-256 key
            byte[] aesKey = new byte[32];
            Array.Copy(hashKey, aesKey, 32);

            // Extract IV from the beginning of the data
            byte[] iv = new byte[16];
            byte[] cipherText = new byte[data.Length - 16];
            Array.Copy(data, iv, iv.Length);
            Array.Copy(data, iv.Length, cipherText, 0, cipherText.Length);

            using (Aes myAes = Aes.Create())
            {
                myAes.Key = aesKey;
                myAes.IV = iv;

                // Decrypt the bytes to a byte array.
                byte[] decrypted = Decrypt(cipherText, myAes.Key, myAes.IV);
                return decrypted;
            }
        }

        /// <summary>
        /// Encrypts the given raw data using the specified key and IV.
        /// </summary>
        /// <param name="rawData">The raw data to be encrypted.</param>
        /// <param name="Key">The encryption key.</param>
        /// <param name="IV">The initialization vector.</param>
        /// <returns>The encrypted data as a byte array.</returns>
        private static byte[] Encrypt(byte[] rawData, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (rawData == null || rawData.Length <= 0)
                throw new ArgumentNullException(nameof(rawData));
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException(nameof(Key));
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException(nameof(IV));
            byte[] encrypted;

            // Create an Aes object with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (BinaryWriter swEncrypt = new BinaryWriter(csEncrypt))
                        {
                            // Write the raw data to the CryptoStream
                            swEncrypt.Write(rawData);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        /// <summary>
        /// Decrypts the given encrypted data using the specified key and IV.
        /// </summary>
        /// <param name="encryptedData">The encrypted data to be decrypted.</param>
        /// <param name="Key">The decryption key.</param>
        /// <param name="IV">The initialization vector.</param>
        /// <returns>The decrypted data as a byte array.</returns>
        private static byte[] Decrypt(byte[] encryptedData, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (encryptedData == null || encryptedData.Length <= 0)
                throw new ArgumentNullException(nameof(encryptedData));
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException(nameof(Key));
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException(nameof(IV));

            byte[] decrypted;

            // Create an Aes object with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream())
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                    {
                        // Write the encrypted data to the CryptoStream
                        csDecrypt.Write(encryptedData, 0, encryptedData.Length);
                    }
                    // Convert the MemoryStream content to a byte array
                    decrypted = msDecrypt.ToArray();
                }
            }

            // Return the decrypted bytes from the memory stream.
            return decrypted;
        }

        /// <summary>
        /// Computes the SHA256 hash of the given input string.
        /// </summary>
        /// <param name="input">The input string to be hashed.</param>
        /// <returns>The computed hash as a byte array.</returns>
        private static byte[] ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the input string to a byte array
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // Compute the hash of the input bytes
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Return the computed hash bytes
                return hashBytes;
            }
        }
    }
}
