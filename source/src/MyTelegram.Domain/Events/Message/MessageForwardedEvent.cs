// namespace MyTelegram.Domain.Events.Message;

// public class MessageForwardedEvent : AggregateEvent<MessageBoxAggregate, MessageBoxId>, IHasCorrelationId
// {
//     public MessageForwardedEvent(
//         long selfAuthKeyId,
//         long ownerPeerId,
//         long senderPeerId,
//         int messageId,
//         string message,
//         byte[]? entities,
//         int date,
//         string postAuthor,
//         long randomId,
//         MessageBoxType messageBoxType,
//         byte[]? media,
//         int? views,
//         Guid correlationId)
//     {
//         SelfAuthKeyId = selfAuthKeyId;
//         OwnerPeerId = ownerPeerId;
//         SenderPeerId = senderPeerId;
//         MessageId = messageId;
//         Message = message;
//         Entities = entities;
//         Date = date;
//         PostAuthor = postAuthor;
//         RandomId = randomId;
//         MessageBoxType = messageBoxType;
//         Media = media;
//         Views = views;
//         CorrelationId = correlationId;
//     }

//     public int Date { get; }
//     public byte[]? Entities { get; }
//     public byte[]? Media { get; }
//     public string Message { get; }
//     public MessageBoxType MessageBoxType { get; }
//     public int MessageId { get; }
//     public long OwnerPeerId { get; }
//     public string PostAuthor { get; }
//     public long RandomId { get; }

//     public long SelfAuthKeyId { get; }
//     public long SenderPeerId { get; }
//     public int? Views { get; }
//     public Guid CorrelationId { get; }
// }
