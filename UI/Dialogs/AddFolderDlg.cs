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
    /// Represents a dialog for adding a new folder within the application.
    /// </summary>
    public partial class AddFolderDlg : BaseDialogDlg
    {
        private readonly FolderModel folder;
        private readonly IFileManagementService fileManagementService;
        private readonly IApplicationController app;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddFolderDlg"/> class with a specific folder model.
        /// </summary>
        /// <param name="folder">The folder model where the new folder will be added.</param>
        public AddFolderDlg(FolderModel folder)
        {
            this.folder = folder;
            this.fileManagementService = DI.Container.GetInstance<IFileManagementService>();
            this.app = DI.Container.GetInstance<IApplicationController>();
            // Initializes the form components
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Cancel button click event. Closes the dialog and sets the result to Cancel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Public property to get or set the name of the folder.
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// Handles the OK button click event. Validates the folder name and closes the dialog if the name is valid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            // Trims the folder name input and checks for validity
            if (!this.fileManagementService.IsValidFolderName(this.folder, this.txtFolderName.Text.Trim()))
            {
                // Displays an error message if the folder name is not valid
                this.app.ShowErrorMessage(this, "Invalid folder name. Please provide a name appropriate for your Windows operating system.");
                return;
            }

            // Sets the FolderName property to the validated, trimmed folder name
            this.FolderName = this.txtFolderName.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
