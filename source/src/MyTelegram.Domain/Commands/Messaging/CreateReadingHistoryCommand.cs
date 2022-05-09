namespace MyTelegram.Domain.Commands.Messaging;

public class CreateReadingHistoryCommand : Command<ReadingHistoryAggregate, ReadingHistoryId, IExecutionResult>
{
    public long ReaderPeerId { get; }
    public long TargetPeerId { get; }
    public int MessageId { get; }

    public CreateReadingHistoryCommand(ReadingHistoryId aggregateId,
        /*RequestInfo request,*/ long readerPeerId,
        long targetPeerId,
        int messageId) : base(aggregateId)
    {
        ReaderPeerId = readerPeerId;
        TargetPeerId = targetPeerId;
        MessageId = messageId;
    }
}