using Microsoft.AspNetCore.Mvc;
using POS.Application.Services;
using POS.Identity.Application.DTOs;

namespace POS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TenantsController : ControllerBase
{
    private readonly TenantService _tenantService;

    public TenantsController(TenantService tenantService)
    {
        _tenantService = tenantService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result =
            await _tenantService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result =
            await _tenantService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateTenantDto dto)
    {
        var result =
            await _tenantService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateTenantDto dto)
    {
        var updated =
            await _tenantService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted =
            await _tenantService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}