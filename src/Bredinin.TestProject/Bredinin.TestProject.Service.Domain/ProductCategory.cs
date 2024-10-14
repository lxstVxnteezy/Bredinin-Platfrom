using Bredinin.TestProject.Service.Domain.Base;

namespace Bredinin.TestProject.Service.Domain
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
