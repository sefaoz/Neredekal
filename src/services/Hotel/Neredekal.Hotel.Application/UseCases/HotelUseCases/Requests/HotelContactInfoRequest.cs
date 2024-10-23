using Neredekal.Hotel.Domain.AggregateModels.HotelModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Hotel.Application.UseCases.HotelUseCases.Requests
{
    public class HotelContactInfoRequest
    {
        public required InformationTypeEnum InformationType { get; set; }
        public required string InformationContent { get; set; }
    }
}
