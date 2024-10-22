using MediatR;
using Microsoft.EntityFrameworkCore;
using Neredekal.Common.Domain.SeedWorks;
using Neredekal.Hotel.Domain.AggregateModels.HotelModels;
using Neredekal.Hotel.Insfrastructure.Persistence.Configurations;

namespace Neredekal.Hotel.Insfrastructure.Persistence
{
    public class HotelDbContext : DbContext, IUnitOfWorks
    {
        private readonly IMediator _mediator;

        public HotelDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Domain.AggregateModels.HotelModels.Hotel> Hotels { get; set; }
        public DbSet<HotelContactInfoItems> HotelContactInfoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
            modelBuilder.ApplyConfiguration(new HotelContactInfoConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = this.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(x => x.Entity._domainEvents != null && x.Entity._domainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity._domainEvents)
                .ToList();

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
