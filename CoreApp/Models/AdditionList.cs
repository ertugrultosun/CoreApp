using System;
using System.Collections.Generic;

namespace CoreApp.Models
{
    public partial class AdditionList
    {
        public long Id { get; set; }
        public long? Addid { get; set; }
        public long? Noteid { get; set; }
        public long? Cuserid { get; set; }

        public Additions Add { get; set; }
        public Users Cuser { get; set; }
        public Notebook Note { get; set; }
    }
}
