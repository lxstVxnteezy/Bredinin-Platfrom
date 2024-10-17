using Bredinin.TestProject.DataContext.DataAccess.Repositories;
using Bredinin.TestProject.Service.Contracts.Exceptions;
using Bredinin.TestProject.Service.Contracts.ProductCategory.Update;
using Bredinin.TestProject.Service.Core.Extensions;

namespace Bredinin.TestProject.Service.Http.Handlers.Methods.ProductCategory
{
    public interface IProductCategoryUpdateHandler : IHandler
    {
        public Task<UpdateProductCategoryResponse> Handle(
            Guid id,
            UpdateProductCategoryRequest request,
            CancellationToken ctn);
    }

    internal class ProductCategoryUpdateHandler : IProductCategoryUpdateHandler
    {
        private readonly IRepository<Domain.ProductCategory> _productCategoryRepository;

        public ProductCategoryUpdateHandler(IRepository<Domain.ProductCategory> productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }


        public async Task<UpdateProductCategoryResponse> Handle(
            Guid id,
            UpdateProductCategoryRequest request,
            CancellationToken ctn)
        {
            AssertExistProductCategory(request);

            var foundProductCategory = await _productCategoryRepository.FoundByIdAsync(id, ctn);
            if (foundProductCategory == null)
            {
                throw OwnError.CanNotUpdateProductCategory.ToException($"Product category with id {id} " +
                                                                       $"not found in db");
            }

            foundProductCategory.Name = request.Name;
            foundProductCategory.Description = request.Description;

            await _productCategoryRepository.SaveChanges(ctn);

            return new UpdateProductCategoryResponse(
                    foundProductCategory.Name,
                    foundProductCategory.Description
                );
        }

        private void AssertExistProductCategory(UpdateProductCategoryRequest request)
        {
            var isExist = _productCategoryRepository.Query.Any(x => x.Name == request.Name);
            if (isExist)
                throw OwnError.CanNotUpdateProductCategory.ToException($"Category with name = {request.Name} already exists");

        }
    }
}
