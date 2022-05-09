// namespace MyTelegram.Domain.Events.Message;

// public class IncrementViewsStartedEvent : RequestAggregateEvent<MessageBoxAggregate, MessageBoxId>,
//     IHasCorrelationId
// {
//     public IncrementViewsStartedEvent(long reqMsgId,
//         long channelId,
//         long userId,
//         Guid correlationId,
//         IList<int> messageIdList) : base(reqMsgId)
//     {
//         ChannelId = channelId;
//         UserId = userId;
//         CorrelationId = correlationId;
//         MessageIdList = messageIdList;
//     }

//     public long ChannelId { get; }

//     //public int MessageId { get; }
//     //public int Views { get; }
//     //public bool ShouldIncrementViews { get; }
//     public IList<int> MessageIdList { get; }
//     public long UserId { get; }
//     public Guid CorrelationId { get; }
// }
