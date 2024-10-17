using Bredinin.TestProject.DataContext.DataAccess.Repositories;
using Bredinin.TestProject.Service.Contracts.Exceptions;
using Bredinin.TestProject.Service.Contracts.Product.Update;
using Bredinin.TestProject.Service.Core.Extensions;

namespace Bredinin.TestProject.Service.Http.Handlers.Methods.Product
{
    public interface IProductUpdateHandler : IHandler
    {
        public Task<UpdateProductResponse> Handle(
            Guid id,
            UpdateProductRequest request,
            CancellationToken ctn);
    }

    internal class ProductUpdateHandler : IProductUpdateHandler
    {
        private readonly IRepository<Domain.Product> _productRepository;
        private readonly IRepository<Domain.ProductCategory> _productCategoryRepository;

        public ProductUpdateHandler(
            IRepository<Domain.Product> productRepository, 
            IRepository<Domain.ProductCategory> productCategoryRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }
        public async Task<UpdateProductResponse> Handle(Guid id, UpdateProductRequest request, CancellationToken ctn)
        {
            AssertExistCategory(request);
            var foundProduct = await _productRepository.FoundByIdAsync(id, ctn);
            if (foundProduct == null)
            {
                throw OwnError.CanNotUpdateProduct.ToException($"Product category with id {id} " +
                                                               $"not found in db");
            }
            foundProduct.Name = request.Name;
            foundProduct.Description = request.Description;
            foundProduct.ProductCategoryId = request.ProductCategoryId;
            foundProduct.Price = request.Price;


          await _productRepository.SaveChanges(ctn);

          return new UpdateProductResponse(
              foundProduct.Name, 
              foundProduct.Description, 
              foundProduct.Price,
              foundProduct.ProductCategoryId);
        }
        private void AssertExistCategory(UpdateProductRequest request)
        {
            var IsExist = _productCategoryRepository.Query.SingleOrDefault(x => x.Id == request.ProductCategoryId);
            if (IsExist == null)
                throw OwnError.UnableToCreateProduct.ToException($"Category with id = {request.ProductCategoryId} not found in db");
        }
    }
}
