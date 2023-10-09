namespace MyTelegram.Domain.Commands.Messaging;

public class CreateInboxMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public CreateInboxMessageCommand(MessageId aggregateId,
        RequestInfo requestInfo,
        MessageItem inboxMessageItem,
        int senderMessageId) : base(aggregateId, requestInfo)
    {
        InboxMessageItem = inboxMessageItem;
        SenderMessageId = senderMessageId;
    }

    public MessageItem InboxMessageItem { get; }
    public int SenderMessageId { get; }
}