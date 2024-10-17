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

        public ProductUpdateHandler(IRepository<Domain.Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<UpdateProductResponse> Handle(Guid id, UpdateProductRequest request, CancellationToken ctn)
        {
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

    }
}
