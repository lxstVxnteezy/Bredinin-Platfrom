using Bredinin.TestProject.DataContext.DataAccess.Repositories;
using Bredinin.TestProject.Service.Contracts.Exceptions;
using Bredinin.TestProject.Service.Contracts.ProductCategory.Create;
using Bredinin.TestProject.Service.Core.Extensions;


namespace Bredinin.TestProject.Service.Http.Handlers.Methods.ProductCategory
{
    public interface IProductCategoryCreateHandler : IHandler
    {
        Task<ProductCategoryResponse> Handle(ProductCategoryRequest request, CancellationToken ctn);
    }
    internal class ProductCategoryCreateHandler : IProductCategoryCreateHandler
    {
        private readonly IRepository<Domain.ProductCategory> _productCategoryRepository;

        public ProductCategoryCreateHandler(IRepository<Domain.ProductCategory> productCategory)
        {
            _productCategoryRepository = productCategory;
        }

        public async Task<ProductCategoryResponse> Handle(ProductCategoryRequest request, CancellationToken ctn)
        {
            AssertExistProductCategory(request);

            var newProductCategory = new Domain.ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };

            _productCategoryRepository.Add(newProductCategory);

            await _productCategoryRepository.SaveChanges(ctn);

            return new ProductCategoryResponse(Id: newProductCategory.Id);

        }

        private void AssertExistProductCategory(ProductCategoryRequest request)
        {
            var isExist = _productCategoryRepository.Query.Any(x => x.Name == request.Name);
            if (isExist)
                throw OwnError.UnableToCreateProductCategory.ToException($"Category with name = {request.Name} already exists");

        }
    }
}
