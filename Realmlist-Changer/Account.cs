using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realmlist_Changer
{
    public class Account
    {
        public Account(string accountName, string accountPassword)
        {
            this.accountName = accountName;
            this.accountPassword = accountPassword;
        }

        public string accountName { get; set; }
        public string accountPassword { get; set; }
    }
}
