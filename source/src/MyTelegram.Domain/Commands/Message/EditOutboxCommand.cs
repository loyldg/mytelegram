// using EventFlow.Aggregates.ExecutionResults;
// using MyTelegram.Domain.Events;

// namespace MyTelegram.Domain.Commands.Message;

// public class EditOutboxCommand : RequestCommand<MessageBoxAggregate, MessageBoxId, IExecutionResult>,
//     IHasCorrelationId
// {
//     public EditOutboxCommand(MessageBoxId aggregateId,
//         long reqMsgId,
//         long selfAuthKeyId,
//         long userId,
//         int messageId,
//         string newMessage,
//         byte[]? entities,
//         int date,
//         byte[]? media,
//         Guid correlationId) : base(aggregateId, reqMsgId)
//     {
//         SelfAuthKeyId = selfAuthKeyId;
//         UserId = userId;
//         MessageId = messageId;
//         NewMessage = newMessage;
//         Entities = entities;
//         Date = date;
//         Media = media;
//         CorrelationId = correlationId;
//     }

//     public int Date { get; }
//     public byte[]? Entities { get; }
//     public byte[]? Media { get; }

//     public int MessageId { get; }
//     public string NewMessage { get; }

//     public long SelfAuthKeyId { get; }
//     public long UserId { get; }
//     public Guid CorrelationId { get; }
// }


