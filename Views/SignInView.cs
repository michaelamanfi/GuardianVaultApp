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
    public partial class SignInView : IView<SignInModel>
    {
        public SignInView()
        {
        }

        public SignInModel ShowView(IWin32Window parent, SignInModel model)
        {
            using (SignInDlg dlg = new SignInDlg())
            {
                // Show the dialog as a modal window and check if the user clicked OK
                if (dlg.ShowDialog(parent) == DialogResult.OK)
                {
                    // If sign-in is successful, update the user name in upper case
                    return new SignInModel { UserName = dlg.UserName.ToUpper(), Authenticated = true };

                }
                else
                {
                    // Return false if the dialog was canceled or closed without confirmation
                    return new SignInModel { Authenticated = false };
                }
            }
        }
    }
}
