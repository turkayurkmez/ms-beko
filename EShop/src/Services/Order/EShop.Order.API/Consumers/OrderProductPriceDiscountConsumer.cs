using EShop.EventBus;
using MassTransit;

namespace EShop.Order.API.Consumers
{
    public class OrderProductPriceDiscountConsumer(ILogger<OrderProductPriceDiscountConsumer> logger) : IConsumer<ProductPriceDiscountEvent>
    {
        public Task Consume(ConsumeContext<ProductPriceDiscountEvent> context)
        {
            logger.LogInformation($"Burası order servisi... {context.Message.ProductId} ürününün yeni fiyatı {context.Message.NewPrice} oldu");

            return Task.CompletedTask;
        }
    }
}
