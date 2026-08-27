using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Purchase.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Purchase.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPurchaseInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register your infrastructure services here
            // For example, if you have a DbContext:
            services.AddDbContext<POSPurchaseDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            return services;
        }
    }
}
