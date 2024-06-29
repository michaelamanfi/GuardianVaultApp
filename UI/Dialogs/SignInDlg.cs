using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Represents a dialog for signing in to the Guardian Vault application.
    /// </summary>
    public partial class SignInDlg : BaseDialogDlg
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignInDlg"/> class.
        /// </summary>
        public SignInDlg()
        {
            // Initialize the form components
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Cancel button click event. Closes the dialog and sets the result to Cancel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Ask if the user really wants to exit the application
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.No)
                return;

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Public property to get the user name after successful authentication.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Handles the OK button click event. Validates user input and authenticates the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            // Validate the user name field for empty or whitespace input
            if (this.txtUserName.IsEmptyOrWhiteSpace(this, "UserName"))
                return;

            // Validate the password field for empty or whitespace input
            if (this.txtPassword.IsEmptyOrWhiteSpace(this, "Password"))
                return;

            // Retrieves an instance of the IAuthenticationService from the Dependency Injection container
            var authService = DI.Container.GetInstance<IAuthenticationService>();
            // Attempt to authenticate the user with provided credentials
            if (authService.AuthenticateUser(this.txtUserName.Text.Trim(), this.txtPassword.Text.Trim()))
            {
                // Set the UserName property to the trimmed user name if authentication is successful
                this.UserName = this.txtUserName.Text.Trim();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // Display an error message if authentication fails
                MessageBox.Show(this, "Invalid Windows username and password.", "Sign-in Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
