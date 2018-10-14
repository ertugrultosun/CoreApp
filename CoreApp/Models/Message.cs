using System;
using System.Collections.Generic;

namespace CoreApp.Models
{
    public partial class Message
    {
        public long Id { get; set; }
        public string Senddate { get; set; }
        public string Status { get; set; }
        public long? Notebookid { get; set; }
        public long? Userid { get; set; }

        public Notebook Notebook { get; set; }
        public Users User { get; set; }
    }
}
