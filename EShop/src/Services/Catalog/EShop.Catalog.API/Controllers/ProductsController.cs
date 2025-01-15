using EShop.Catalog.Application.Features.Products.Queries;
using EShop.Catalog.Application.Services;
using EShop.Catalog.Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<IActionResult> GetProducts()
        {
            //var productService = new ProductService();
            //var products = productService.GetProducts();

            var request = new GetAllProductsQueryRequest();
            var products = _mediator.Send(request);

            var result = Ok(products);
            return Task.FromResult<IActionResult>(result);
        }
    }
}
