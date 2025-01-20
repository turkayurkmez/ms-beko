using EShop.EventBus;
using MassTransit;

namespace EShop.Basket.API.Consumers
{
    public class BasketProductPriceDiscountConsumer(ILogger<BasketProductPriceDiscountConsumer> logger) : IConsumer<ProductPriceDiscountEvent>
    {
        public Task Consume(ConsumeContext<ProductPriceDiscountEvent> context)
        {
           var message = context.Message;
            logger.LogInformation($"Burası basket servisi... {message.ProductId} ürününün yeni fiyatı {message.NewPrice} oldu");

            return Task.CompletedTask;

        }
    }
}
