using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Bredinin.TestProject.Service.Domain;
using Bredinin.TestProject.Service.Domain.Base;

namespace Bredinin.TestProject.Service.DataContext
{
    public class ServiceContext : DbContext
    {
        public ServiceContext(DbContextOptions options) : base(options)
        {
        }

        public ServiceContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly(),
                t => t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) &&
                    typeof(BaseEntity).IsAssignableFrom(i.GenericTypeArguments[0]))
            );
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
