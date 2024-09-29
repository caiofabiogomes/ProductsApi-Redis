using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Products.Application.Services.Abstraction;
using Products.Application.Services.Implementation;

namespace Products.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services 
                .AddAutoMapper()
                .AddApplicationService();

            return services;
        } 

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationModule));

            return services;
        }

        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IProductsService, ProductsService>();
            
            return services;
        }
    }
}
