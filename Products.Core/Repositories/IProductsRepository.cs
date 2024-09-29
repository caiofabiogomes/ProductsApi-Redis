using Products.Core.Entites;

namespace Products.Core.Repositories
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
