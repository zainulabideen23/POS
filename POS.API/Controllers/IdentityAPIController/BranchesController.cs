using Microsoft.AspNetCore.Mvc;
using POS.Application.Services;
using POS.Identity.Application.DTOs;

namespace POS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BranchesController : ControllerBase
{
    private readonly BranchService _branchService;

    public BranchesController(BranchService branchService)
    {
        _branchService = branchService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result =
            await _branchService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result =
            await _branchService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("tenant/{tenantId:int}")]
    public async Task<IActionResult> GetByTenant(
        int tenantId)
    {
        var result =
            await _branchService
                .GetByTenantIdAsync(tenantId);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateBranchDto dto)
    {
        var result =
            await _branchService.CreateAsync(dto);

        if (result == null)
            return BadRequest("Tenant does not exist.");

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateBranchDto dto)
    {
        var updated =
            await _branchService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted =
            await _branchService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}