using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neredekal.Common.Domain.OutboxModel;
using Neredekal.Rapor.Application.Abstractions.Repositories;

namespace Neredekal.Rapor.Infrastructure.Persistence.Repositories
{
    public class OutboxMessageRepository : IOutboxMessageRepository
    {
        private readonly ReportDbContext _context;

        public OutboxMessageRepository(ReportDbContext context)
        {
            _context = context;
        }

        public async Task Create(OutboxMessage outboxMessage) => await _context.Set<OutboxMessage>().AddAsync(outboxMessage);

        public async Task Update(OutboxMessage outboxMessage) => _context.Set<OutboxMessage>().Update(outboxMessage);

        public async Task<OutboxMessage> Get(Guid id) => await _context.Set<OutboxMessage>().FindAsync(id);
    }
}
