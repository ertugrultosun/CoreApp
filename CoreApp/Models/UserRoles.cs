using System;
using System.Collections.Generic;

namespace CoreApp.Models
{
    public partial class UserRoles
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }

        public Roles Role { get; set; }
        public Users User { get; set; }
    }
}
