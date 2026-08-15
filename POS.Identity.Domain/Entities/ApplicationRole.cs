using Microsoft.AspNetCore.Identity;
using POS.Identity.Domain.Entities;

public class ApplicationRole : IdentityRole
{
    // Navigation Properties
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}