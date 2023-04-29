// namespace MyTelegram.Domain.Events.Message;

// public class StartDeleteMessagesEvent : RequestAggregateEvent<MessageBoxAggregate, MessageBoxId>, IHasCorrelationId
// {
//     public StartDeleteMessagesEvent(
//         long reqMsgId,
//         long selfAuthKeyId,
//         long selfUserId,
//         long ownerPeerId,
//         bool isOut,
//         long senderPeerId,
//         int senderMessageId,
//         PeerType toPeerType,
//         long toPeerId,
//         IReadOnlyList<int> idList,
//         bool revoke,
//         IReadOnlyList<InboxItem> inboxItems,
//         Guid correlationId) : base(reqMsgId)
//     {
//         SelfAuthKeyId = selfAuthKeyId;
//         SelfUserId = selfUserId;
//         OwnerPeerId = ownerPeerId;
//         IsOut = isOut;
//         SenderPeerId = senderPeerId;
//         SenderMessageId = senderMessageId;
//         ToPeerType = toPeerType;
//         ToPeerId = toPeerId;
//         IdList = idList;
//         Revoke = revoke;
//         InboxItems = inboxItems;
//         CorrelationId = correlationId;
//     }

//     public IReadOnlyList<int> IdList { get; }
//     public IReadOnlyList<InboxItem> InboxItems { get; }
//     public bool IsOut { get; }
//     public long OwnerPeerId { get; }
//     public bool Revoke { get; }

//     public long SelfAuthKeyId { get; }
//     public long SelfUserId { get; }
//     public int SenderMessageId { get; }
//     public long SenderPeerId { get; }
//     public long ToPeerId { get; }
//     public PeerType ToPeerType { get; }
//     public Guid CorrelationId { get; }
// }


