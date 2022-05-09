// using EventFlow.Aggregates.ExecutionResults;
// using MyTelegram.Domain.Events;

// namespace MyTelegram.Domain.Commands.Message;

// public class UpdateOutboxPinnedMessageCommand : Command<MessageBoxAggregate, MessageBoxId, IExecutionResult>,
//     IHasCorrelationId
// {
//     public UpdateOutboxPinnedMessageCommand(MessageBoxId aggregateId,
//         //long reqMsgId,
//         //int messageId,
//         bool silent,
//         bool pinned,
//         bool pmOneSide,
//         int date,
//         Guid correlationId) : base(aggregateId)
//     {
//         //MessageId = messageId;
//         Silent = silent;
//         Pinned = pinned;
//         PmOneSide = pmOneSide;
//         Date = date;
//         CorrelationId = correlationId;
//     }

//     public int Date { get; }
//     public bool Pinned { get; }

//     public bool PmOneSide { get; }

//     //public int MessageId { get; }
//     public bool Silent { get; }

//     public Guid CorrelationId { get; }
// }
