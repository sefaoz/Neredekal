using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Neredekal.Rapor.Application.Abstractions.Repositories;
using Neredekal.Rapor.Domain.AggregateModels.RaporModels;

namespace Neredekal.Rapor.Infrastructure.Persistence.Repositories
{
    public class ReportDetailRepository : IReportDetailRepository
    {
        private readonly ReportDbContext _context;

        public ReportDetailRepository(ReportDbContext context)
        {
            _context = context;
        }

        public async Task Create(ReportDetail reportDetail, CancellationToken cancellationToken)
        {
            await _context.Set<ReportDetail>().AddAsync(reportDetail, cancellationToken);
        }

        public async Task<ReportDetail> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<ReportDetail>().FirstOrDefaultAsync(x => x.UUID == id, cancellationToken);
        }

        public async Task<List<ReportDetail>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Set<ReportDetail>().ToListAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
