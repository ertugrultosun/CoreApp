using System;
using System.Collections.Generic;

namespace CoreApp.Models
{
    public partial class Notebook
    {
        public Notebook()
        {
            AdditionList = new HashSet<AdditionList>();
            CurActivity = new HashSet<CurActivity>();
            FinActivity = new HashSet<FinActivity>();
            Message = new HashSet<Message>();
            Note = new HashSet<Note>();
            UserList = new HashSet<UserList>();
        }

        public long Id { get; set; }
        public string Cdate { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public long? Cuserid { get; set; }

        public Users Cuser { get; set; }
        public ICollection<AdditionList> AdditionList { get; set; }
        public ICollection<CurActivity> CurActivity { get; set; }
        public ICollection<FinActivity> FinActivity { get; set; }
        public ICollection<Message> Message { get; set; }
        public ICollection<Note> Note { get; set; }
        public ICollection<UserList> UserList { get; set; }
    }
}
