namespace MyTelegram.Domain.Commands.Messaging;

public class StartSendMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public StartSendMessageCommand(MessageId aggregateId,
        RequestInfo requestInfo,
        MessageItem outMessageItem,
        bool clearDraft = false,
        int groupItemCount = 1,
        Guid correlationId = default,
        bool forwardFromLinkedChannel = false) : base(aggregateId, requestInfo)
    {
        OutMessageItem = outMessageItem;
        ClearDraft = clearDraft;
        GroupItemCount = groupItemCount;
        CorrelationId = correlationId;
        ForwardFromLinkedChannel = forwardFromLinkedChannel;
    }

    public MessageItem OutMessageItem { get; }
    public bool ClearDraft { get; }
    public int GroupItemCount { get; }
    public Guid CorrelationId { get; }
    public bool ForwardFromLinkedChannel { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(OutMessageItem.RandomId);
    }
}
