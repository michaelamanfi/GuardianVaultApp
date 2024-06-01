using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Controller class for managing user interactions.
    /// </summary>
    public class SignInController : ISignInController
    {
        public SignInController()
        {
        }

        /// <summary>
        /// Initiates the sign-in process by displaying the sign-in dialog.
        /// </summary>
        /// <param name="parent">The parent window for the modal dialog.</param>
        /// <returns>True if sign-in was successful, false otherwise.</returns>
        public SignInModel SignIn()
        {
            IWin32Window parent = DI.Container.GetInstance<IWin32Window>();

            // Retrieves an instance of IView configured for SignInModel from the DI container.
            var view = DI.Container.GetInstance<IView<SignInModel>>();

            // Displays the view to the user and captures the modified settings model.
            var login = view.ShowView(parent,null);
            return login;
        }
    }
}
