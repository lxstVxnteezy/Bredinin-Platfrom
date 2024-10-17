namespace Bredinin.TestProject.Service.Contracts.Product.Create
{ 
    public record ProductCreateRequest(string Name, string Description, decimal Price, Guid ProductCategoryId);
}
