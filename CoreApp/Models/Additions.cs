using System;
using System.Collections.Generic;

namespace CoreApp.Models
{
    public partial class Additions
    {
        public Additions()
        {
            AdditionList = new HashSet<AdditionList>();
        }

        public long Id { get; set; }
        public string Cdate { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public long? Userid { get; set; }

        public Users User { get; set; }
        public ICollection<AdditionList> AdditionList { get; set; }
    }
}
