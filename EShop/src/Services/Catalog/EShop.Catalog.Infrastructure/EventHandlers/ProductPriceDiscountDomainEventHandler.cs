using EShop.Catalog.Domain.Events;
using EShop.Catalog.Infrastructure.Migrations;
using EShop.Catalog.Infrastructure.Persistence;
using EShop.EventBus;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Catalog.Infrastructure.EventHandlers
{
    public class ProductPriceDiscountDomainEventHandler(ILogger<ProductPriceDiscountDomainEventHandler> logger, IPublishEndpoint publisher) : INotificationHandler<ProductPriceDiscountedEvent>
    {


        public  Task Handle(ProductPriceDiscountedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"{notification.ProductId} id'li ürünün eski fiyatı {notification.OldPrice} yenisi ise {notification.NewPrice}");

            //burada, DomainEvent Integration Event'e dönüştürülecek ve RabbitMQ, Kafka gibi bir mesajlaşma aracı ile diğer mikroservislere gönderilecek!!!
            var productPriceDiscountEvent = new ProductPriceDiscountEvent(notification.ProductId, notification.NewPrice, notification.OldPrice);



            /*
             *  Outbox pattern işleyişi:
             *    1. İş mantığı (fiyat güncelleme) ve outbox veritabanı kaydı transaction içerisinde gerçekleşir.
             *    2. Background job/service, outbox tablosunu düzenli olarak kontrol eder             *    
             *    3. İşlenmemiş kayıtları alır ve ilgili event'i yayınlar
             *    4. İşlenen kayıtların durumunu günceller veya siler
             */


            //Event sourcing:
            //Event sourcing, bir entity'nin state'ini değiştiren her bir işlemi bir event olarak kaydetmektir.



            publisher.Publish(productPriceDiscountEvent);

            return Task.CompletedTask;
        }
    }
}
