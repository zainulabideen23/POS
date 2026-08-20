using Microsoft.Extensions.DependencyInjection;
using POS.Application.Services;
using POS.Identity.Application.Interfaces;
using POS.Identity.Application.Services;


namespace POS.Identity.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityApplication(this IServiceCollection services)
        {
            // Register application services here
            services.AddScoped<BranchService>();
            services.AddScoped<TenantService>();
            services.AddScoped<IRoleService,RoleService>();
            services.AddScoped<PermissionService>();
            services.AddScoped<RolePermissionService>();
            return services;
        }
    }
}
