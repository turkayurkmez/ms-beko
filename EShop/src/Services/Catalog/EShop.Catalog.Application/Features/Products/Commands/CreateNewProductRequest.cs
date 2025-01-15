using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Catalog.Application.Features.Products.Commands
{
    public record CreateNewProductRequest(string Name, string Description, decimal Price, int Stock, int? CategoryId, string ImageUrl = "noImage.png");



}
