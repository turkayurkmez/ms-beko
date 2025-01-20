using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Catalog.Infrastructure.Outbox
{

    //MassTransit kullanılırsa, bu sınıfa ihtiyacınız olmayacak. MassTransit, Outbox Pattern'i destekler.
    public class OutBoxMessage
    {
        public  Guid Id { get; set; }
        public string AggregateType { get; set; }
        public string AggregateId { get; set; }
        public string EventType { get; set; }
        public string PayLoad { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Processed { get; set; }
    }
}
