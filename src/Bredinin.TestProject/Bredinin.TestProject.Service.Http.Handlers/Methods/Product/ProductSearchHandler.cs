using Bredinin.TestProject.DataContext.DataAccess.Repositories;
using Bredinin.TestProject.Service.Contracts.Product.Info;
using Microsoft.EntityFrameworkCore;

namespace Bredinin.TestProject.Service.Http.Handlers.Methods.Product
{
    public interface IProductSearchHandler : IHandler
    {
        Task<InfoProductResponse[]> Handle(CancellationToken ctn);
    }
    internal class ProductSearchHandler : IProductSearchHandler
    {
        private readonly IRepository<Domain.Product> _productRepository;

        public ProductSearchHandler(IRepository<Domain.Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<InfoProductResponse[]> Handle(CancellationToken ctn)
        {
            var products = await _productRepository.Query.ToArrayAsync(ctn);

            return products.Select(MapToResponse).ToArray();
        }

        private static InfoProductResponse MapToResponse(Domain.Product product)
        {
            return new InfoProductResponse(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.ProductCategoryId
            );
        }
    }
}
