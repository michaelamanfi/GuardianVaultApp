using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardianVault
{
    /// <summary>
    /// Defines the levels of encryption available within the application.
    /// Each level describes the complexity and the security features applied.
    /// </summary>
    public enum EncryptionLevels
    {
        /// <summary>
        /// Level 1 - No encryption complexity.
        /// </summary>
        LEVEL1 = 0, // None

        /// <summary>
        /// Level 2 - Increased encryption complexity using the CPU identifier.
        /// </summary>
        LEVEL2 = 1, // CPU id

        /// <summary>
        /// Level 3 - Advanced encryption using both CPU and motherboard identifiers.
        /// </summary>
        LEVEL3 = 2 // CPU id, motherboard id
    }
}
