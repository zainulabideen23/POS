using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Services;
using POS.Identity.Application.DTOs;

namespace POS.API.Controllers;

[Authorize]
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
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result =
            await _branchService.GetAllAsync(cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result =
            await _branchService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

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
        Guid id,
        UpdateBranchDto dto)
    {
        var updated =
            await _branchService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted =
            await _branchService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}