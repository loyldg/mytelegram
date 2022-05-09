// using EventFlow.Aggregates.ExecutionResults;
// using MyTelegram.Domain.Events;

// namespace MyTelegram.Domain.Commands.Message;

// public class ForwardMessageCommand : Command<MessageBoxAggregate, MessageBoxId, IExecutionResult>, IHasCorrelationId
// {
//     public ForwardMessageCommand(MessageBoxId aggregateId,
//         long selfAuthKeyId,
//         long randomId,
//         Guid correlationId
//     ) : base(aggregateId)
//     {
//         SelfAuthKeyId = selfAuthKeyId;
//         RandomId = randomId;
//         CorrelationId = correlationId;
//     }

//     public long RandomId { get; }

//     public long SelfAuthKeyId { get; }
//     public Guid CorrelationId { get; }
// }
