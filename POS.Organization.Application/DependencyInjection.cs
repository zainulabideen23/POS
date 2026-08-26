using Microsoft.Extensions.DependencyInjection;
using POS.Organization.Application.Interfaces;
using POS.Organization.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Organization.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection ADDOrganizationApplication(this IServiceCollection services)
        {
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<IPOSRegisterService, POSRegisterService>();
            return services;
        }
    }
}
