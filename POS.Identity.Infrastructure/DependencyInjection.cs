// POS.Identity.Infrastructure/DependencyInjection.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Identity.Application.Interfaces;
using POS.Identity.Domain.Entities;
using POS.Identity.Infrastructure.Data;
using POS.Identity.Infrastructure.Services;

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

        services.AddDataProtection();   // <-- ADD THIS (before AddIdentityCore)

        // 2. Add ASP.NET Core Identity
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequireDigit = true;
            options.User.RequireUniqueEmail = true;
        })
        .AddRoles<IdentityRole>() // Add roles this way
        .AddEntityFrameworkStores<PosIdentityDbContext>()
        .AddDefaultTokenProviders();

        // 3. Register your custom authentication service HERE
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}