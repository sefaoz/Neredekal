using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neredekal.Common.Domain.SeedWorks;

namespace Neredekal.Rapor.Domain.AggregateModels.RaporModels
{
    public class ReportDetail : AggregateRoot
    {
        #region properties
        public override Guid UUID { get; set; }
        public DateTime RequestDate { get; set; }
        public ReportStatusEnum ReportStatus { get; set; }
        public string? Data { get; set; }
        #endregion

        #region ctor
        protected ReportDetail() { }

        private ReportDetail(Guid id, ReportStatusEnum reportStatus, string? data)
        {
            UUID = id;
            ReportStatus = reportStatus;
            Data = data;
            RequestDate = DateTime.UtcNow;
        }
        #endregion

        #region methods

        public static ReportDetail CreateReportDetail(Guid id, string data, ReportStatusEnum reportStatus = ReportStatusEnum.Preparing)
        {
            return new(id, reportStatus, data);
        }

        public void SetStatusCompleted(string data)
        {
            ReportStatus = ReportStatusEnum.Completed;
            Data = data;
        }

        #endregion
    }
}
