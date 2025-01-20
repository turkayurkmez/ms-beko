using EShop.EventBus;
using MassTransit;

namespace EShop.Payment.API.Consumers
{
    public class StockAvailableEventConsumer(ILogger<StockAvailableEventConsumer> logger) : IConsumer<StockAvailableEvent>
    {
        public async Task Consume(ConsumeContext<StockAvailableEvent> context)
        {
            var command = context.Message.Command;
            var isSuccess = new Random().Next(1, 10) > 5;

            if (isSuccess)
            {
                // Ödeme işlemi başarılıysa, PaymentSucceededEvent'i yayınla
              
                var @event = new PaymentSucceededEvent(command.OrderId);
                await context.Publish(@event);
                logger.LogInformation("Ödeme başarılı.... Olay fırlatıldı");
            }
            else
            {
                // Ödeme işlemi başarısızsa, PaymentFailedEvent'i yayınla
               
                var @event = new PaymentFailedEvent(command.OrderId);
                await context.Publish(@event);
                logger.LogInformation("Ödeme başarısız.... Olay fırlatıldı");
            }


        }
    }
}
