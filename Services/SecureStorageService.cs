using System;
using System.IO;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;

namespace GuardianVault
{
     /// <summary>
     /// This class implements secure storage management of the configuration data
     /// </summary>
    public class SecureStorageService : ISecureStorageService
    {
        public const string FILE = ".guardian";

        /// <summary>
      /// Stores the specified data in isolated storage.
      /// </summary>
      /// <param name="fileName">The name of the file to store the data in.</param>
      /// <param name="data">The data to store.</param>
        public void SaveAppData(AppSettingsModel data)
        {
            string jsonData = JsonConvert.SerializeObject(data);

            using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(FILE, FileMode.Create, isolatedStorage))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(jsonData);
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves data from the specified file in isolated storage.
        /// </summary>
        /// <param name="fileName">The name of the file to retrieve the data from.</param>
        /// <returns>The retrieved data.</returns>
        public AppSettingsModel RetrieveAppData()
        {
            using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                if (isolatedStorage.FileExists(FILE))
                {
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(FILE, FileMode.Open, isolatedStorage))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string jsonData = reader.ReadToEnd();
                            return JsonConvert.DeserializeObject<AppSettingsModel>(jsonData);
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Deletes the specified file from isolated storage.
        /// </summary>
        /// <param name="fileName">The name of the file to delete.</param>
        public void DeleteAppData()
        {
            using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                if (isolatedStorage.FileExists(FILE))
                {
                    isolatedStorage.DeleteFile(FILE);
                }
            }
        }

        /// <summary>
        /// Checks if the specified file exists in isolated storage.
        /// </summary>
        /// <param name="fileName">The name of the file to check for existence.</param>
        /// <returns>True if the file exists, false otherwise.</returns>
        public bool DoesAppDataExist()
        {
            using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                return isolatedStorage.FileExists(FILE);
            }
        }
    }
}
