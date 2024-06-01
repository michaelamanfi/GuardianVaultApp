using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace GuardianVault
{
    /// <summary>
    /// A utility class for authenticating local Windows users.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        // Import the LogonUser function from the Windows API
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool LogonUser(
            string lpszUsername,
            string lpszDomain,
            string lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            out IntPtr phToken);

        // Import the CloseHandle function from the Windows API
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private extern static bool CloseHandle(IntPtr handle);

        // Define constants for the logon type and logon provider
        private const int LOGON32_LOGON_INTERACTIVE = 2;
        private const int LOGON32_PROVIDER_DEFAULT = 0;

        /// <summary>
        /// Authenticates a user using the provided username, domain, and password.
        /// </summary>
        /// <param name="username">The username of the user to authenticate.</param>
        /// <param name="domain">The domain of the user to authenticate (use "." for local users).</param>
        /// <param name="password">The password of the user to authenticate.</param>
        /// <returns>True if authentication is successful, otherwise false.</returns>
        public bool AuthenticateUser(string username, string password)
        {
            string domain = "."; // The domain for a local user is "."
            IntPtr tokenHandle = IntPtr.Zero;

            try
            {
                // Attempt to log on the user
                bool success = LogonUser(
                    username,
                    domain,
                    password,
                    LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT,
                    out tokenHandle);

                if (!success)
                {
                    int error = Marshal.GetLastWin32Error();
                    Console.WriteLine("LogonUser failed with error code: " + error);
                    return false;
                }

                // Use the tokenHandle to create a WindowsIdentity object
                using (WindowsIdentity identity = new WindowsIdentity(tokenHandle))
                {
                    // If the identity was created successfully, the user is authenticated
                    return identity != null;
                }
            }
            finally
            {
                // Close the token handle if it was opened
                if (tokenHandle != IntPtr.Zero)
                {
                    CloseHandle(tokenHandle);
                }
            }
        }
    }
}
