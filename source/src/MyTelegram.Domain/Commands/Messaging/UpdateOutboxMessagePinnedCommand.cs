namespace MyTelegram.Domain.Commands.Messaging;

public class UpdateOutboxMessagePinnedCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public bool Pinned { get; }
    public bool PmOneSize { get; }
    public bool Silent { get; }
    public int Date { get; }

    public UpdateOutboxMessagePinnedCommand(MessageId aggregateId,
        RequestInfo requestInfo,
        bool pinned,
        bool pmOneSize,
        bool silent,
        int date) : base(aggregateId, requestInfo)
    {
        Pinned = pinned;
        PmOneSize = pmOneSize;
        Silent = silent;
        Date = date;
    }
}