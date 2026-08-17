// POS.API/Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using POS.Identity.Application.Interfaces;
using POS.Identity.Application.DTOs;

namespace POS.API.Controllers.IdentityAPIController;

[ApiController]
[Route("api/identity/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
       
        var isRegistered = await _authService.RegisterAsync(request);

        if (!isRegistered)
        {
            return BadRequest("Registration failed. Please check your details.");
        }

        return Ok(new { Message = "User registered successfully." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                Message = "Validation failed.",
                Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
            });
        }

        try
        {
            // 2. Attempt Login (This should return the JWT string)
            var token = await _authService.LoginAsync(request);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }

            // 3. Configure Secure Cookie for JWT
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // XSS Protection: JavaScript cannot read this cookie
                Secure = true,   // HTTPS only (Set to false ONLY if developing on localhost HTTP)
                SameSite = SameSiteMode.Strict, // CSRF Protection
                Expires = DateTime.UtcNow.AddMinutes(120) // Must match your JWT Token Expiry
            };

            // 4. Append the token to the Response Cookies
            Response.Cookies.Append("X-Access-Token", token, cookieOptions);

            // 5. Return success (Notice we DO NOT send the token in the JSON body anymore)
            return Ok(new
            {
                Message = "Login successful",
                IsAuthenticated = true,
                TenantId = request.TenantId // Helpful for the frontend to know the active tenant
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            // Catch specific domain exceptions (e.g., "Tenant access revoked")
            return Unauthorized(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            // Catch unexpected server errors
            // In production, log the 'ex.Message' to Serilog/AppInsights, but don't expose it to the user.
            return StatusCode(500, new { Message = "An internal server error occurred during login." });
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