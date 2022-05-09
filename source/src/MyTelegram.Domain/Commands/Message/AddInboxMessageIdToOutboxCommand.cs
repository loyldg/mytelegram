// using EventFlow.Aggregates.ExecutionResults;

// namespace MyTelegram.Domain.Commands.Message;

// public class
//     AddInboxMessageIdToOutboxCommand : Command<MessageBoxAggregate, MessageBoxId,
//         IExecutionResult> //, IHasCorrelationId
// {
//     public AddInboxMessageIdToOutboxCommand(MessageBoxId aggregateId,
//         long inboxOwnerPeerId,
//         int inboxMessageId //,
//         /*Guid correlationId*/) : base(aggregateId)
//     {
//         InboxOwnerPeerId = inboxOwnerPeerId;
//         InboxMessageId = inboxMessageId;
//         //CorrelationId = correlationId;
//     }

//     public int InboxMessageId { get; }

//     public long InboxOwnerPeerId { get; }
//     //public Guid CorrelationId { get; }
// }
