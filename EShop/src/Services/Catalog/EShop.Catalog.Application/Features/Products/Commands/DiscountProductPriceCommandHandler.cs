using EShop.Catalog.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Catalog.Application.Features.Products.Commands
{
    internal class DiscountProductPriceCommandHandler(IProductRepository productRepository) : IRequestHandler<DiscountProductPriceCommandRequest, DiscountProductPriceCommandResponse>
    {
        public async Task<DiscountProductPriceCommandResponse> Handle(DiscountProductPriceCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId);

            if (product == null)
            {
                return new DiscountProductPriceCommandResponse(false, "Ürün bulunumadı");
            }

            product.ApplyDiscount(request.DiscountRate);
            await productRepository.UpdateAsync(product);

            return new DiscountProductPriceCommandResponse(true, "İndirim uygulandı");

        }
    }
}
