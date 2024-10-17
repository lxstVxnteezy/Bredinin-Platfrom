namespace Bredinin.TestProject.Service.Contracts.Product.Info
{
    public record InfoProductResponse(Guid Id, string Name, string Description,decimal Price, Guid ProductCategoryId);
}
