// using EventFlow.Aggregates.ExecutionResults;
// using MyTelegram.Domain.Events;

// namespace MyTelegram.Domain.Commands.Message;

// public class SetBoxChannelInfoCommand : Command<MessageBoxAggregate, MessageBoxId, IExecutionResult>, IHasCorrelationId
// {
//     public SetBoxChannelInfoCommand(MessageBoxId aggregateId,
//         bool post,
//         int? views,
//         Guid correlationId) : base(aggregateId)
//     {
//         Post = post;
//         Views = views;
//         CorrelationId = correlationId;
//     }

//     public bool Post { get; }
//     public int? Views { get; }
//     public Guid CorrelationId { get; }
// }
