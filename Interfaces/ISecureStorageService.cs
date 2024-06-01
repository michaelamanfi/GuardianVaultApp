namespace GuardianVault
{
    /// <summary>
    /// Defines methods for secure storage management.
    /// </summary>
    public interface ISecureStorageService
    {
        /// <summary>
        /// Checks if the specified file exists in secure storage.
        /// </summary>
        /// <returns>True if the file exists, false otherwise.</returns>
        bool DoesAppDataExist();

        /// <summary>
        /// Deletes the specified file from secure storage.
        /// </summary>
        void DeleteAppData();

        /// <summary>
        /// Retrieves data from the specified file in secure storage.
        /// </summary>
        /// <returns>The retrieved data as a <see cref="AppSettingsModel"/>.</returns>
        AppSettingsModel RetrieveAppData();

        /// <summary>
        /// Stores the specified data in secure storage.
        /// </summary>
        /// <param name="data">The data to store as a <see cref="AppSettingsModel"/>.</param>
        void SaveAppData( AppSettingsModel data);
    }
}