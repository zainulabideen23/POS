using Microsoft.Extensions.DependencyInjection;
using POS.Product.Application.Interfaces.CategoryInterfaces;
using POS.Product.Application.Services;

namespace POS.Product.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddProductApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}