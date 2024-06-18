using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardianVault
{
    /// <summary>
    /// Represents the user configuration settings and details specific to the user's encryption preferences.
    /// </summary>
    public class UserSettingsModel
    {
        /// <summary>
        /// Gets or sets the path to the Master Password model.
        /// </summary>
        /// <value>The master password model.</value>
        public MasterPasswordModel PasswordModel { get; set; }

        /// <summary>
        /// Gets or sets the path to the folder where encrypted files are stored.
        /// </summary>
        /// <value>The path to the encrypted folder.</value>
        public string EncryptedFolderPath { get; set; }

        /// <summary>
        /// Gets or sets the encryption level applied to the files within the specified folder.
        /// </summary>
        /// <value>The level of encryption used for securing files.</value>
        public EncryptionLevels EncryptionLevel { get; set; }
    }
}
