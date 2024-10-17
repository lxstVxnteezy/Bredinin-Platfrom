using Bredinin.TestProject.DataContext.DataAccess.Repositories;
using Bredinin.TestProject.Service.Contracts.Exceptions;
using Bredinin.TestProject.Service.Contracts.Product.Create;
using Bredinin.TestProject.Service.Core.Extensions;

namespace Bredinin.TestProject.Service.Http.Handlers.Methods.Product
{
    public interface IProductCreateHandler : IHandler
    {
        Task<ProductCreateResponse> Handle(ProductCreateRequest request, CancellationToken ctn);
    }
    internal class ProductCreateHandler : IProductCreateHandler
    {
        private readonly IRepository<Domain.Product> _productRepository;
        private readonly IRepository<Domain.ProductCategory> _productCategoryRepository;

        public ProductCreateHandler(
            IRepository<Domain.Product> productRepository, 
            IRepository<Domain.ProductCategory> productCategoryRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<ProductCreateResponse> Handle(ProductCreateRequest request, CancellationToken ctn)
        {
            AssertExistProduct(request);
            AssertExistCategory(request);

            var newProduct = new Domain.Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ProductCategoryId = request.ProductCategoryId
            };
            
            _productRepository.Add(newProduct);

            await _productRepository.SaveChanges(ctn);

            return new ProductCreateResponse(newProduct.Id);

        }

        private void AssertExistCategory(ProductCreateRequest request)
        {
            var IsExist = _productCategoryRepository.Query.SingleOrDefault(x => x.Id == request.ProductCategoryId);
            if (IsExist == null)
                throw OwnError.UnableToCreateProduct.ToException($"Category with id = {request.ProductCategoryId} not found in db");

        }

        private void AssertExistProduct(ProductCreateRequest request)
        {
            var isExist = _productRepository.Query.Any(x => x.Name == request.Name);
            if (isExist)
                throw OwnError.UnableToCreateProduct.ToException($"Product with name = {request.Name} already exists");
        }
    }
}
