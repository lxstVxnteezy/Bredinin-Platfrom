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

        public ProductCreateHandler(IRepository<Domain.Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductCreateResponse> Handle(ProductCreateRequest request, CancellationToken ctn)
        {
            AssertExistProductCategory(request);

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

        private void AssertExistProductCategory(ProductCreateRequest request)
        {
            var isExist = _productRepository.Query.Any(x => x.Name == request.Name);
            if (isExist)
                throw OwnError.UnableToCreateProduct.ToException($"Product with name = {request.Name} already exists");
        }
    }
}
