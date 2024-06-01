using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuardianVault
{
    public interface  IView<T>
    {
        T ShowView(IWin32Window parent, T model);
    }
}
