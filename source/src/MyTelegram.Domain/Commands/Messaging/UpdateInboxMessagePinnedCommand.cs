namespace MyTelegram.Domain.Commands.Messaging;

public class UpdateInboxMessagePinnedCommand : Command<MessageAggregate, MessageId, IExecutionResult>
{
    public UpdateInboxMessagePinnedCommand(MessageId aggregateId,
        bool pinned,
        bool pmOneSide,
        bool silent,
        int date,
        Guid correlationId) : base(aggregateId)
    {
        Pinned = pinned;
        PmOneSide = pmOneSide;
        Silent = silent;
        Date = date;
        CorrelationId = correlationId;
    }

    public bool Pinned { get; }
    public bool PmOneSide { get; }
    public bool Silent { get; }
    public int Date { get; }
    public Guid CorrelationId { get; }
}
