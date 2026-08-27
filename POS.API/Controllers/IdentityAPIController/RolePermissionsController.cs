using Microsoft.AspNetCore.Mvc;
using POS.Identity.Application.DTOs.Permissions;
using POS.Identity.Application.Services;

namespace POS.API.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolePermissionsController : ControllerBase
    {
        private readonly RolePermissionService _rolePermissionService;

        public RolePermissionsController(
            RolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpPost("{roleId}/permissions")]
        public async Task<IActionResult> AssignPermission(
            Guid roleId,
            AssignPermissionRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _rolePermissionService
                    .AssignPermissionAsync(
                        roleId,
                        request.PermissionId,
                        cancellationToken);

                return Ok(new
                {
                    message =
                        "Permission assigned successfully."
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("{roleId}/permissions")]
        public async Task<IActionResult> GetPermissions(
            Guid roleId,
            CancellationToken cancellationToken)
        {
            var permissions =
                await _rolePermissionService
                    .GetPermissionsByRoleAsync(
                        roleId,
                        cancellationToken);

            return Ok(permissions);
        }

        [HttpDelete(
            "{roleId}/permissions/{permissionId:int}")]
        public async Task<IActionResult> RemovePermission(
            Guid roleId,
            int permissionId,
            CancellationToken cancellationToken)
        {
            try
            {
                await _rolePermissionService
                    .RemovePermissionAsync(
                        roleId,
                        permissionId,
                        cancellationToken);

                return Ok(new
                {
                    message =
                        "Permission removed successfully."
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }
    }
}