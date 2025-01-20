using EShop.EventBus;
using MassTransit;

namespace EShop.Order.API.Consumers
{
    public class PaymentSuccessEventConsumer(ILogger<PaymentSuccessEventConsumer> logger) : IConsumer<PaymentSucceededEvent>
    {
        public Task Consume(ConsumeContext<PaymentSucceededEvent> context)
        {
            logger.LogInformation("Ödeme servisi başarılı olduğundan sipariş success olarak güncellendi");
            return Task.CompletedTask;

        }
    }
}
