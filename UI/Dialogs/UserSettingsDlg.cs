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
    /// Dialog for configuring user settings in the Guardian Vault application.
    /// </summary>
    public partial class UserSettingsDlg : BaseDialogDlg
    {
        private readonly UserSettingsModel settingsModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSettingsDlg"/> class with a specific user settings model.
        /// </summary>
        /// <param name="settingsModel">The user settings model to be manipulated within this dialog.</param>
        public UserSettingsDlg(UserSettingsModel settingsModel)
        {
            this.settingsModel = settingsModel;
            InitializeComponent();

            // Load the encrypted folder path from the settings model into the text box.
            this.txtPath.Text = settingsModel.EncryptedFolderPath;

            // Populate the ComboBox with predefined encryption levels.
            List<int> intValues = new List<int> {
                (int)EncryptionLevels.LEVEL1,
                (int)EncryptionLevels.LEVEL2,
                (int)EncryptionLevels.LEVEL3
            };
            encryptionLevelesDropdown.DataSource = intValues;
            encryptionLevelesDropdown.DropDownStyle = ComboBoxStyle.DropDownList;

            // Set the current encryption level from the settings model as the selected item in the ComboBox.
            this.encryptionLevelesDropdown.SelectedItem = (int)this.settingsModel.EncryptionLevel;
        }

        /// <summary>
        /// Handles the Cancel button click event, which cancels the settings change and closes the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Handles the OK button click event, which saves the updated settings and closes the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            settingsModel.EncryptedFolderPath = this.txtPath.Text.Trim();
            settingsModel.EncryptionLevel = (EncryptionLevels)this.encryptionLevelesDropdown.SelectedItem;

            // Log the settings update action.
            var logger = DI.Container.GetInstance<ILogger>();
            logger.LogInformation("User settings updated.");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Handles the Browse button click event, allowing the user to select a folder path through a browsing dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var browseFolderView = DI.Container.GetInstance<IView<string>>();
            var selectedPath = browseFolderView.ShowView(this, "Please select the folder you wish to use as your root folder.");

            if (!string.IsNullOrWhiteSpace(selectedPath))
            {
                this.txtPath.Text = selectedPath;
            }
        }
    }
}
