using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // Navigation Properties
    public ICollection<UserTenant> UserTenants { get; set; } = new List<UserTenant>();
    public ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();
    }
}
