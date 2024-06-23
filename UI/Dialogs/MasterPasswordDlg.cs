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
    /// Represents a dialog for entering the master password required to access the Guardian Vault.
    /// </summary>
    public partial class MasterPasswordDlg : BaseDialogDlg
    {
        /// <summary>
        /// Constructor for MasterPasswordDlg, initializes the components of the dialog.
        /// </summary>
        public MasterPasswordDlg()
        {
            // Initialize the form's components
            InitializeComponent();
        }

        /// <summary>
        /// Event handler for the cancel button click. This method closes the dialog and returns a Cancel dialog result.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Set the dialog result to Cancel to specify that the dialog was closed without user validation
            this.DialogResult = DialogResult.Cancel;
            // Closes the dialog
            this.Close();
        }

        /// <summary>
        /// Event handler for the OK button click. Validates the password field before closing the dialog with an OK result.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            // Check if the password text field is either empty or whitespace
            if (this.txtPassword.IsEmptyOrWhiteSpace(this, "Password"))
                // If it is, return early without closing the dialog
                return;

            // Set the dialog result to OK to indicate successful data entry
            this.DialogResult = DialogResult.OK;
            // Closes the dialog
            this.Close();
        }
    }
}
