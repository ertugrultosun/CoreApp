using System;
using System.Collections.Generic;

namespace CoreApp.Models
{
    public partial class Bank
    {
        public Bank()
        {
            BankAccount = new HashSet<BankAccount>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }

        public ICollection<BankAccount> BankAccount { get; set; }
    }
}
