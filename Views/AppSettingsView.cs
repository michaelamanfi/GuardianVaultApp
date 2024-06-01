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
    public partial class AppSettingsView : IView<AppSettingsModel>
    {
        public AppSettingsView()
        {
        }

        public AppSettingsModel ShowView(IWin32Window parent, AppSettingsModel settingsModel)
        {
            using (AppSettingsDlg dlg = new AppSettingsDlg())
            {
                dlg.txtPath.Text = settingsModel.EncryptedFolderPath;
                // Show the dialog as a modal window and check if the user clicked OK
                if (dlg.ShowDialog(parent) == DialogResult.OK)
                {
                    settingsModel.EncryptedFolderPath = dlg.txtPath.Text.Trim();
                }
            }

            return settingsModel;
        }
    }
}
