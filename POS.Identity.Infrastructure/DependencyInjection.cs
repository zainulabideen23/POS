// POS.Identity.Infrastructure/DependencyInjection.cs
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using POS.Application.Services;
using POS.Identity.Application.Interfaces;
using POS.Identity.Domain.Entities;
using POS.Identity.Infrastructure.Data;
using POS.Identity.Infrastructure.Services;
using POS.Infrastructure.Repositories;
using System.Text;

namespace POS.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Add the DbContext first
        services.AddDbContext<PosIdentityDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("POS.Identity.Infrastructure")
            ));

        services.AddDataProtection();   

        // 2. Add ASP.NET Core Identity
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequireDigit = true;
            options.User.RequireUniqueEmail = true;
        })
        .AddRoles<ApplicationRole>() // Add roles this way
        .AddEntityFrameworkStores<PosIdentityDbContext>()
        .AddDefaultTokenProviders();

        // 3. Register your custom authentication service HERE
        services.AddScoped<IAuthService, AuthService>();


        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ITokenService, TokenService>();


        

        // 3. Register JWT Authentication right here in the module
        var jwtSettings = configuration.GetSection("JwtSettings");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
                ValidateIssuer = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidateAudience = true,
                ValidAudience = jwtSettings["Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            // Add this event block to intercept the request and read the cookie
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    if (context.Request.Cookies.ContainsKey("X-Access-Token"))
                    {
                        context.Token = context.Request.Cookies["X-Access-Token"];
                    }
                    return Task.CompletedTask;
                }
            };
        });
        return services;
    }
}