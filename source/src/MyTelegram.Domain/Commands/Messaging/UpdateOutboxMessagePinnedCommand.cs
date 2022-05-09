namespace MyTelegram.Domain.Commands.Messaging;

public class UpdateOutboxMessagePinnedCommand : Command<MessageAggregate, MessageId, IExecutionResult>
{
    public bool Pinned { get; }
    public bool PmOneSize { get; }
    public bool Silent { get; }
    public int Date { get; }
    public Guid CorrelationId { get; }

    public UpdateOutboxMessagePinnedCommand(MessageId aggregateId, bool pinned,
        bool pmOneSize,
        bool silent,
        int date,
        Guid correlationId) : base(aggregateId)
    {
        Pinned = pinned;
        PmOneSize = pmOneSize;
        Silent = silent;
        Date = date;
        CorrelationId = correlationId;
    }
}