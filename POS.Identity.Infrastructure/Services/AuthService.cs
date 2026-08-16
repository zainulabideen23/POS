// POS.Identity.Infrastructure/Services/AuthService.cs
using Microsoft.AspNetCore.Identity;
using POS.Identity.Application.DTOs;
using POS.Identity.Application.Interfaces;
using POS.Identity.Domain.Entities;
// using POS.Identity.Domain.Entities; (Assuming ApplicationUser is here)

namespace POS.Identity.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        return result.Succeeded;
    }

    public async Task<string> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) return null;

        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isValidPassword) return null;

        // JWT logic goes here later
        return "JWT_TOKEN_PLACEHOLDER";
    }
}