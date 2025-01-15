using EShop.Catalog.Domain.Events;
using EShop.Shared.SharedLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Catalog.Domain.Aggregates
{
    public class Product : AggregateRoot<Guid>
    {

        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }

        public int Stock { get; private set; }
        public int? CategoryId { get; private set; }

        public string? ImageUrl { get; private set; } = "noImage.png";
        public Product()
        {
            //EF Core için gerekli
        }

        public Product(string name, string description, decimal price, int stock, int? categoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
        }

        public void ApplyDiscount(decimal discountRate)
        {
            var oldPrice = Price;
            Price = Price  * (1-discountRate);
            //burada, "ürün fiyatı indirildi" olayı fırlatılabilir.
            AddDomainEvent(new ProductPriceDiscountedEvent(Id, oldPrice, Price));

        }

    }
}
