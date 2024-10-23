using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neredekal.Common.Domain.SeedWorks;

namespace Neredekal.Hotel.Application.Abstractions.Repositories
{
    public interface IHotelRepository : IUnitOfWorks
    {
        Task Create(Domain.AggregateModels.HotelModels.Hotel hotel, CancellationToken cancellationToken);
        Task Delete(Guid id);
        Task<Domain.AggregateModels.HotelModels.Hotel> Get(Guid id, CancellationToken cancellationToken);
        Task<List<Domain.AggregateModels.HotelModels.Hotel>> GetAll(CancellationToken cancellationToken);
    }
}
