using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Operations.Application.DTOs;
using POS.Organization.Application.Interfaces;

namespace POS.API.Controllers.OrganizationAPIController
{
    [ApiController]
    [Route("api/register-sessions")]
    [Authorize] // Assuming POS requires authentication
    public class RegisterSessionsController : ControllerBase
    {
        private readonly IRegisterSessionService _registerSessionService;

        public RegisterSessionsController(IRegisterSessionService registerSessionService)
        {
            _registerSessionService = registerSessionService ;
        }

        /// <summary>
        /// Gets a list of available POS registers for opening a session.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of available registers.</returns>
        [HttpGet("available-registers")]
        [ProducesResponseType(typeof(IEnumerable<RegisterDropdownResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RegisterDropdownResponse>>> GetAvailableRegisters(CancellationToken cancellationToken)
        {
            var registers = await _registerSessionService.GetAvailableRegistersAsync(cancellationToken);
            return Ok(registers);
        }

        /// <summary>
        /// Opens a new register session.
        /// </summary>
        /// <param name="request">The request payload containing RegisterId and OpeningCash.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The opened session details.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(RegisterSessionResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RegisterSessionResponse>> OpenSession([FromBody] OpenSessionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _registerSessionService.OpenSessionAsync(request, cancellationToken);

                // Return 201 Created. 
                // Note: If you add a GetSession endpoint later, replace string.Empty with the nameof(GetSessionById)
                return Created(string.Empty, response);
            }
            catch (KeyNotFoundException ex)
            {
                // Returns 404 if the register does not exist
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Returns 400 Bad Request if the register is already open
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
