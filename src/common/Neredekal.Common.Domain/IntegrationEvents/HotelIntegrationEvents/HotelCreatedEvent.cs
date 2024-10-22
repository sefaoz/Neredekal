using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Common.Domain.IntegrationEvents.HotelIntegrationEvents
{
    public record HotelCreatedEvent(Guid id, List<HotelContactInfoItemDto> Items) : IIntegrationEvent;

    public class HotelContactInfoItemDto
    {
        public int InformationType { get; set; }
        public string InformationContent { get; set; }
    }
}
