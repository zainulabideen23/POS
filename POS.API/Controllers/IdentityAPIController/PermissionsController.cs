using Microsoft.AspNetCore.Mvc;
using POS.Identity.Application.DTOs.Permissions;
using POS.Identity.Application.Services;

namespace POS.API.Controllers.IdentityAPIController
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly PermissionService _permissionService;

        public PermissionsController(
            PermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var permissions =
                await _permissionService.GetAllAsync(
                    cancellationToken);

            return Ok(permissions);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var permission =
                await _permissionService.GetByIdAsync(
                    id,
                    cancellationToken);

            if (permission == null)
                return NotFound();

            return Ok(permission);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreatePermissionRequest request,
            CancellationToken cancellationToken)
        {
            var permission =
                await _permissionService.CreateAsync(
                    request.Name,
                    request.Description,
                    cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = permission.Id },
                permission);
        }
    }
}