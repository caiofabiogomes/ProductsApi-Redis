using Microsoft.EntityFrameworkCore;
using Products.Core.Entites;
using System.Linq.Expressions;
using System.Reflection;

namespace Products.Infrastructure.Persistence
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var filter = Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(parameter, nameof(BaseEntity.IsDeleted)),
                            Expression.Constant(false)),
                        parameter);
                    entityType.SetQueryFilter(filter);

                    var method = modelBuilder.Entity(entityType.ClrType).GetType().GetMethod("HasIndex", new[] { typeof(string) });
                    if (method != null)
                    {
                        var indexBuilder = method.Invoke(modelBuilder.Entity(entityType.ClrType), new object[] { nameof(BaseEntity.IsDeleted) });
                        var hasFilterMethod = indexBuilder.GetType().GetMethod("HasFilter");
                        hasFilterMethod.Invoke(indexBuilder, new object[] { $"{nameof(BaseEntity.IsDeleted)} = 0" });
                    }
                }
            }
        }

        public override int SaveChanges()
        {
            HandleSoftDelete();
            UpdateLastUpdated();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleSoftDelete();
            UpdateLastUpdated();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateLastUpdated()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity &&
                            (e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                ((BaseEntity)entry.Entity).Update();
            }
        }

        private void HandleSoftDelete()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted && e.Entity is BaseEntity);

            foreach (var entry in entries)
            {
                entry.State = EntityState.Modified;
                ((BaseEntity)entry.Entity).Delete();
            }
        }
    }
}
