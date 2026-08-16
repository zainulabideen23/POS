using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation Property
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
