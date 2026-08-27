using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using POS.Identity.Application.DTOs.Roles;
using POS.Identity.Application.Interfaces;
using POS.Identity.Domain.Entities;
using POS.Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUser _currentUser;

        public RoleService(RoleManager<ApplicationRole> roleManager,UserManager<ApplicationUser> userManager, ICurrentUser currentUser)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public async Task<(bool Success, string Message)> CreateRoleAsync(CreateRoleRequest request,CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.RoleName))
            {
                return (
                    false,
                    "Role name is required."
                );
            }

            Guid tenantId = _currentUser.TenantId;

            var roleName = request.RoleName.Trim();
            var normalizedName = roleName.ToUpperInvariant();

            var exists = await _roleManager.Roles
                .AnyAsync(
                    x => x.TenantId == tenantId &&
                         x.NormalizedName == normalizedName,
                    cancellationToken);

            if (exists)
            { 
                return (false, $"Role '{roleName}' already exists.");
            }

            var role = new ApplicationRole
            {
                Name = roleName,
                NormalizedName = normalizedName,
                TenantId = tenantId
            };

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ",result.Errors.Select(x => x.Description));

                return ( false, $"Role creation failed: {errors}");
            }

            return (true,$"Role '{roleName}' created successfully.");
        }

        public async Task<List<RoleResponse>> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            return await _roleManager.Roles.Select(r => new RoleResponse
                {
                    Id = r.Id,
                    Name = r.Name!
                })
                .ToListAsync<RoleResponse>(cancellationToken);
        }

        public async Task<(bool Success, string Message)> AssignRoleToUserAsync(
    AssignRoleRequest request,
    CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                return (false, "User not found.");
            }

            var roleExists = await _roleManager.RoleExistsAsync(
                request.RoleName);

            if (!roleExists)
            {
                return (false, $"Role '{request.RoleName}' does not exist.");
            }

            var alreadyAssigned = await _userManager.IsInRoleAsync(
                user,
                request.RoleName);

            if (alreadyAssigned)
            {
                return (
                    false,
                    $"Role '{request.RoleName}' is already assigned to this user."
                );
            }

            // Inserts into AspNetUserRoles
            var result = await _userManager.AddToRoleAsync(
                user,
                request.RoleName);

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(x => x.Description));

                return (
                    false,
                    $"Failed to assign role: {errors}"
                );
            }

            return (
                true,
                $"Role '{request.RoleName}' assigned to user successfully."
            );
        }
    }
}
