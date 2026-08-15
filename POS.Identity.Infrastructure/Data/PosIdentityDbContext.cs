using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POS.Identity.Domain.Entities;
using System.Reflection;


namespace POS.Identity.Infrastructure.Data;

// Note: Change the generic signature to include ApplicationRole
public class PosIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserTenant> UserTenants { get; set; }
    public DbSet<UserBranch> UserBranches { get; set; }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public PosIdentityDbContext(DbContextOptions<PosIdentityDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}