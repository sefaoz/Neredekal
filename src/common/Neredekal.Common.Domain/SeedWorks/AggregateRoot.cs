using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Neredekal.Common.Domain.SeedWorks
{
    public abstract class AggregateRoot : Entity
    {
        public List<INotification> _domainEvents;

        public void AddDomainEvents(IDomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }
    }
}
