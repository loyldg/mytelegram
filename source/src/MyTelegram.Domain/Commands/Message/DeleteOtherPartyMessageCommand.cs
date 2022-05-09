// using EventFlow.Aggregates.ExecutionResults;
// using MyTelegram.Domain.Events;

// namespace MyTelegram.Domain.Commands.Message;

// public class DeleteOtherPartyMessageCommand : Command<MessageBoxAggregate, MessageBoxId, IExecutionResult>,
//     IHasCorrelationId
// {
//     public DeleteOtherPartyMessageCommand(MessageBoxId aggregateId,
//         //long reqMsgId,

//         //bool revoke,
//         Guid correlationId) : base(aggregateId)
//     {
//         //Revoke = revoke;
//         CorrelationId = correlationId;
//     }

//     //public bool Revoke { get; }
//     public Guid CorrelationId { get; }
// }
