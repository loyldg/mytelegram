// using EventFlow.Aggregates.ExecutionResults;
// using MyTelegram.Domain.Events;

// namespace MyTelegram.Domain.Commands.Message;

// public class StartUpdatePinMessageCommand : RequestCommand<MessageBoxAggregate, MessageBoxId, IExecutionResult>,
//     IHasCorrelationId
// {
//     public StartUpdatePinMessageCommand(MessageBoxId aggregateId,
//         long reqMsgId,
//         long selfAuthKeyId,
//         long selfPermAuthKeyId,
//         long selfUserId,
//         //int messageId,
//         bool silent,
//         bool pinned,
//         bool pmOneSide,
//         int date,
//         string messageActionData,
//         long randomId,
//         Guid correlationId
//     ) : base(aggregateId, reqMsgId)
//     {
//         //MessageId = messageId;
//         SelfAuthKeyId = selfAuthKeyId;
//         SelfPermAuthKeyId = selfPermAuthKeyId;
//         SelfUserId = selfUserId;
//         Silent = silent;
//         Pinned = pinned;
//         PmOneSide = pmOneSide;
//         Date = date;
//         MessageActionData = messageActionData;
//         RandomId = randomId;
//         CorrelationId = correlationId;
//     }

//     public int Date { get; }
//     public string MessageActionData { get; }
//     public bool Pinned { get; }
//     public bool PmOneSide { get; }

//     public long RandomId { get; }

//     //public int MessageId { get; }
//     public long SelfAuthKeyId { get; }
//     public long SelfPermAuthKeyId { get; }
//     public long SelfUserId { get; }
//     public bool Silent { get; }
//     public Guid CorrelationId { get; }
// }
