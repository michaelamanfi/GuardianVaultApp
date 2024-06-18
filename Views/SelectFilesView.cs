using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    /// <summary>
    /// Represents a view for selecting one or more files.
    /// </summary>
    /// <typeparam name="FileModel[]">The type of the model, which is an array of <see cref="FileModel"/> objects.</typeparam>
    public class SelectFilesView : IView<FileModel[]>
    {
        /// <summary>
        /// Shows the view for selecting files.
        /// </summary>
        /// <param name="parent">The parent window for the file selection dialog.</param>
        /// <param name="model">The initial model, which is not used in this implementation.</param>
        /// <returns>An array of <see cref="FileModel"/> objects representing the selected files, or <c>null</c> if no files were selected.</returns>
        public FileModel[] ShowView(IWin32Window parent, FileModel[] model)
        {
            // Create a new OpenFileDialog instance
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the title and other properties of the dialog
            openFileDialog.Title = "Select one or more files to encrypt.";
            openFileDialog.Multiselect = true; // Allow selecting multiple files
            openFileDialog.Filter = "All files (*.*)|*.*"; // Set the file filter

            // Show the dialog and check if the user clicked OK
            if (openFileDialog.ShowDialog(parent) == DialogResult.OK)
            {
                // Create a list to store the selected file models
                List<FileModel> fileModels = new List<FileModel>();

                // Iterate over the selected file paths
                foreach (string file in openFileDialog.FileNames)
                {
                    // Create a new FileModel instance for each selected file
                    fileModels.Add(new FileModel { Path = file });
                }

                // Return the selected file models as an array
                return fileModels.ToArray();
            }

            // If no files were selected, return null
            return null;
        }
    }
}