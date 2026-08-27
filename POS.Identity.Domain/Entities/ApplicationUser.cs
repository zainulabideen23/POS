using Microsoft.AspNetCore.Identity;
    

namespace POS.Identity.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
         //User having key chain 
        //for netuserrole I add no navigation property but relationship
        //between user and role exist and mange by identity framework

        // Navigation Properties
    public ICollection<UserTenant> UserTenants { get; set; } = new List<UserTenant>();
    public ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();
    }
}
