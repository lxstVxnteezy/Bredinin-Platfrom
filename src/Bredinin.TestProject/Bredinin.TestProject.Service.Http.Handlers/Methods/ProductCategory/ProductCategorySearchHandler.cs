using Bredinin.TestProject.DataContext.DataAccess.Repositories;
using Bredinin.TestProject.Service.Contracts.ProductCategory.Info;
using Microsoft.EntityFrameworkCore;

namespace Bredinin.TestProject.Service.Http.Handlers.Methods.ProductCategory
{
    public interface IProductCategorySearchHandler : IHandler
    {
        Task<InfoProductCategoryResponse[]> Handle(CancellationToken ctn);
    }
    internal class ProductCategorySearchHandler : IProductCategorySearchHandler
    {
        private readonly IRepository<Domain.ProductCategory> _productCategoryRepository;

        public ProductCategorySearchHandler(IRepository<Domain.ProductCategory> productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }
        public async Task<InfoProductCategoryResponse[]> Handle(CancellationToken ctn)
        {
            var productCategories = await _productCategoryRepository.Query
                .Include(x => x.Products)
                .ToArrayAsync(ctn);

            return productCategories.Select(MapToResponse).ToArray();
        }

        private static InfoProductCategoryResponse MapToResponse(Domain.ProductCategory productCategory)
        {
            return new InfoProductCategoryResponse(
                productCategory.Id,
                productCategory.Name,
                productCategory.Description ?? string.Empty,
                productCategory.Products?.Select(p => p.Id).ToArray() ?? Array.Empty<Guid>()
            );
        }
    }
}
