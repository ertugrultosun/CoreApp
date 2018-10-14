using System;
using System.Collections.Generic;

namespace CoreApp.Models
{
    public partial class RolePermissions
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }

        public Permissions Permission { get; set; }
        public Roles Role { get; set; }
    }
}
