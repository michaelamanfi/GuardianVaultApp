using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardianVault
{
    public class SignInModel
    {
        public string UserName { get; set; } 
        public bool Authenticated { get; set; }
    }
}
