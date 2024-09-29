using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.Core.Caching;
using Products.Core.Repositories;
using Products.Infrastructure.Caching;
using Products.Infrastructure.Persistence;
using Products.Infrastructure.Persistence.Repositories;

namespace Products.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPersistence(configuration)
                .AddRepositories()
                .AddRedis();

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ProductsDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductsRepository, ProductsRepository>(); 

            return services;
        }

        private static IServiceCollection AddRedis(this IServiceCollection services)
        {
            services.AddScoped<ICachingService, CachingService>();
            services.AddStackExchangeRedisCache(options =>
                {
                    options.InstanceName = "Products";
                    options.Configuration = "127.0.0.1:6379";
                }
            );

            return services;
        }
    }
}
