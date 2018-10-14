using System;
using System.Collections.Generic;

namespace CoreApp.Models
{
    public partial class BankAccount
    {
        public BankAccount()
        {
            CurActivity = new HashSet<CurActivity>();
            FinActivity = new HashSet<FinActivity>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? Userid { get; set; }
        public string Type { get; set; }
        public string Cdate { get; set; }
        public long? Bankid { get; set; }

        public Bank Bank { get; set; }
        public Users User { get; set; }
        public ICollection<CurActivity> CurActivity { get; set; }
        public ICollection<FinActivity> FinActivity { get; set; }
    }
}
