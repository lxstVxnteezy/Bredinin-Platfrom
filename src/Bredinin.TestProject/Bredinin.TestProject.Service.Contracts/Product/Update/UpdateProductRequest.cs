namespace Bredinin.TestProject.Service.Contracts.Product.Update
{
    public record UpdateProductRequest(string Name, string Description, decimal Price, Guid ProductCategoryId);
}
