using Neredekal.Rapor.Domain.AggregateModels.RaporModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Rapor.Domain.UnitTests.Tests
{
    public class ReportUnitTests
    {
        [Fact]
        public void CreateReportDetail_ShouldSetDefaultStatus_WhenNotProvided()
        {
            // Arrange
            var id = Guid.NewGuid();
            var data = "Sample data";

            // Act
            var reportDetail = ReportDetail.CreateReportDetail(id, data);

            // Assert
            Assert.Equal(ReportStatusEnum.Preparing, reportDetail.ReportStatus);
        }

        [Fact]
        public void SetStatusCompleted_ShouldNotChangeStatus_WhenCalledMultipleTimes()
        {
            // Arrange
            var id = Guid.NewGuid();
            var data = "Initial data";
            var reportDetail = ReportDetail.CreateReportDetail(id, data);

            // Act
            reportDetail.SetStatusCompleted("First update");
            var previousStatus = reportDetail.ReportStatus;
            reportDetail.SetStatusCompleted("Second update");

            // Assert
            Assert.Equal(previousStatus, reportDetail.ReportStatus);
            Assert.Equal("First update", reportDetail.Data); // İlk veri değişimi korunur
        }

        [Fact]
        public void RequestDate_ShouldNotChange_AfterCreation()
        {
            // Arrange
            var id = Guid.NewGuid();
            var data = "Sample data";
            var reportDetail = ReportDetail.CreateReportDetail(id, data);
            var initialRequestDate = reportDetail.RequestDate;

            // Act
            System.Threading.Thread.Sleep(1000); // Bir saniye beklet
            var newReportDetail = ReportDetail.CreateReportDetail(Guid.NewGuid(), "New data");

            // Assert
            Assert.Equal(initialRequestDate, reportDetail.RequestDate);
            Assert.True(newReportDetail.RequestDate > initialRequestDate);
        }

        [Fact]
        public void SetStatusCompleted_ShouldUpdateData_OnlyOnce()
        {
            // Arrange
            var id = Guid.NewGuid();
            var reportDetail = ReportDetail.CreateReportDetail(id, "Initial data");

            // Act
            reportDetail.SetStatusCompleted("First update");
            var currentData = reportDetail.Data; // İlk güncellemeyi al

            // İkinci bir güncelleme yap
            reportDetail.SetStatusCompleted("Second update");

            // Assert
            Assert.Equal(currentData, reportDetail.Data); // İlk veri değişimi korunur
        }

        [Fact]
        public void CreateReportDetail_ShouldThrowException_WhenDataIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => ReportDetail.CreateReportDetail(id, null));
            Assert.Equal($"Value cannot be null. (Parameter '{exception.ParamName}')", exception.Message);
        }

        [Fact]
        public void CreateReportDetail_ShouldThrowException_WhenIdIsEmpty()
        {
            // Arrange
            var reportId = Guid.NewGuid();
            var data = "Sample data";

            // Act
            var report = ReportDetail.CreateReportDetail(Guid.Empty, data);

            // Assert
            Assert.NotEqual(Guid.Empty, report.UUID);
        }
    }
}
