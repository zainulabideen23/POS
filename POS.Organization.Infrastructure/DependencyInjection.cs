using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Organization.Application.Interfaces;
using POS.Organization.Infrastructure.Persistence;
using POS.Organization.Infrastructure.Repository;


namespace POS.Organization.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOrganizationInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<POSOrganzationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
