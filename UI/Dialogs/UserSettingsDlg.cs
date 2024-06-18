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
    public partial class UserSettingsDlg : BaseDialogDlg
    {
        private readonly UserSettingsModel settingsModel;
        public UserSettingsDlg(UserSettingsModel settingsModel)
        {
            this.settingsModel = settingsModel;
            InitializeComponent();

            this.txtPath.Text = settingsModel.EncryptedFolderPath;

            // Create a list of integers to populate the ComboBox
            List<int> intValues = new List<int> { (int)EncryptionLevels.LEVEL1, (int)EncryptionLevels.LEVEL2,
            (int)EncryptionLevels.LEVEL3
            };

            // Set the ComboBox DataSource to the list of integers
            encryptionLevelesDropdown.DataSource = intValues;

            // Set the ComboBox DropDownStyle to DropDownList to prevent text input
            encryptionLevelesDropdown.DropDownStyle = ComboBoxStyle.DropDownList;

            this.encryptionLevelesDropdown.SelectedItem = (int)this.settingsModel.EncryptionLevel;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            settingsModel.EncryptedFolderPath = this.txtPath.Text.Trim();
            settingsModel.EncryptionLevel = (EncryptionLevels)this.encryptionLevelesDropdown.SelectedItem;


            var logger = DI.Container.GetInstance<ILogger>();
            logger.LogInformation("User settings updated.");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

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
