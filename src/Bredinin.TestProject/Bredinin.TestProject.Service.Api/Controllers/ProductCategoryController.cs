using Bredinin.TestProject.Service.Api.Controllers.Base;
using Bredinin.TestProject.Service.Contracts.ProductCategory.Create;
using Bredinin.TestProject.Service.Contracts.ProductCategory.Info;
using Bredinin.TestProject.Service.Contracts.ProductCategory.Update;
using Bredinin.TestProject.Service.Http.Handlers.Methods.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace Bredinin.TestProject.Service.Api.Controllers
{
    [Route("api/productCategory")]
    public class ProductCategoryController:BaseApiController
    {
        [HttpPost("/create")]
        public Task<ProductCategoryResponse> Create(
            [FromServices] IProductCategoryCreateHandler handler,
            [FromBody] ProductCategoryRequest request,
            CancellationToken ctn)
        {
            return handler.Handle(request, ctn);
        }

        [HttpDelete("{id}/delete")]
        public Task<ActionResult> Delete(
            [FromServices] IProductCategoryDeleteHandler handler,
            [FromRoute] Guid id,
            CancellationToken ctn
        )
        {
            return handler.Handle(id, ctn);
        }

        [HttpGet("{id}/getById")]
        public Task<InfoProductCategoryResponse> GetById(
            [FromServices] IProductCategoryInfoHandler handler,
            [FromRoute] Guid id,
            CancellationToken ctn)
        {
            return handler.Handle(id, ctn);
        }

        [HttpGet ("/getList")]
        public Task<InfoProductCategoryResponse[]> Get(
            [FromServices] IProductCategorySearchHandler handler, 
            CancellationToken ctn)
        {
            return handler.Handle(ctn);
        }

        [HttpPut("{id}/update")]
        public Task<UpdateProductCategoryResponse> Update(
            [FromServices] IProductCategoryUpdateHandler handler,
            [FromRoute] Guid id,
            [FromBody] UpdateProductCategoryRequest request,
            CancellationToken ctn)
        {
            return handler.Handle(id, request, ctn);
        }

    }
}
