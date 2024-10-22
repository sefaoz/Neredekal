using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neredekal.Common.Domain.SeedWorks;
using Neredekal.Hotel.Domain.AggregateModels.HotelModels;

namespace Neredekal.Hotel.Application.Abstractions.Repositories
{
    public interface IHotelContactInfoRepository : IUnitOfWorks
    {
        Task Create(HotelContactInfoItems contactInfo);
        Task Delete(Guid id);
        Task<HotelContactInfoItems> Get(Guid id);
        Task<List<HotelContactInfoItems>> GetHotelContactInfoItems(Guid hotelId);
    }
}
