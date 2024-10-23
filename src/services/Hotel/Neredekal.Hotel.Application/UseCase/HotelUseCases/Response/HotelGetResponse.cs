using Neredekal.Hotel.Domain.AggregateModels.HotelModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Hotel.Application.UseCase.HotelUseCases.Response
{
    public class HotelGetResponse
    {
        public Guid Id { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public string CompanyName { get; set; }
        public List<HotelContactInfoDto> HotelContactInfoItems { get; set; }
    }

    public class HotelContactInfoDto
    {
        public Guid Id { get; set; }
        public string InformationType { get; set; }
        public string InformationContent { get; set; }
    }

}
