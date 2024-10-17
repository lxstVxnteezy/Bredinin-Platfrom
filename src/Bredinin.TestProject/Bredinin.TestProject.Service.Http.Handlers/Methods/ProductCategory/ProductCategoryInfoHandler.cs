using Bredinin.TestProject.DataContext.DataAccess.Repositories;
using Bredinin.TestProject.Service.Contracts.Exceptions;
using Bredinin.TestProject.Service.Contracts.ProductCategory.Info;
using Bredinin.TestProject.Service.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Bredinin.TestProject.Service.Http.Handlers.Methods.ProductCategory
{
    public interface IProductCategoryInfoHandler : IHandler
    {
        Task<InfoProductCategoryResponse> Handle(Guid id, CancellationToken ctn);
    }

    internal class ProductCategoryInfoHandler : IProductCategoryInfoHandler
    {
        private readonly IRepository<Domain.ProductCategory> _productCategoryRepository;

        public ProductCategoryInfoHandler(IRepository<Domain.ProductCategory> productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<InfoProductCategoryResponse> Handle(Guid id, CancellationToken ctn)
        {
            var foundProductCategory = await _productCategoryRepository.Query
                .Include(x => x.Products)
                .SingleOrDefaultAsync(x => x.Id == id, ctn);

            if (foundProductCategory == null)
            {
                throw OwnError.CanNotFindProductCategory.ToException($"Product category with id {id} " +
                                                                     $"not found in db");
            }

            return new InfoProductCategoryResponse(
                Id: foundProductCategory.Id,
                Name: foundProductCategory.Name,
                Description: foundProductCategory.Description,
                ProductIds: foundProductCategory.Products.Select(pr => pr.Id).ToArray()
            );

        }
    }
}
