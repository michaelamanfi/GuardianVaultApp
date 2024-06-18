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
    public partial class AddFolderDlg : BaseDialogDlg
    {
        private readonly FolderModel folder;
        private readonly IFileManagementService fileManagementService;
        private readonly IApplicationController app;
        public AddFolderDlg(FolderModel folder)
        {
            this.folder = folder;
            this.fileManagementService = DI.Container.GetInstance<IFileManagementService>();
            this.app = DI.Container.GetInstance<IApplicationController>();
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public string FolderName { get; set; }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!this.fileManagementService.IsValidFolderName(this.folder,this.txtFolderName.Text.Trim()))
            {
                this.app.ShowErrorMessage(this,"Invalid folder name. Please provide a name appropriate for your Windows operating system.");

                return;
            }

            this.FolderName = this.txtFolderName.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
