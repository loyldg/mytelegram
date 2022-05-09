// using EventFlow.Aggregates.ExecutionResults;
// using MyTelegram.Domain.Events;

// namespace MyTelegram.Domain.Commands.Message;

// public class ReplyToMessageCommand : Command<MessageBoxAggregate, MessageBoxId, IExecutionResult>, IHasCorrelationId
// {
//     public ReplyToMessageCommand(MessageBoxId aggregateId,
//         Guid correlationId,
//         long userId) : base(aggregateId)
//     {
//         CorrelationId = correlationId;
//         UserId = userId;
//     }

//     public long UserId { get; }
//     public Guid CorrelationId { get; }
// }
