using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Product.Application.Dtos.Unit;
using POS.Product.Application.Interfaces.UnitInterfaces;
using POS.Product.Domain.Enum;

namespace POS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitsController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var units = await _unitService.GetAllAsync(
                cancellationToken);

            return Ok(units);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var unit = await _unitService.GetByIdAsync(
                id,
                cancellationToken);

            if (unit == null)
                return NotFound();

            return Ok(unit);
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(
            UnitCategory category,
            CancellationToken cancellationToken)
        {
            if (!Enum.IsDefined(category))
                return BadRequest("Invalid unit category.");

            var units = await _unitService.GetByCategoryAsync(
                category,
                cancellationToken);

            return Ok(units);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string searchTerm,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return BadRequest("Search term is required.");

            var units = await _unitService.SearchAsync(
                searchTerm,
                cancellationToken);

            return Ok(units);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateUnitDto dto,
            CancellationToken cancellationToken)
        {
            var unit = await _unitService.CreateAsync(
                dto,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = unit.Id },
                unit);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateUnitDto dto,
            CancellationToken cancellationToken)
        {
            var unit = await _unitService.UpdateAsync(
                id,
                dto,
                cancellationToken);

            if (unit == null)
                return NotFound();

            return Ok(unit);
        }

        [HttpPatch("{id:guid}/activate")]
        public async Task<IActionResult> Activate(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _unitService.ActivateAsync(
                id,
                cancellationToken);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id:guid}/deactivate")]
        public async Task<IActionResult> Deactivate(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _unitService.DeactivateAsync(
                id,
                cancellationToken);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _unitService.DeleteAsync(
                id,
                cancellationToken);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPost("convert")]
        public async Task<IActionResult> Convert(
            [FromBody] ConvertUnitDto dto,
            CancellationToken cancellationToken)
        {
            var result = await _unitService.ConvertAsync(
                dto,
                cancellationToken);

            return Ok(result);
        }
    }

}
