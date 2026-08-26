using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.Organization.Application.DTOs;
using POS.Organization.Application.Interfaces;

namespace POS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class POSRegistersController : ControllerBase
    {
        private readonly IPOSRegisterService _registerService;

        public POSRegistersController(IPOSRegisterService registerService)
        {
            _registerService = registerService ?? throw new ArgumentNullException(nameof(registerService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var registers = await _registerService.GetAllRegistersAsync(cancellationToken);
                return Ok(registers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the registers.", details = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var register = await _registerService.GetRegisterByIdAsync(id, cancellationToken);

                if (register == null)
                {
                    return NotFound(new { message = $"Register with ID '{id}' was not found." });
                }

                return Ok(register);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the register.", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePOSRegisterRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var register = await _registerService.CreateRegisterAsync(request, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = register.Id }, register);
            }
            catch (InvalidOperationException ex)
            {
                // Catches the duplicate code/name check from the service
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the register.", details = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePOSRegisterRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _registerService.UpdateRegisterAsync(id, request, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the register.", details = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _registerService.DeleteRegisterAsync(id, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the register.", details = ex.Message });
            }
        }
    }
}