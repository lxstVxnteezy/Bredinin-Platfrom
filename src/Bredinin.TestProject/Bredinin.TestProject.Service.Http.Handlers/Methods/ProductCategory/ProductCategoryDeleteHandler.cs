using Bredinin.TestProject.DataContext.DataAccess.Repositories;
using Bredinin.TestProject.Service.Contracts.Exceptions;
using Bredinin.TestProject.Service.Core.Extensions;
using Microsoft.AspNetCore.Mvc;


namespace Bredinin.TestProject.Service.Http.Handlers.Methods.ProductCategory
{
    public interface IProductCategoryDeleteHandler : IHandler
    {
        Task<ActionResult> Handle(Guid id, CancellationToken ctn);
    }

    public class ProductCategoryDeleteHandler : IProductCategoryDeleteHandler
    {
        private readonly IRepository<Domain.ProductCategory> _productCategoryRepository;

        public ProductCategoryDeleteHandler(IRepository<Domain.ProductCategory> productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<ActionResult> Handle(Guid id, CancellationToken ctn)
        {
            var foundProductCategory = await _productCategoryRepository.FoundByIdAsync(id, ctn);
            if (foundProductCategory == null)
            {
                throw OwnError.CanNotDeleteProductCategory.ToException($"Product category with id {id} " +
                                                                       $"not found in db");
            }

            _productCategoryRepository.Remove(foundProductCategory);

            await _productCategoryRepository.SaveChanges(ctn);

            return new StatusCodeResult(204);

        }
    }
}
