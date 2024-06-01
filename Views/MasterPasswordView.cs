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
    public partial class MasterPasswordView : IView<MasterPasswordModel>
    {
        public MasterPasswordView()
        {
        }

        public MasterPasswordModel ShowView(IWin32Window parent, MasterPasswordModel masterPasswordModel)
        {
            using (MasterPasswordDlg dlg = new MasterPasswordDlg())
            {
                dlg.txtPassword.Text = masterPasswordModel.Password;
                dlg.chRememberKey.Checked = masterPasswordModel.RememberKey;

                // Show the dialog as a modal window and check if the user clicked OK
                if (dlg.ShowDialog(parent) == DialogResult.OK)
                {
                    masterPasswordModel.RememberKey = dlg.chRememberKey.Checked;
                    masterPasswordModel.Password = dlg.txtPassword.Text.Trim();
                }
            }
            return masterPasswordModel;
        }
    }
}
