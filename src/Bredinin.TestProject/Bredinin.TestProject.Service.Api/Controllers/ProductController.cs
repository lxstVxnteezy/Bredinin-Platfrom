using Bredinin.TestProject.Service.Api.Controllers.Base;
using Bredinin.TestProject.Service.Contracts.Product.Create;
using Bredinin.TestProject.Service.Contracts.Product.Info;
using Bredinin.TestProject.Service.Contracts.Product.Update;
using Bredinin.TestProject.Service.Http.Handlers.Methods.Product;
using Microsoft.AspNetCore.Mvc;

namespace Bredinin.TestProject.Service.Api.Controllers
{
    [Route("api/product")]
    public class ProductController: BaseApiController
    {
        [HttpPost ("create")]
        public Task<ProductCreateResponse> Create(
            [FromServices] IProductCreateHandler handler,
            [FromBody] ProductCreateRequest request,
            CancellationToken ctn)
        {
            return handler.Handle(request, ctn);
        }

        [HttpDelete("{id}/delete")]
        public Task<ActionResult> Delete(
            [FromServices] IProductDeleteHandler handler,
            [FromRoute] Guid id,
            CancellationToken ctn
        )
        {
            return handler.Handle(id, ctn);
        }

        [HttpGet("{id}/getById")]
        public Task<InfoProductResponse> GetById(
            [FromServices] IProductInfoHandler handler,
            [FromRoute] Guid id,
            CancellationToken ctn)
        {
            return handler.Handle(id, ctn);
        }

        [HttpGet("/getProducts")]
        public Task<InfoProductResponse[]> Get(
            [FromServices] IProductSearchHandler handler,
            CancellationToken ctn)
        {
            return handler.Handle(ctn);
        }

        [HttpPut("{id}/update")]
        public Task<UpdateProductResponse> Update(
            [FromServices] IProductUpdateHandler handler,
            [FromRoute] Guid id,
            [FromBody] UpdateProductRequest request,
            CancellationToken ctn)
        {
            return handler.Handle(id, request, ctn);
        }



    }
}
