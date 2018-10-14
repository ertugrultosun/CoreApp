using System;
using System.Collections.Generic;

namespace CoreApp.Models
{
    public partial class FinActivity
    {
        public long Id { get; set; }
        public string Date { get; set; }
        public long? Accid { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public double? Amount { get; set; }
        public long? Notebookid { get; set; }
        public long? Cuserid { get; set; }

        public BankAccount Acc { get; set; }
        public Users Cuser { get; set; }
        public Notebook Notebook { get; set; }
    }
}
