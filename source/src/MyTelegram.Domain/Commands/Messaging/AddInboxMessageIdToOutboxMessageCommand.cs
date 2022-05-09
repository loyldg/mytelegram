namespace MyTelegram.Domain.Commands.Messaging;

public class AddInboxMessageIdToOutboxMessageCommand : Command<MessageAggregate, MessageId,
    IExecutionResult>, IHasCorrelationId
{
    public AddInboxMessageIdToOutboxMessageCommand(MessageId aggregateId,
        long inboxOwnerPeerId,
        int inboxMessageId,
        Guid correlationId) : base(aggregateId)
    {
        InboxOwnerPeerId = inboxOwnerPeerId;
        InboxMessageId = inboxMessageId;
        CorrelationId = correlationId;
    }

    public int InboxMessageId { get; }

    public long InboxOwnerPeerId { get; }
    public Guid CorrelationId { get; }
}