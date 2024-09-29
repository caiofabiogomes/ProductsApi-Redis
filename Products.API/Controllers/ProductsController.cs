using Microsoft.AspNetCore.Mvc;
using Products.Application.Services.Abstraction;
using Products.Application.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productsService.GetAllProductsAsync();

            return Ok(products);
        }
         
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _productsService.GetProductByIdAsync(id);

            return Ok(product);
        }
         
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto product)
        {
            await _productsService.AddProductAsync(product);

            return Created("Product created!", System.Net.HttpStatusCode.Created);
        }
         
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductDto product)
        {
            await _productsService.UpdateProductAsync(product);

            return Ok("Product updated!");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productsService.DeleteProductAsync(id);

            return Ok("Product deleted!");
        }
    }
}
