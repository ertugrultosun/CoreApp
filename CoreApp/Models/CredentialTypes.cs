using System;
using System.Collections.Generic;

namespace CoreApp.Models
{
    public partial class CredentialTypes
    {
        public CredentialTypes()
        {
            Credentials = new HashSet<Credentials>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? Position { get; set; }

        public ICollection<Credentials> Credentials { get; set; }
    }
}
