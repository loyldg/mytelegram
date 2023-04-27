namespace MyTelegram.Domain.Commands.Messaging;

public class CreateReadingHistoryCommand : Command<ReadingHistoryAggregate, ReadingHistoryId, IExecutionResult>
{
    public long ReaderPeerId { get; }
    public long TargetPeerId { get; }
    public int MessageId { get; }
    public int Date { get; }

    public CreateReadingHistoryCommand(ReadingHistoryId aggregateId,
        /*RequestInfo requestInfo,*/ long readerPeerId,
        long targetPeerId,
        int messageId,
        int date
        ) : base(aggregateId)
    {
        ReaderPeerId = readerPeerId;
        TargetPeerId = targetPeerId;
        MessageId = messageId;
        Date = date;
    }
}