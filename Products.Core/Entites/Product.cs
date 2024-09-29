namespace Products.Core.Entites
{
    public class Product : BaseEntity
    {
        public Product()
        {
            
        }

        public Product(string name, string description, decimal price)  
        {
            if(!IsValidPrice(price))
                throw new ArgumentException("Price must be greater than 0", nameof(price));

            Name = name;
            Description = description;
            Price = price;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }

        public void UpdatePrice(decimal price)
        {
            if (!IsValidPrice(price)) 
                throw new ArgumentException("New price must be greater than 0", nameof(price)); 

            Price = price;
        }

        private bool IsValidPrice(decimal price)
        {
            return price > 0;
        }
    }
}
