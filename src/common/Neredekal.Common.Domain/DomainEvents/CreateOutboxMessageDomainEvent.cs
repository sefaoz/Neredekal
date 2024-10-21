using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neredekal.Common.Domain.SeedWorks;

namespace Neredekal.Common.Domain.DomainEvents
{
    public record CreateOutboxMessageDomainEvent(string Type, string Content) : IDomainEvent;
}
