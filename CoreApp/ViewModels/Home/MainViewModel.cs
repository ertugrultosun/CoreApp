using CoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.ViewModels.Home
{
    public class MainViewModel
    {
        public Note note { get; set; }
        public Notebook notebook { get; set; }
        public Bank bank { get; set; }
        public BankAccount bankAccount { get; set; }
    }
}
