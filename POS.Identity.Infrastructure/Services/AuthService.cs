    // POS.Identity.Infrastructure/Services/AuthService.cs
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
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
        public readonly PosIdentityDbContext _PosidentityDbContext;   // Added DbContext

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

    public async Task<(bool Success, string Message)> RegisterAsync(
     RegisterRequest request)
    {
        await using var transaction =
            await _PosidentityDbContext.Database.BeginTransactionAsync();

        try
        {
            // 1. Find Tenant by name
            var tenant = await _PosidentityDbContext.Tenants
                .FirstOrDefaultAsync(t => t.Name == request.TenantName);

            if (tenant == null)
            {
                return (false, $"Tenant '{request.TenantName}' was not found.");
            }

            // 2. Find Branch by name inside that Tenant
            var branch = await _PosidentityDbContext.Branches
                .FirstOrDefaultAsync(b =>
                    b.Name == request.BranchName &&
                    b.TenantId == tenant.Id);

            if (branch == null)
            {
                return (
                    false,
                    $"Branch '{request.BranchName}' was not found in tenant '{tenant.Name}'."
                );
            }

            // 3. Create User
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(
                user,
                request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(e => e.Description));

                return (false, $"User registration failed: {errors}");
            }

            // 4. Create UserTenant
            var userTenant = new UserTenant
            {
                UserId = user.Id,
                TenantId = tenant.Id,
                IsActive = true,
                JoinedAt = DateTime.UtcNow
            };

            _PosidentityDbContext.UserTenants.Add(userTenant);

            // 5. Create UserBranch
            var userBranch = new UserBranch
            {
                UserId = user.Id,
                BranchId = branch.Id
            };

            _PosidentityDbContext.UserBranches.Add(userBranch);

            // 6. Assign Role
            if (!string.IsNullOrWhiteSpace(request.RoleName))
            {
                var roleExists =
                    await _roleManager.RoleExistsAsync(request.RoleName);

                if (!roleExists)
                {
                    return (
                        false,
                        $"Role '{request.RoleName}' does not exist."
                    );
                }

                var roleResult =
                    await _userManager.AddToRoleAsync(
                        user,
                        request.RoleName);

                if (!roleResult.Succeeded)
                {
                    var errors = string.Join(
                        ", ",
                        roleResult.Errors.Select(e => e.Description));

                    return (
                        false,
                        $"Failed to assign role '{request.RoleName}': {errors}"
                    );
                }
            }

            // 7. Save changes
            await _PosidentityDbContext.SaveChangesAsync();

            // 8. Commit transaction
            await transaction.CommitAsync();

            return (true, "Registration successful.");
        }
        catch
        {
            await transaction.RollbackAsync();

            return (
                false,
                "Registration failed due to a server error."
            );
        }
    }
    public async Task<string?> LoginAsync(LoginRequest request)
    {
        // 1. Find user
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return null;

        // 2. Verify password
        var isValidPassword =
            await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isValidPassword)
            return null;

        // 3. Get user's active tenant
        var tenantAccess = await _PosidentityDbContext.UserTenants
            .FirstOrDefaultAsync(ut =>
                ut.UserId == user.Id &&
                ut.IsActive);

        if (tenantAccess == null)
        {
            throw new UnauthorizedAccessException(
                "User does not have an active tenant.");
        }

        // 4. Get user's branch
        var userBranch = await _PosidentityDbContext.UserBranches
            .FirstOrDefaultAsync(ub =>
                ub.UserId == user.Id);

        if (userBranch == null)
        {
            throw new UnauthorizedAccessException(
                "User does not have an assigned branch.");
        }

        // 5. Generate JWT
        return _tokenService.GenerateToken(
            user,
            tenantAccess.TenantId,
            userBranch.BranchId);
    }

}