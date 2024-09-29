using Products.Application.ViewModels;

namespace Products.Application.Services.Abstraction
{
    public interface IProductsService
    {
        Task<ProductDto> GetProductByIdAsync(Guid id);

        Task<List<ProductDto>> GetAllProductsAsync();

        Task AddProductAsync(ProductDto product);

        Task DeleteProductAsync(Guid id);

        Task UpdateProductAsync(ProductDto product);
    }
}
