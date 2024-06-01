using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    public class ApplicationController : IApplicationController
    {
        private readonly MasterPasswordModel masterPasswordModel;
        public ApplicationController()
        {
            masterPasswordModel = new MasterPasswordModel();
        }
        public MasterPasswordModel GetMasterPassword()
        {
            return masterPasswordModel;
        }
        public void UpdateMasterPassword()
        {
            // Retrieves an instance of IView configured for SignInModel from the DI container.
            var view = DI.Container.GetInstance<IView<MasterPasswordModel>>();

            IWin32Window parent = DI.Container.GetInstance<IWin32Window>();
            // Displays the view to the user and captures the modified settings model.
            var model = view.ShowView(parent, masterPasswordModel);
            if (model.RememberKey)
            {
                //Save secret key
            }            
        }
    }
}
