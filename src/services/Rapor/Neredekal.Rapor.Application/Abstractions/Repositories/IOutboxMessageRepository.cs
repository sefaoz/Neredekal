using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neredekal.Common.Domain.OutboxModel;

namespace Neredekal.Rapor.Application.Abstractions.Repositories
{
    public interface IOutboxMessageRepository
    {
        Task Create(OutboxMessage outboxMessage);
        Task Update(OutboxMessage  outboxMessage);
        Task<OutboxMessage> Get(Guid id);
    }
}
