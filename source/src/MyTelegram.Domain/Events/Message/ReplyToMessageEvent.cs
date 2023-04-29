// namespace MyTelegram.Domain.Events.Message;

// public class ReplyToMessageEvent : AggregateEvent<MessageBoxAggregate, MessageBoxId>, IHasCorrelationId
// {
//     public ReplyToMessageEvent(IReadOnlyList<InboxItem> inboxItems,
//         Guid correlationId)
//     {
//         InboxItems = inboxItems;
//         CorrelationId = correlationId;
//     }

//     //public ReplyToMessageEvent(int replyToOwnerPeerId,
//     //    int messageId,
//     //    long toPeerId,
//     //    Guid correlationId)
//     //{
//     //    ReplyToOwnerPeerId = replyToOwnerPeerId;
//     //    MessageId = messageId;
//     //    ToPeerId = toPeerId;
//     //    CorrelationId = correlationId;
//     //}

//     //public int ReplyToOwnerPeerId { get; }
//     //public int MessageId { get; }
//     //public long ToPeerId { get; }

//     public IReadOnlyList<InboxItem> InboxItems { get; }
//     public Guid CorrelationId { get; }
// }


