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
    public partial class SignInDlg : BaseDialogDlg
    {
        public SignInDlg()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public string UserName { get; private set; }
        private void btnOK_Click(object sender, EventArgs e)
        {
            var authService = DI.Container.GetInstance<IAuthenticationService>();
            if (authService.AuthenticateUser(this.txtUserName.Text.Trim(),this.txtPassword.Text.Trim()))
            {
                this.UserName = this.txtUserName.Text.Trim();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(this,"Invalid Windows username and password.","Sign-in Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
