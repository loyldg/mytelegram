// using System.Text;
// using EventFlow.Aggregates.ExecutionResults;
// using MyTelegram.Domain.Events;

// namespace MyTelegram.Domain.Commands.Message;

// public class ReadInboxCommand : DistinctCommand<MessageBoxAggregate, MessageBoxId, IExecutionResult>, IHasCorrelationId
// {
//     public ReadInboxCommand(MessageBoxId aggregateId,
//         long reqMsgId,
//         long readerUid,
//         //long toPeerId,
//         //int readerMaxMessageId,
//         string sourceCommandId,
//         Guid correlationId
//     ) : base(aggregateId)
//     {
//         ReaderUid = readerUid;
//         SourceCommandId = sourceCommandId;
//         //ToPeerId = toPeerId;
//         //ReaderMaxMessageId = readerMaxMessageId;
//         CorrelationId = correlationId;
//         ReqMsgId = reqMsgId;
//     }

//     public long ReaderUid { get; }

//     public long ReqMsgId { get; }
//     public string SourceCommandId { get; }
//     public Guid CorrelationId { get; }

//     protected override IEnumerable<byte[]> GetSourceIdComponents()
//     {
//         //yield return BitConverter.GetBytes(ReaderUid);
//         yield return Encoding.UTF8.GetBytes(SourceCommandId);
//     }
// }
