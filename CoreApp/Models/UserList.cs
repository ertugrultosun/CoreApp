using System;
using System.Collections.Generic;

namespace CoreApp.Models
{
    public partial class UserList
    {
        public long Id { get; set; }
        public string Joindate { get; set; }
        public long? Notebookid { get; set; }
        public long? Userid { get; set; }

        public Notebook Notebook { get; set; }
        public Users User { get; set; }
    }
}
