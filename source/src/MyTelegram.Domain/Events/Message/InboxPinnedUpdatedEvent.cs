// namespace MyTelegram.Domain.Events.Message;

// public class InboxPinnedUpdatedEvent : AggregateEvent<MessageBoxAggregate, MessageBoxId>, IHasCorrelationId
// {
//     public InboxPinnedUpdatedEvent(
//         long ownerPeerId,
//         int messageId,
//         //long channelId,
//         bool pinned,
//         bool pmOneSide,
//         bool silent,
//         int date,
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
//         ToPeerType = toPeerType;
//         ToPeerId = toPeerId;
//         Pts = pts;
//         CorrelationId = correlationId;
//     }

//     public int Date { get; }

//     public int MessageId { get; }

//     public long OwnerPeerId { get; }

//     //public long ChannelId { get; }
//     public bool Pinned { get; }
//     public bool PmOneSide { get; }
//     public int Pts { get; }
//     public bool Silent { get; }
//     public long ToPeerId { get; }
//     public PeerType ToPeerType { get; }
//     public Guid CorrelationId { get; }
// }
