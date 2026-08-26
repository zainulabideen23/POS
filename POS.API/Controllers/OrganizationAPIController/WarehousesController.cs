
using Microsoft.AspNetCore.Mvc;
using POS.Organization.Application.DTOs;
using POS.Organization.Application.Interfaces;

namespace POS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehousesController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService ;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var warehouses = await _warehouseService.GetAllWarehousesAsync(cancellationToken);
                return Ok(warehouses);
            }
            catch (Exception ex)
            {
                // Logs the exception internally (assuming a logger is added later)
                return StatusCode(500, new { message = "An error occurred while retrieving the warehouses.", details = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var warehouse = await _warehouseService.GetWarehouseByIdAsync(id, cancellationToken);

                if (warehouse == null)
                {
                    return NotFound(new { message = $"Warehouse with ID '{id}' was not found." });
                }

                return Ok(warehouse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the warehouse.", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWarehouseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var warehouse = await _warehouseService.CreateWarehouseAsync(request, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = warehouse.Id }, warehouse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the warehouse.", details = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWarehouseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _warehouseService.UpdateWarehouseAsync(id, request, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Catches the specific exception thrown by our service and returns a 404 with the message
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the warehouse.", details = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _warehouseService.DeleteWarehouseAsync(id, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the warehouse.", details = ex.Message });
            }
        }
    }
}