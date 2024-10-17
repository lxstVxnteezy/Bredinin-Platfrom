using Bredinin.TestProject.DataContext.DataAccess.Repositories;
using Bredinin.TestProject.Service.Contracts.Exceptions;
using Bredinin.TestProject.Service.Contracts.Product.Info;
using Bredinin.TestProject.Service.Core.Extensions;

namespace Bredinin.TestProject.Service.Http.Handlers.Methods.Product
{
    public interface IProductInfoHandler : IHandler
    {
        Task<InfoProductResponse> Handle(Guid id, CancellationToken ctn);
    }
    internal class ProductInfoHandler : IProductInfoHandler
    {
        private readonly IRepository<Domain.Product> _productRepository;

        public ProductInfoHandler(IRepository<Domain.Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<InfoProductResponse> Handle(Guid id, CancellationToken ctn)
        {
            var foundProduct = await _productRepository.FoundByIdAsync(id, ctn);

            if (foundProduct == null)
            {
                throw OwnError.UnableToFindProduct.ToException($"Product with id {id} " +
                                                               $"not found in db");
            }

            return new InfoProductResponse(
                foundProduct.Id,
                foundProduct.Name,
                foundProduct.Description,
                foundProduct.Price,
                foundProduct.ProductCategoryId);
        }
    }
}
