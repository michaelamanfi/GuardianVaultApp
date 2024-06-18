using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardianVault
{
    /// <summary>
    /// Represents the master password data model.
    /// </summary>
    public class MasterPasswordModel
    {
        /// <summary>
        /// Gets or sets the hash value of the master password.
        /// </summary>
        /// <value>The hash of the master password.</value>
        public string HashValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the secret should be remembered by the application.
        /// </summary>
        /// <value>True if the secret should be remembered; otherwise, false.</value>
        public bool RememberSecret { get; set; }

        /// <summary>
        /// Gets a value indicating whether a key for the master password exists.
        /// </summary>
        /// <value>True if a key exists; otherwise, false.</value>
        public bool HasKey
        {
            get
            {
                return !string.IsNullOrEmpty(HashValue);
            }
        }
    }
}
