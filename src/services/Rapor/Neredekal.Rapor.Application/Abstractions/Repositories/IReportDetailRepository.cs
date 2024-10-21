using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neredekal.Common.Domain.SeedWorks;
using Neredekal.Rapor.Domain.AggregateModels.RaporModels;

namespace Neredekal.Rapor.Application.Abstractions.Repositories
{
    public interface IReportDetailRepository : IUnitOfWorks
    {
        Task Create(ReportDetail reportDetail);
        Task<ReportDetail> Get(Guid id);
    }
}
