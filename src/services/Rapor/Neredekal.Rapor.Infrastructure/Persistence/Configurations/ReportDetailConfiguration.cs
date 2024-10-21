using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Neredekal.Rapor.Domain.AggregateModels.RaporModels;

namespace Neredekal.Rapor.Infrastructure.Persistence.Configurations
{
    internal class ReportDetailConfiguration : IEntityTypeConfiguration<ReportDetail>
    {
        public void Configure(EntityTypeBuilder<ReportDetail> builder)
        {
            builder.HasKey(x => x.UUID);

            builder.Property(x => x.RequestDate).IsRequired();
            builder.Property(x => x.ReportStatus).IsRequired();
            builder.Property(x => x.Data).IsRequired();

        }
    }
}
