using System;
using System.Collections.Generic;

namespace CoreApp.Models
{
    public partial class Users
    {
        public Users()
        {
            AdditionList = new HashSet<AdditionList>();
            Additions = new HashSet<Additions>();
            BankAccount = new HashSet<BankAccount>();
            Credentials = new HashSet<Credentials>();
            CurActivity = new HashSet<CurActivity>();
            FinActivity = new HashSet<FinActivity>();
            Message = new HashSet<Message>();
            Note = new HashSet<Note>();
            Notebook = new HashSet<Notebook>();
            UserList = new HashSet<UserList>();
            UserRoles = new HashSet<UserRoles>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Created { get; set; }

        public ICollection<AdditionList> AdditionList { get; set; }
        public ICollection<Additions> Additions { get; set; }
        public ICollection<BankAccount> BankAccount { get; set; }
        public ICollection<Credentials> Credentials { get; set; }
        public ICollection<CurActivity> CurActivity { get; set; }
        public ICollection<FinActivity> FinActivity { get; set; }
        public ICollection<Message> Message { get; set; }
        public ICollection<Note> Note { get; set; }
        public ICollection<Notebook> Notebook { get; set; }
        public ICollection<UserList> UserList { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
