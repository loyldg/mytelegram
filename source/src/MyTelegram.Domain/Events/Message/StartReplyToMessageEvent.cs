// namespace MyTelegram.Domain.Events.Message;

// public class StartReplyToMessageEvent : AggregateEvent<MessageBoxAggregate, MessageBoxId>, IHasCorrelationId
// {
//     public StartReplyToMessageEvent(bool isOut,
//         IReadOnlyList<InboxItem> inboxItems,
//         long senderPeerId,
//         int senderMessageId,
//         long toPeerId,
//         Guid correlationId)

//     {
//         IsOut = isOut;
//         InboxItems = inboxItems;
//         SenderPeerId = senderPeerId;
//         SenderMessageId = senderMessageId;
//         ToPeerId = toPeerId;
//         CorrelationId = correlationId;
//     }

//     public IReadOnlyList<InboxItem> InboxItems { get; }

//     public bool IsOut { get; }
//     public int SenderMessageId { get; }
//     public long SenderPeerId { get; }
//     public long ToPeerId { get; }
//     public Guid CorrelationId { get; }
// }


