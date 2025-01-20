using EShop.EventBus;
using MassTransit;
using System.Reflection.Metadata.Ecma335;

namespace EShop.Stock.API.Consumers
{
    public class OrderCreatedEventConsumer(ILogger<OrderCreatedEventConsumer> logger) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var command = context.Message.OrderCreatedCommand;
            //Stock kontrolü yap
            bool isStockAvailable = checkStock(command.OrderItems);

            if (isStockAvailable)
            {
                //Stok varsa, stok azaltma işlemi yap
                //Stok azaltma işlemi başarılıysa, StokAvailableEvent'i yayınla
                //Stok azaltma işlemi başarısızsa, StokNotAvailableEvent'i yayınla

                var availableCommand = new StockAvailableCommand(command.OrderId, command.CustomerId, command.CreditCardInfo, command.OrderItems.Sum(x=>x.Quantity*x.Price));
                var @event = new StockAvailableEvent(availableCommand);
                await context.Publish(@event);
                logger.LogInformation("Stok uygun.... Olay fırlatıldı");

            }
            else
            {
                //Stok yoksa, StokNotAvailableEvent'i yayınla
                var notAvailableCommand = new StockNotAvailableCommand(command.OrderId);
                var @event = new StockNotAvailableEvent(notAvailableCommand);
                await context.Publish(@event);
                logger.LogInformation("Stok uygun değil.... Olay fırlatıldı");
            }

        }

        private bool checkStock(IEnumerable<OrderItem> orderItems)
        {
            return new Random().Next(1, 10) > 5;
        }
    }
}
