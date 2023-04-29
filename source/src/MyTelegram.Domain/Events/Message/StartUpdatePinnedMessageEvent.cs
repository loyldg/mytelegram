// namespace MyTelegram.Domain.Events.Message;

// public class StartUpdatePinnedMessageEvent : RequestAggregateEvent<MessageBoxAggregate, MessageBoxId>, IHasCorrelationId
// {
//     public StartUpdatePinnedMessageEvent(
//         long reqMsgId,
//         long selfAuthKeyId,
//         long selfPermAuthKeyId,
//         long selfUserId,
//         long ownerPeerId,
//         int messageId,
//         //long channelId,
//         bool pinned,
//         bool pmOneSide,
//         bool silent,
//         int date,
//         bool isOut,
//         IReadOnlyList<InboxItem> inboxItems,
//         long senderPeerId,
//         int senderMessageId,
//         PeerType toPeerType,
//         long toPeerId,
//         long randomId,
//         string messageActionData,
//         Guid correlationId) : base(reqMsgId)
//     {
//         SelfAuthKeyId = selfAuthKeyId;
//         SelfPermAuthKeyId = selfPermAuthKeyId;
//         SelfUserId = selfUserId;
//         OwnerPeerId = ownerPeerId;
//         MessageId = messageId;
//         //ChannelId = channelId;
//         Pinned = pinned;
//         PmOneSide = pmOneSide;
//         Silent = silent;
//         Date = date;
//         IsOut = isOut;
//         InboxItems = inboxItems;
//         SenderPeerId = senderPeerId;
//         SenderMessageId = senderMessageId;
//         ToPeerType = toPeerType;
//         ToPeerId = toPeerId;
//         RandomId = randomId;
//         MessageActionData = messageActionData;
//         CorrelationId = correlationId;
//     }

//     public int Date { get; }
//     public IReadOnlyList<InboxItem> InboxItems { get; }

//     public bool IsOut { get; }
//     public string MessageActionData { get; }

//     public int MessageId { get; }

//     public long OwnerPeerId { get; }

//     //public long ChannelId { get; }
//     public bool Pinned { get; }
//     public bool PmOneSide { get; }
//     public long RandomId { get; }

//     public long SelfAuthKeyId { get; }
//     public long SelfPermAuthKeyId { get; }
//     public long SelfUserId { get; }
//     public int SenderMessageId { get; }
//     public long SenderPeerId { get; }
//     public bool Silent { get; }
//     public long ToPeerId { get; }
//     public PeerType ToPeerType { get; }
//     public Guid CorrelationId { get; }
// }


