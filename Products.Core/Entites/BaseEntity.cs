namespace Products.Core.Entites
{
    public abstract class BaseEntity 
    {
        protected BaseEntity()
        { 

            CreatedAt = DateTime.Now;
        }

        private Guid _id;

        public Guid Id
        {
            get => _id;
            set { _id = value == Guid.Empty ? Guid.NewGuid() : value; }
        }

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
