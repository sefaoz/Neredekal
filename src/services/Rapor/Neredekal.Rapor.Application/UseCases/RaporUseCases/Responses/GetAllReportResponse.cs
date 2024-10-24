using Neredekal.Rapor.Domain.AggregateModels.RaporModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Rapor.Application.UseCases.RaporUseCases.Responses
{
    public class GetAllReportResponse
    {
        public Guid Id { get; set; }
        public DateTime RequestDate { get; set; }
        public ReportStatusEnum ReportStatus { get; set; }
    }
}
