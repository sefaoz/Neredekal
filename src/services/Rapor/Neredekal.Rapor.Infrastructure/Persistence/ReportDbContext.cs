using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neredekal.Common.Domain.OutboxModel;
using Neredekal.Common.Domain.SeedWorks;
using Neredekal.Rapor.Domain.AggregateModels.RaporModels;
using Neredekal.Rapor.Infrastructure.Persistence.Configurations;

namespace Neredekal.Rapor.Infrastructure.Persistence
{
    public class ReportDbContext : DbContext, IUnitOfWorks
    {
        private readonly IMediator _mediator;

        public ReportDbContext(DbContextOptions options,IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<ReportDetail> ReportDetails { get; set; }
        public DbSet<OutboxMessage> OutboxMessage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ReportDetailConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var domainEntities = this.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(x => x.Entity._domainEvents != null && x.Entity._domainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x=> x.Entity._domainEvents)
                .ToList();

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
