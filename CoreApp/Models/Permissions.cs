using System;
using System.Collections.Generic;

namespace CoreApp.Models
{
    public partial class Permissions
    {
        public Permissions()
        {
            RolePermissions = new HashSet<RolePermissions>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? Position { get; set; }

        public ICollection<RolePermissions> RolePermissions { get; set; }
    }
}
