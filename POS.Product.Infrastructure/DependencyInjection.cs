using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Product.Application.Interfaces;
using POS.Product.Application.Interfaces.BrandInterfaces;
using POS.Product.Application.Interfaces.CategoryInterfaces;
using POS.Product.Application.Interfaces.ProductInterfaces;
using POS.Product.Application.Services;
using POS.Product.Infrastructure.Persistance;
using POS.Product.Infrastructure.Repository;

namespace POS.Product.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddProductInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<ApplciationDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString(
                    "DefaultConnection"));
            options.ConfigureWarnings(warning => warning.Ignore(
                RelationalEventId.PendingModelChangesWarning));
        });

        services.AddScoped<IProductRepository, ProductRepository>();


        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}