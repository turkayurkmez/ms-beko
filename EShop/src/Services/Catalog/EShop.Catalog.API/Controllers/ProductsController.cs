using EShop.Catalog.Application.Features.Products.Commands;
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
        public async Task<IActionResult> GetProducts()
        {
            //var productService = new ProductService();
            //var products = productService.GetProducts();

            var request = new GetAllProductsQueryRequest();         

            var products = await _mediator.Send(request);

            return Ok(products);
        
        }

        [HttpPut("{id}/discount/{discount}")]
        public async Task<IActionResult> DiscountProductPrice(Guid id, decimal discount)
        {
            //var productService = new ProductService();
            //productService.DiscountProductPrice(id, discount);
            var request = new DiscountProductPriceCommandRequest(id, discount);
            var response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
