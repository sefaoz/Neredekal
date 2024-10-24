using MediatR;
using Neredekal.Common.Domain.DomainEvents;
using Neredekal.Common.Domain.IntegrationEvents.ReportEvents;
using Neredekal.Common.Domain.OutboxModel;
using Neredekal.Rapor.Application.Abstractions.Repositories;
using Neredekal.Rapor.Domain.AggregateModels.RaporModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Rapor.Application.UseCase.RaporUseCases.Commands
{
    public record CreateReportDetailCommand : IRequest;

    public class CreateRaporDetailCommandHandler : IRequestHandler<CreateReportDetailCommand>
    {
        private readonly IReportDetailRepository _repository;

        public CreateRaporDetailCommandHandler(IReportDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateReportDetailCommand request, CancellationToken cancellationToken)
        {
            var report = ReportDetail.CreateReportDetail(Guid.NewGuid(),"");
            await _repository.Create(report, cancellationToken);

            var @event = new ReportDetailCreatedIntegrationEvent(report.UUID);

            report.AddDomainEvents(new CreateOutboxMessageDomainEvent(@event.GetType().AssemblyQualifiedName, System.Text.Json.JsonSerializer.Serialize(@event)));

            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
