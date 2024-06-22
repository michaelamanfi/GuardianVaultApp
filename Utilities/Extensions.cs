using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method to check if a TextBox is empty or contains only whitespace
        /// </summary>
        /// <param name="textBox">The TextBox control</param>
        /// <returns></returns>
        public static bool IsEmptyOrWhiteSpace(this TextBox textBox, IWin32Window parent, string field)
        {
            if(string.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show(parent, $"Invalid {field}. Please provide the missing information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            return false;
        }
    }
}
