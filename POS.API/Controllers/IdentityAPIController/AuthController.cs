// POS.API/Controllers/AuthController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Identity.Application.DTOs;
using POS.Identity.Application.Interfaces;
using POS.Shared.Application.Interfaces;

namespace POS.API.Controllers.IdentityAPIController;

[ApiController]
[Route("api/identity/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ICurrentUser _currentUser;

    public AuthController(IAuthService authService, ICurrentUser currentUser)
    {
        _authService = authService;
        _currentUser = currentUser;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
    [FromBody] RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);

        if (!result.Success)
        {
            return BadRequest(new
            {
                success = false,
                message = result.Message
            });
        }

        return Ok(new
        {
            success = true,
            message = result.Message
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                Message = "Validation failed.",
                Errors = ModelState.Values
                    .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
            });
        }

        try
        {
            // LoginAsync gets TenantId and BranchId
            // from UserTenant and UserBranch
            var token = await _authService.LoginAsync(request);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new
                {
                    Message = "Invalid email or password."
                });
            }

            // Store JWT in HttpOnly cookie
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(120)
            };

            Response.Cookies.Append(
                "X-Access-Token",
                token,
                cookieOptions);

            return Ok(new
            {
                Message = "Login successful",
                IsAuthenticated = true
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new
            {
                Message = ex.Message
            });
        }
        catch (Exception)
        {
            return StatusCode(500, new
            {
                Message = "An internal server error occurred during login."
            });
        }
    }

    // POS.API/Controllers/AuthController.cs
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("X-Access-Token");
        return Ok(new { Message = "Logged out successfully" });
    }

}