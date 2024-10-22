using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Neredekal.Common.Domain.SeedWorks;
using Neredekal.Hotel.Application.Abstractions.Repositories;
using Neredekal.Hotel.Domain.AggregateModels.HotelModels;

namespace Neredekal.Hotel.Insfrastructure.Persistence.Repositories
{
    internal class HotelContactInfoRepository : IHotelContactInfoRepository
    {
        private readonly HotelDbContext _context;

        public HotelContactInfoRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task Create(HotelContactInfoItems contactInfo)
        {
            await _context.Set<HotelContactInfoItems>().AddAsync(contactInfo);
        }

        public async Task Delete(Guid id)
        {
            var contactItem = await _context.Set<HotelContactInfoItems>().FirstOrDefaultAsync(x => x.UUID == id);
            if (contactItem != null)
            {
                _context.Set<HotelContactInfoItems>().Remove(contactItem);
            }
        }

        async Task<HotelContactInfoItems> IHotelContactInfoRepository.Get(Guid id)
        {
            return await _context.Set<HotelContactInfoItems>().FirstOrDefaultAsync(x => x.UUID == id);
        }

        public async Task<List<HotelContactInfoItems>> GetHotelContactInfoItems(Guid hotelId)
        {
            return await _context.Set<HotelContactInfoItems>().Where(x => x.HotelId == hotelId).ToListAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
