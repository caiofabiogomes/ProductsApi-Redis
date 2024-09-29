using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Products.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ProductsDbContext(serviceProvider.GetRequiredService<DbContextOptions<ProductsDbContext>>());
           
            context.Database.Migrate();
        }
    }
}
