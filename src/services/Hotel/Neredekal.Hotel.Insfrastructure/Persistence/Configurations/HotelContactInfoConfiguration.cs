using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Neredekal.Hotel.Domain.AggregateModels.HotelModels;

namespace Neredekal.Hotel.Insfrastructure.Persistence.Configurations
{
    internal class HotelContactInfoConfiguration : IEntityTypeConfiguration<HotelContactInfoItems>
    {
        public void Configure(EntityTypeBuilder<HotelContactInfoItems> builder)
        {
            builder.HasKey(x => x.UUID);

            builder.Property(x => x.InformationType).IsRequired();
            builder.Property(x => x.InformationContent).IsRequired();

            builder.Property(x => x.HotelId).IsRequired();
            builder.HasOne(x => x.Hotel).WithMany(x => x.HotelContactInfoItems).HasForeignKey(x => x.HotelId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
