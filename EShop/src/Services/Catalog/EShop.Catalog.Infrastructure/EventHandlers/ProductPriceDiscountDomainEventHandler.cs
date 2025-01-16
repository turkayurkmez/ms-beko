using EShop.Catalog.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Catalog.Infrastructure.EventHandlers
{
    public class ProductPriceDiscountDomainEventHandler(ILogger<ProductPriceDiscountDomainEventHandler> logger) : INotificationHandler<ProductPriceDiscountedEvent>
    {


        public  Task Handle(ProductPriceDiscountedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"{notification.ProductId} id'li ürünün eski fiyatı {notification.OldPrice} yenisi ise {notification.NewPrice}");

            //burada, DomainEvent Integration Event'e dönüştürülecek ve RabbitMQ, Kafka gibi bir mesajlaşma aracı ile diğer mikroservislere gönderilecek!!!

            return Task.CompletedTask;
        }
    }
}
