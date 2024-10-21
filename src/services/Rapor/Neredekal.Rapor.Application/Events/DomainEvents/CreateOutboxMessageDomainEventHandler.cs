using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Neredekal.Common.Domain.DomainEvents;
using Neredekal.Common.Domain.OutboxModel;
using Neredekal.Rapor.Application.Abstractions.Repositories;

namespace Neredekal.Rapor.Application.Events.DomainEvents
{
    internal class CreateOutboxMessageDomainEventHandler : INotificationHandler<CreateOutboxMessageDomainEvent>
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public CreateOutboxMessageDomainEventHandler(IOutboxMessageRepository outboxMessageRepository)
        {
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task Handle(CreateOutboxMessageDomainEvent notification, CancellationToken cancellationToken)
        {
            var outboxMessage = OutboxMessage.Create(Guid.NewGuid(), notification.Type, notification.Content);

            await _outboxMessageRepository.Create(outboxMessage);
        }
    }
}
