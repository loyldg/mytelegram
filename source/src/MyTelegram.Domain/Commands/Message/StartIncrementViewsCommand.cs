// using EventFlow.Aggregates.ExecutionResults;
// using MyTelegram.Domain.Events;

// namespace MyTelegram.Domain.Commands.Message;

// public class StartIncrementViewsCommand : RequestCommand<MessageBoxAggregate, MessageBoxId, IExecutionResult>,
//     IHasCorrelationId
// {
//     public StartIncrementViewsCommand(MessageBoxId aggregateId,
//         long reqMsgId,
//         //long channelId,
//         long userId,
//         IList<int> messageIdList,
//         Guid correlationId) : base(aggregateId, reqMsgId)
//     {
//         //ChannelId = channelId;
//         UserId = userId;
//         MessageIdList = messageIdList;
//         CorrelationId = correlationId;
//     }

//     public IList<int> MessageIdList { get; }

//     //public long ChannelId { get; }
//     public long UserId { get; }

//     public Guid CorrelationId { get; }
// }


