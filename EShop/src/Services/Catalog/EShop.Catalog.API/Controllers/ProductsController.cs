using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public Task<IActionResult> GetProducts()
        {

            var result = Ok(new { products = new[] {"A","B","C" } });
            return Task.FromResult<IActionResult>(result);
        }
    }
}
