using Bredinin.TestProject.Service.Domain.Base;

namespace Bredinin.TestProject.Service.Domain
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal? Price { get; set; } 

        public virtual ProductCategory? ProductCategory { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
