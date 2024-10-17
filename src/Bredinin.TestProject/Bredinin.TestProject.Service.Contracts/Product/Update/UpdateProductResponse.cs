namespace Bredinin.TestProject.Service.Contracts.Product.Update
{
    public record UpdateProductResponse(string Name, string Description, decimal Price, Guid ProductCategoryId);
}
