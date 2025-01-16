using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Catalog.Application.Contracts;
using EShop.Catalog.Domain.Aggregates;
using MediatR;

namespace EShop.Catalog.Application.Features.Products.Queries
{

    //Uçak pisti gibi düşün
    public class GetAllProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetAllProductsQueryRequest, IEnumerable<GetProductResponse>>
    {
      
        public async Task<IEnumerable<GetProductResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetAllAsync();

            var response = products.Select(p => new GetProductResponse(p.Id, p.Name, p.Description, p.Price, p.CategoryId, p.ImageUrl));

            return response;


        }
    }
}
