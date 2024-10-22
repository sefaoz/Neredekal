using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neredekal.Rapor.Application.Abstractions.Repositories;
using Neredekal.Rapor.Domain.AggregateModels.RaporModels;

namespace Neredekal.Rapor.Infrastructure.Persistence.Repositories
{
    public class ReportDetailRepository : IReportDetailRepository
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Create(ReportDetail reportDetail)
        {

        }

        public Task<ReportDetail> Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
