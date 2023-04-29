// using EventFlow.Aggregates.ExecutionResults;
// using MyTelegram.Domain.Events;

// namespace MyTelegram.Domain.Commands.Message;

// public class StartDeleteMessagesCommand : RequestCommand<MessageBoxAggregate, MessageBoxId, IExecutionResult>,
//     IHasCorrelationId
// {
//     public StartDeleteMessagesCommand(MessageBoxId aggregateId,
//         long reqMsgId,
//         long selfAuthKeyId,
//         long selfUserId,
//         IReadOnlyList<int> idList,
//         bool revoke,
//         //int nextMaxId,
//         //long randomId,
//         //string messageActionData,
//         Guid correlationId) : base(aggregateId, reqMsgId)
//     {
//         SelfAuthKeyId = selfAuthKeyId;
//         SelfUserId = selfUserId;
//         IdList = idList;
//         Revoke = revoke;
//         //NextMaxId = nextMaxId;
//         //RandomId = randomId;
//         //MessageActionData = messageActionData;
//         CorrelationId = correlationId;
//     }

//     public IReadOnlyList<int> IdList { get; }
//     public bool Revoke { get; }

//     public long SelfAuthKeyId { get; }

//     public long SelfUserId { get; }

//     //public int NextMaxId { get; }
//     //public long RandomId { get; }
//     //public string MessageActionData { get; }
//     public Guid CorrelationId { get; }
// }


