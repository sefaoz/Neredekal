using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Neredekal.Hotel.Application.Abstractions.Repositories;

namespace Neredekal.Hotel.Insfrastructure.Persistence.Repositories
{
    internal class HotelRepository : IHotelRepository
    {
        private readonly HotelDbContext _context;

        public HotelRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task Create(Domain.AggregateModels.HotelModels.Hotel hotel)
        {
            await _context.Set<Domain.AggregateModels.HotelModels.Hotel>().AddAsync(hotel);
        }

        public async Task Delete(Guid id)
        {
            var hotel = await _context.Set<Domain.AggregateModels.HotelModels.Hotel>().FirstOrDefaultAsync(x=> x.UUID == id);

            if (hotel != null)
            {
                _context.Set<Domain.AggregateModels.HotelModels.Hotel>().Remove(hotel);
            }
        }

        public async Task<Domain.AggregateModels.HotelModels.Hotel> Get(Guid id,CancellationToken cancellationToken)
        {
            return await _context.Set<Domain.AggregateModels.HotelModels.Hotel>().Include(x => x.HotelContactInfoItems)
                .FirstOrDefaultAsync(x => x.UUID == id, cancellationToken);
        }

        public async Task<List<Domain.AggregateModels.HotelModels.Hotel>> GetAll()
        {
            return await _context.Set<Domain.AggregateModels.HotelModels.Hotel>().Include(x => x.HotelContactInfoItems)
                .ToListAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
