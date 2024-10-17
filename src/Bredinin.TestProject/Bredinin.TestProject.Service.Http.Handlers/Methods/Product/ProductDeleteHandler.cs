using Bredinin.TestProject.DataContext.DataAccess.Repositories;
using Bredinin.TestProject.Service.Contracts.Exceptions;
using Bredinin.TestProject.Service.Core.Extensions;
using Microsoft.AspNetCore.Mvc;


namespace Bredinin.TestProject.Service.Http.Handlers.Methods.Product
{
    public interface IProductDeleteHandler : IHandler
    {
        Task<ActionResult> Handle(Guid id, CancellationToken ctn);
    }
    internal class ProductDeleteHandler : IProductDeleteHandler
    {
        private readonly IRepository<Domain.Product> _productRepository;

        public ProductDeleteHandler(IRepository<Domain.Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ActionResult> Handle(Guid id, CancellationToken ctn)
        {
            var foundProductCategory = await _productRepository.FoundByIdAsync(id, ctn);
            if (foundProductCategory == null)
            {
                throw OwnError.CanNotDeleteProduct.ToException($"Product with id {id} " +
                                                               $"not found in db");
            }

            _productRepository.Remove(foundProductCategory);

            await _productRepository.SaveChanges(ctn);

            return new StatusCodeResult(204);
        }
    }
}
