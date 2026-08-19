using Microsoft.AspNetCore.Mvc;
using POS.Identity.Application.DTOs.Roles;
using POS.Identity.Application.Interfaces;

namespace POS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request,CancellationToken cancellationToken)
    {
        var result = await _roleService.CreateRoleAsync( request,cancellationToken);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(new
        {
            success = true,
            message = result.Message
        });
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetAllRolesAsync(cancellationToken);
        return Ok(roles);
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole(
    [FromBody] AssignRoleRequest request,
    CancellationToken cancellationToken)
    {
        var result = await _roleService.AssignRoleToUserAsync(
            request,
            cancellationToken);

        if (!result.Success)
            return BadRequest(new
            {
                success = false,
                message = result.Message
            });

        return Ok(new
        {
            success = true,
            message = result.Message
        });
    }
}