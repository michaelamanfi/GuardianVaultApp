using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Provides a generic interface for displaying views with models.
    /// </summary>
    /// <typeparam name="T">The type of the model used by the view.</typeparam>
    public interface IView<T>
    {
        /// <summary>
        /// Displays a view modally with a specified parent window and model.
        /// </summary>
        /// <param name="parent">The parent window for the modal view.</param>
        /// <param name="model">The model to display in the view.</param>
        /// <returns>The updated model after the view is closed.</returns>
        T ShowView(IWin32Window parent, T model);
    }
}
