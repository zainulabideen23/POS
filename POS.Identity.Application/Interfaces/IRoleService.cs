using POS.Identity.Application.DTOs.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Application.Interfaces
{
    public interface IRoleService
    {
        Task<(bool Success, string Message)> CreateRoleAsync(CreateRoleRequest request, CancellationToken cancellationToken);
        Task<List<RoleResponse>> GetAllRolesAsync(CancellationToken cancellationToken);
        Task<(bool Success, string Message)> AssignRoleToUserAsync(AssignRoleRequest request, CancellationToken cancellationToken);
    }
}
