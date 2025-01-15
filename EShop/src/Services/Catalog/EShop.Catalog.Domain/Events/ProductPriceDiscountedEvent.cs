using EShop.Shared.SharedLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Catalog.Domain.Events
{
    public class ProductPriceDiscountedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }
        public decimal OldPrice { get; private set; }
        public decimal NewPrice { get; private set; }
        public ProductPriceDiscountedEvent(Guid productId, decimal oldPrice, decimal newPrice)
        {
            ProductId = productId;
            OldPrice = oldPrice;
            NewPrice = newPrice;
        }

    }
}
