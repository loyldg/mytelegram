// namespace MyTelegram.Domain.Events.Message;

// public class OutboxPinnedUpdatedEvent : AggregateEvent<MessageBoxAggregate, MessageBoxId>, IHasCorrelationId
// {
//     public OutboxPinnedUpdatedEvent(
//         long ownerPeerId,
//         int messageId,
//         //long channelId,
//         bool pinned,
//         bool pmOneSide,
//         bool silent,
//         int date,
//         IReadOnlyList<InboxItem> inboxItems,
//         long senderPeerId,
//         int senderMessageId,
//         PeerType toPeerType,
//         long toPeerId,
//         int pts,
//         Guid correlationId)
//     {
//         OwnerPeerId = ownerPeerId;
//         MessageId = messageId;
//         //ChannelId = channelId;
//         Pinned = pinned;
//         PmOneSide = pmOneSide;
//         Silent = silent;
//         Date = date;
//         InboxItems = inboxItems;
//         SenderPeerId = senderPeerId;
//         SenderMessageId = senderMessageId;
//         ToPeerType = toPeerType;
//         ToPeerId = toPeerId;
//         Pts = pts;
//         CorrelationId = correlationId;
//     }

//     public int Date { get; }
//     public IReadOnlyList<InboxItem> InboxItems { get; }

//     public int MessageId { get; }

//     public long OwnerPeerId { get; }

//     //public long ChannelId { get; }
//     public bool Pinned { get; }
//     public bool PmOneSide { get; }
//     public int Pts { get; }
//     public int SenderMessageId { get; }
//     public long SenderPeerId { get; }
//     public bool Silent { get; }
//     public long ToPeerId { get; }
//     public PeerType ToPeerType { get; }
//     public Guid CorrelationId { get; }
// }
