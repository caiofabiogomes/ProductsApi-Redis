namespace Products.Application.ViewModels
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
