namespace MyTelegram.Domain.Commands.Messaging;

public class AddInboxMessageIdToOutboxMessageCommand : RequestCommand2<MessageAggregate, MessageId,
    IExecutionResult>
{
    public AddInboxMessageIdToOutboxMessageCommand(MessageId aggregateId,
        RequestInfo requestInfo,
        long inboxOwnerPeerId,
        int inboxMessageId) : base(aggregateId, requestInfo)
    {
        InboxOwnerPeerId = inboxOwnerPeerId;
        InboxMessageId = inboxMessageId;
    }

    public int InboxMessageId { get; }

    public long InboxOwnerPeerId { get; }
}

public class AddInboxItemsToOutboxMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public List<InboxItem> InboxItems { get; }

    public AddInboxItemsToOutboxMessageCommand(MessageId aggregateId,
        RequestInfo requestInfo,
        List<InboxItem> inboxItems) : base(aggregateId, requestInfo)
    {
        InboxItems = inboxItems;
    }
}