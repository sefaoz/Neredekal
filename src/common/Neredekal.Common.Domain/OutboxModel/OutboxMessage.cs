using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neredekal.Common.Domain.OutboxModel
{
    public class OutboxMessage
    {
        public Guid Id { get; init; }
        public string Type { get; private set; } = string.Empty;
        public string Content { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public DateTime? ProcessedAt { get; private set; }
        public string? Error { get;private set; }
        public OutboxMessageStatus Status { get; private set; }

        protected OutboxMessage() { }

        private OutboxMessage(Guid id, string type, string content, OutboxMessageStatus status)
        {
            Id = id;
            Type= type;
            Content = content;
            Status = status;
            CreatedAt = DateTime.UtcNow;
        }

        public static OutboxMessage Create(Guid id, string type, string content,
            OutboxMessageStatus status = OutboxMessageStatus.Pending)
        {
            return new OutboxMessage(id, type, content, status);
        }

        public void SetStatusPublished()
        {
            Status = OutboxMessageStatus.Published;
            ProcessedAt = DateTime.UtcNow;
        }

        public void SetStatusError(string error)
        {
            Status = OutboxMessageStatus.Error;
            Error = error;
            ProcessedAt = DateTime.UtcNow;
        }
    }
}
