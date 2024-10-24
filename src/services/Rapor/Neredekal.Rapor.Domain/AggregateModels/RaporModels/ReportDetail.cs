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
        public string Data { get; set; }
        #endregion

        #region ctor
        protected ReportDetail() { }

        private ReportDetail(Guid id, string data, ReportStatusEnum reportStatus)
        {
            UUID = id;
            Data = data;
            ReportStatus = reportStatus;
            RequestDate = DateTime.UtcNow;
        }
        #endregion

        #region methods

        public static ReportDetail CreateReportDetail(Guid id, string data, ReportStatusEnum reportStatus = ReportStatusEnum.Preparing)
        {
            if(id == Guid.Empty) id = Guid.NewGuid();
            Guard.CannotNull(data, nameof(data));

            return new(id, data, reportStatus);
        }

        public void SetStatusCompleted(string data)
        {
            if(ReportStatus != ReportStatusEnum.Completed)
            {
                ReportStatus = ReportStatusEnum.Completed;
                Data = data;
            }
        }

        #endregion
    }
}
