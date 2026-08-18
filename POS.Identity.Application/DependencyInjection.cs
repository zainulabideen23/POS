using Microsoft.Extensions.DependencyInjection;
using POS.Application.Services;


namespace POS.Identity.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityApplication(this IServiceCollection services)
        {
            // Register application services here
            services.AddScoped<BranchService>();
            services.AddScoped<TenantService>();
            return services;
        }
    }
}
