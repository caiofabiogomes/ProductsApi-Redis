namespace Products.Core.Entites
{
    public abstract class BaseEntity 
    {
        protected BaseEntity()
        { 

            CreatedAt = DateTime.Now;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public bool IsDeleted { get; private set; }

        public void Update()
        {
            UpdatedAt = DateTime.Now;
        }

        public void Delete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.Now;
        }
    }
}
