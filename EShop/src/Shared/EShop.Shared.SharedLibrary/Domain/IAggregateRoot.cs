using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Shared.SharedLibrary.Domain
{
    public interface IAggregateRoot
    {
        //tanım: Aggregate'ler domain olaylarını fırlatır ve bunları dinleyenlerin bu olaylara tepki vermesini sağlar.

        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void ClearDomainEvents();

    }
}
