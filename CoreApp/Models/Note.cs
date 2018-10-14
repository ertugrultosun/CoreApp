using System;
using System.Collections.Generic;

namespace CoreApp.Models
{
    public partial class Note
    {
        public long Id { get; set; }
        public string Cdate { get; set; }
        public string Tdate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Ctype { get; set; }
        public string Description { get; set; }
        public long? Notebookid { get; set; }
        public long? Cuserid { get; set; }

        public Users Cuser { get; set; }
        public Notebook Notebook { get; set; }
    }
}
