using AutoMapper;
using Products.Application.Services.Abstraction;
using Products.Application.ViewModels;
using Products.Core.Caching;
using Products.Core.Entites;
using Products.Core.Repositories;
using Newtonsoft.Json;

namespace Products.Application.Services.Implementation
{
    public class ProductsService(IProductsRepository productsRepository, IMapper mapper, ICachingService cachingService) : IProductsService
    {
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ICachingService _cachingService = cachingService;

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            ProductDto product;
            var cacheKey = $"product-{id}";
            var productCache = await _cachingService.GetAsync(cacheKey);

            if (!string.IsNullOrWhiteSpace(productCache))
            {
                product = JsonConvert.DeserializeObject<ProductDto>(productCache);
            }
            else
            {
                var productEntity = await _productsRepository.GetByIdAsync(id);
                if (productEntity is null)
                    return null;

                var jsonProduct = JsonConvert.SerializeObject(productEntity);
                await _cachingService.SetAsync(cacheKey, jsonProduct);

                product = _mapper.Map<ProductDto>(productEntity);
            }
            
            return product;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            List<ProductDto> products = new List<ProductDto>();

            var cacheKey = "all-products";

            var productsCache = await _cachingService.GetAsync(cacheKey);

            if (!string.IsNullOrWhiteSpace(productsCache))
            {
                products = JsonConvert.DeserializeObject<List<ProductDto>>(productsCache);
            }
            else
            {
                var productsEntities = await _productsRepository.GetAllAsync();
                var jsonProducts = JsonConvert.SerializeObject(productsEntities);
                await _cachingService.SetAsync(cacheKey, jsonProducts);
                products = _mapper.Map<List<ProductDto>>(productsEntities);
            }
            
            return products;
        }

        public async Task AddProductAsync(ProductDto product)
        {
            var productEntity = new Product(product.Name, product.Description, product.Price);

            await _productsRepository.AddAsync(productEntity);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _productsRepository.GetByIdAsync(id);

            if (product is null)
                throw new Exception("Product not found");

            await _productsRepository.DeleteAsync(id);
        }

        public async Task UpdateProductAsync(ProductDto product)
        {
            var productEntity = _mapper.Map<Product>(product);

            var productIsNull = await _productsRepository.GetByIdAsync(product.Id) is null;

            if (productIsNull)
                throw new Exception("Product not found");

            var jsonProduct = JsonConvert.SerializeObject(product);

            var cacheKey = $"product-{product.Id}";
            await cachingService.RemoveAsync(cacheKey);

            var cacheKeyList = $"product-{product.Id}";
            await cachingService.RemoveAsync(cacheKeyList);

            await _productsRepository.UpdateAsync(productEntity);
        }
    }
}
