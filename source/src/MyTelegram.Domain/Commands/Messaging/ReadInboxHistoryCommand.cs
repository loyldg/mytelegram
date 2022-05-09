namespace MyTelegram.Domain.Commands.Messaging;

public class ReadInboxHistoryCommand : RequestCommand<MessageAggregate, MessageId, IExecutionResult>
{
    public long ReaderUid { get; }
    public Guid CorrelationId { get; }

    public ReadInboxHistoryCommand(MessageId aggregateId,
        long reqMsgId, long readerUid,
        Guid correlationId) : base(aggregateId, reqMsgId)
    {
        ReaderUid = readerUid;
        CorrelationId = correlationId;
    }
}