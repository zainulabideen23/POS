using Microsoft.Extensions.DependencyInjection;
using POS.Shared.Application.Interfaces;
using POS.Shared.Infrastructure.Services;

namespace POS.Shared.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSharedInfrastructure(
        this IServiceCollection services)
    {
        object value = services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUser, CurrentUserService>();

        return services;
    }
}