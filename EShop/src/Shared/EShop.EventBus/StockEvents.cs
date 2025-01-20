using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.EventBus
{
    public record StockAvailableEvent(StockAvailableCommand Command) : IntegrationEvent;

    public record StockAvailableCommand(int OrderId, string CustomerId, string CreditCardNumber, decimal? TotalPrice);

    public record StockNotAvailableEvent(StockNotAvailableCommand Command) : IntegrationEvent;

    public record StockNotAvailableCommand(int OrderId);


}
