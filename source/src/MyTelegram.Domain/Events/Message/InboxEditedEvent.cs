// namespace MyTelegram.Domain.Events.Message;

// public class InboxEditedEvent : AggregateEvent<MessageBoxAggregate, MessageBoxId>, IHasCorrelationId
// {
//     public InboxEditedEvent(
//         //long reqMsgId,
//         long inboxOwnerPeerId,
//         int messageId,
//         string newMessage,
//         byte[]? entities,
//         int date,
//         PeerType toPeerType,
//         long toPeerId,
//         byte[]? media,
//         Guid correlationId)
//     {
//         InboxOwnerPeerId = inboxOwnerPeerId;
//         MessageId = messageId;
//         NewMessage = newMessage;
//         Entities = entities;
//         Date = date;
//         ToPeerType = toPeerType;
//         ToPeerId = toPeerId;
//         Media = media;
//         CorrelationId = correlationId;
//     }

//     public int Date { get; }
//     public byte[]? Entities { get; }
//     public long InboxOwnerPeerId { get; }
//     public byte[]? Media { get; }

//     public int MessageId { get; }
//     public string NewMessage { get; }
//     public long ToPeerId { get; }
//     public PeerType ToPeerType { get; }
//     public Guid CorrelationId { get; }
// }


