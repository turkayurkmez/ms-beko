using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.EventBus
{
    public  record OrderCreatedEvent(OrderCreatedCommand OrderCreatedCommand): IntegrationEvent;

    public record OrderCreatedCommand(int OrderId, string CustomerId, string CreditCardInfo, IEnumerable<OrderItem> OrderItems);

    public record OrderItem(string ProductId, int Quantity, decimal Price);

}
