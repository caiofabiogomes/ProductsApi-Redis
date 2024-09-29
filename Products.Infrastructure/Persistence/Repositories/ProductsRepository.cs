using Microsoft.EntityFrameworkCore;
using Products.Core.Entites;
using Products.Core.Repositories;

namespace Products.Infrastructure.Persistence.Repositories
{
    public class ProductsRepository(ProductsDbContext context) : IProductsRepository
    {
        private readonly ProductsDbContext _context = context;

        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();

            return products;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            return product;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product is null)
                return;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
