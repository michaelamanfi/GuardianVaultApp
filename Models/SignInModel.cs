using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardianVault
{
    /// <summary>
    /// Represents the data model for the sign-in process.
    /// </summary>
    public class SignInModel
    {
        /// <summary>
        /// Gets or sets the user name of the individual trying to sign in.
        /// </summary>
        /// <value>The user name.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is authenticated successfully.
        /// </summary>
        /// <value>True if the user is authenticated; otherwise, false.</value>
        public bool Authenticated { get; set; }
    }
}
