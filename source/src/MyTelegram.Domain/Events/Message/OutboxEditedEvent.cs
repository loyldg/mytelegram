// namespace MyTelegram.Domain.Events.Message;

// public class OutboxEditedEvent : RequestAggregateEvent<MessageBoxAggregate, MessageBoxId>,
//     IHasCorrelationId
// {
//     public OutboxEditedEvent(long reqMsgId,
//         long selfAuthKeyId,
//         long ownerPeerId,
//         int messageId,
//         bool post,
//         int? views,
//         IReadOnlyList<InboxItem> inboxMessageIdList,
//         string newMessage,
//         byte[]? entities,
//         long senderPeerId,
//         int senderMessageId,
//         int date,
//         PeerType toPeerType,
//         long toPeerId,
//         byte[]? media,
//         Guid correlationId) : base(reqMsgId)
//     {
//         SelfAuthKeyId = selfAuthKeyId;
//         OwnerPeerId = ownerPeerId;
//         MessageId = messageId;
//         Post = post;
//         Views = views;
//         InboxMessageIdList = inboxMessageIdList;
//         NewMessage = newMessage;
//         Entities = entities;
//         SenderPeerId = senderPeerId;
//         SenderMessageId = senderMessageId;
//         Date = date;
//         ToPeerType = toPeerType;
//         ToPeerId = toPeerId;
//         Media = media;
//         CorrelationId = correlationId;
//     }

//     public int Date { get; }
//     public byte[]? Entities { get; }
//     public IReadOnlyList<InboxItem> InboxMessageIdList { get; }
//     public byte[]? Media { get; }

//     public int MessageId { get; }
//     public string NewMessage { get; }
//     public long OwnerPeerId { get; }
//     public bool Post { get; }

//     public long SelfAuthKeyId { get; }
//     public int SenderMessageId { get; }
//     public long SenderPeerId { get; }
//     public long ToPeerId { get; }
//     public PeerType ToPeerType { get; }
//     public int? Views { get; }
//     public Guid CorrelationId { get; }
// }


