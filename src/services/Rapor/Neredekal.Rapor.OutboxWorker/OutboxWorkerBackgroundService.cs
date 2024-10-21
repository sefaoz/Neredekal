using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Neredekal.Common.Domain.OutboxModel;
using Neredekal.Rapor.Infrastructure.Persistence;

namespace Neredekal.Rapor.OutboxWorker
{
    public class OutboxWorkerBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public OutboxWorkerBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var serviceScope = _serviceScopeFactory.CreateScope();
                var reportDbContext = serviceScope.ServiceProvider.GetRequiredService<ReportDbContext>();
                var publishEndpoint = serviceScope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                var outboxMessages = reportDbContext
                    .OutboxMessage
                    .Where(x => x.Status == OutboxMessageStatus.Pending)
                    .Skip(0)
                    .Take(5)
                    .ToList();

                foreach (var outboxMessage in outboxMessages)
                {
                    var eventType = Type.GetType(outboxMessage.Type);

                    var @event = System.Text.Json.JsonSerializer.Deserialize(outboxMessage.Content, eventType);
                    if (@event != null)
                        await publishEndpoint.Publish(@event, CancellationToken.None);
                    outboxMessage.SetStatusPublished();

                    await reportDbContext.SaveChangesAsync();

                    await Task.Delay(30_000);
                }
            }
        }
    }
}
