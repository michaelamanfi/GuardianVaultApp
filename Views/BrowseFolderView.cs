using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Represents a view for browsing and selecting a folder.
    /// </summary>
    /// <typeparam name="string">The type of the model, which is a string representing the initial folder path.</typeparam>
    public class BrowseFolderView : IView<string>
    {
        /// <summary>
        /// Shows the folder browser dialog and allows the user to select a folder.
        /// </summary>
        /// <param name="parent">The parent window for the folder browser dialog.</param>
        /// <param name="model">The initial folder path to display in the dialog.</param>
        /// <returns>The selected folder path, or <c>null</c> if the user canceled or closed the dialog.</returns>
        public string ShowView(IWin32Window parent, string model)
        {
            // Create a new FolderBrowserDialog instance
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            // Set the description of the dialog using the provided model
            folderBrowserDialog.Description = model;

            // Show the folder browser dialog and check if the user clicked OK
            if (folderBrowserDialog.ShowDialog(parent) == DialogResult.OK)
            {
                // If the user clicked OK, return the selected folder path
                return folderBrowserDialog.SelectedPath;
            }

            // If the user canceled or closed the dialog, return null
            return null;
        }
    }
}