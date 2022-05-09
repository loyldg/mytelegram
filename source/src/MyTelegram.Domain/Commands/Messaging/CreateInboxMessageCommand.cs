namespace MyTelegram.Domain.Commands.Messaging;

public class CreateInboxMessageCommand : Command<MessageAggregate, MessageId, IExecutionResult>, IHasCorrelationId
{
    public CreateInboxMessageCommand(MessageId aggregateId, MessageItem inboxMessageItem, int senderMessageId,
        Guid correlationId) : base(aggregateId)
    {
        InboxMessageItem = inboxMessageItem;
        SenderMessageId = senderMessageId;
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; }
    public MessageItem InboxMessageItem { get; }
    public int SenderMessageId { get; }
}