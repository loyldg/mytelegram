namespace MyTelegram.Domain.Commands.Messaging;

public class ReadInboxHistoryCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public long ReaderUserId { get; }
    public int Date { get; }
    public ReadInboxHistoryCommand(MessageId aggregateId,
        RequestInfo requestInfo, long readerUserId,
        int date) : base(aggregateId, requestInfo)
    {
        ReaderUserId = readerUserId;
        Date = date;
    }
}