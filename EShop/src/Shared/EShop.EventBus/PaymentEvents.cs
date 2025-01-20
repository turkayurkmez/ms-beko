using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.EventBus
{
    public record PaymentSucceededEvent(int OrderId) : IntegrationEvent;
    public record PaymentFailedEvent(int OrderId) : IntegrationEvent;

}
