using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HotelModels = Neredekal.Hotel.Domain.AggregateModels.HotelModels.Hotel;

namespace Neredekal.Hotel.Insfrastructure.Persistence.Configurations
{
    internal class HotelConfiguration : IEntityTypeConfiguration<HotelModels>
    {
        public void Configure(EntityTypeBuilder<HotelModels> builder)
        {
            builder.HasKey(x => x.UUID);

            builder.Property(x => x.PersonName).IsRequired();
            builder.Property(x => x.PersonSurname).IsRequired();
            builder.Property(x => x.CompanyName).IsRequired();
        }
    }
}
