using EShop.EventBus;
using MassTransit;

namespace EShop.Order.API.Consumers
{
    public class StockNotAvailableEventConsumer(ILogger<StockNotAvailableEventConsumer> logger) : IConsumer<StockNotAvailableEvent>
    {
        public Task Consume(ConsumeContext<StockNotAvailableEvent> context)
        {
            logger.LogInformation("Stok servisi başarısız olduğundan sipariş failed olarak güncellendi");
            return Task.CompletedTask;
        }
    }
}
