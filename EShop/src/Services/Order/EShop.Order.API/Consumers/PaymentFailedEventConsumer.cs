using EShop.EventBus;
using MassTransit;

namespace EShop.Order.API.Consumers
{
    public class PaymentFailedEventConsumer(ILogger<PaymentFailedEventConsumer> logger) : IConsumer<PaymentFailedEvent>
    {
        public Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            logger.LogInformation("Ödeme servisi başarısız olduğundan sipariş failed olarak güncellendi");
            return Task.CompletedTask;
        }
    }
}
