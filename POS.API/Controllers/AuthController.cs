// POS.API/Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using POS.Identity.Application.Interfaces;
using POS.Identity.Application.DTOs;

namespace POS.API.Controllers;

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
        var token = await _authService.LoginAsync(request);

        if (token == null)
        {
            return Unauthorized("Invalid email or password.");
        }

        return Ok(new { Token = token });
    }
}