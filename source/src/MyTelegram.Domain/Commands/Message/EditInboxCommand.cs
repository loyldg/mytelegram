// using EventFlow.Aggregates.ExecutionResults;
// using MyTelegram.Domain.Events;

// namespace MyTelegram.Domain.Commands.Message;

// public class EditInboxCommand : Command<MessageBoxAggregate, MessageBoxId, IExecutionResult>,
//     IHasCorrelationId
// {
//     public EditInboxCommand(MessageBoxId aggregateId,
//         //long reqMsgId,
//         long inboxOwnerPeerId,
//         int messageId,
//         string newMessage,
//         byte[]? entities,
//         int date,
//         byte[]? media,
//         Guid correlationId) : base(aggregateId)
//     {
//         InboxOwnerPeerId = inboxOwnerPeerId;
//         MessageId = messageId;
//         NewMessage = newMessage;
//         Entities = entities;
//         Date = date;
//         Media = media;
//         CorrelationId = correlationId;
//     }

//     public int Date { get; }
//     public byte[]? Entities { get; }
//     public long InboxOwnerPeerId { get; }
//     public byte[]? Media { get; }

//     public int MessageId { get; }
//     public string NewMessage { get; }
//     public Guid CorrelationId { get; }
// }
