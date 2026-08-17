// POS.Identity.Infrastructure/Services/AuthService.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using POS.Identity.Application.DTOs;
using POS.Identity.Application.Interfaces;
using POS.Identity.Domain.Entities;
using POS.Identity.Infrastructure.Data;
// using POS.Identity.Domain.Entities; (Assuming ApplicationUser is here)

namespace POS.Identity.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager; // Added RoleManager
    private readonly PosIdentityDbContext _PosidentityDbContext;   // Added DbContext

    private readonly ITokenService _tokenService;

    // Inject all three dependencies into the constructor
    public AuthService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        PosIdentityDbContext identityDbContext,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _PosidentityDbContext = identityDbContext;
        _tokenService = tokenService;
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        // 1. Create the user in AspNetUsers
        var result = await _userManager.CreateAsync(user, request.Password);

        // If creation fails (e.g., duplicate email), stop immediately
        if (!result.Succeeded)
        {
            return false;
        }

        // 2. Link the user to their workspace in UserTenants
        var userTenant = new UserTenant
        {
            UserId = user.Id,
            TenantId = request.TenantId,
            IsActive = true,
            JoinedAt = DateTime.UtcNow
        };

        _PosidentityDbContext.UserTenants.Add(userTenant);

        // 3. Assign the user's role in AspNetUserRoles
        if (!string.IsNullOrEmpty(request.RoleName) && await _roleManager.RoleExistsAsync(request.RoleName))
        {
            await _userManager.AddToRoleAsync(user, request.RoleName);
        }

        // 4. Commit the new tenant and role relationships to the database
        var saved = await _PosidentityDbContext.SaveChangesAsync();

        return saved > 0;
    }

    public async Task<string> LoginAsync(LoginRequest request)
    {
        // 1. Verify User exists
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) return null;

        // 2. Verify Password
        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isValidPassword) return null;

        // 3. Verify Tenant Access
        // Ensure this user actually belongs to the workspace they are trying to log into
        var tenantAccess = _PosidentityDbContext.UserTenants
            .FirstOrDefault(ut => ut.UserId == user.Id && ut.TenantId == request.TenantId);

        if (tenantAccess == null || !tenantAccess.IsActive)
        {
            throw new UnauthorizedAccessException("You do not have active access to this workspace.");
        }

        // 4. Gather Permissions (Optional: if you have RolePermissions setup)
        // For now, we will pass an empty list or a list of their roles
        var userRoles = await _userManager.GetRolesAsync(user);
        var permissions = new List<string>(); // Populate this from RolePermissions table later

        // 5. Generate and return the real JWT
        return _tokenService.GenerateToken(user, request.TenantId, permissions);
    }
}