using System;
using System.Collections.Generic;

namespace CoreApp.Models
{
    public partial class Credentials
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long CredentialTypeId { get; set; }
        public string Identifier { get; set; }
        public string Secret { get; set; }
        public string Extra { get; set; }

        public CredentialTypes CredentialType { get; set; }
        public Users User { get; set; }
    }
}
