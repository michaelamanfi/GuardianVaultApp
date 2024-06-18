using System;
using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Provides a view for adding a new folder.
    /// </summary>
    public class AddFolderView : IView<FolderModel>
    {
        /// <summary>
        /// Displays the add folder dialog and processes user input.
        /// </summary>
        /// <param name="parent">The parent window that will own the modal dialog.</param>
        /// <param name="model">The folder model containing initial data, such as the path where the new folder will be created.</param>
        /// <returns>A <see cref="FolderModel"/> representing the newly added folder; returns null if the operation is canceled.</returns>
        public FolderModel ShowView(IWin32Window parent, FolderModel model)
        {
            // Create a new instance of the AddFolderDlg, initializing it with the provided model.
            AddFolderDlg addFolderDlg = new AddFolderDlg(model);

            // Show the dialog as a modal window and check if the user clicked OK.
            if (addFolderDlg.ShowDialog(parent) == DialogResult.OK)
            {
                // If the user clicked OK, create a new FolderModel with the updated path and name.
                return new FolderModel
                {
                    Path = $"{model.Path}\\{addFolderDlg.FolderName}",
                    Name = addFolderDlg.FolderName
                };
            }

            // If the user canceled the dialog, return null.
            return null;
        }
    }
}
