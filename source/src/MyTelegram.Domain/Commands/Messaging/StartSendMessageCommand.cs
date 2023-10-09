namespace MyTelegram.Domain.Commands.Messaging;

public class StartSendMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public MessageItem OutMessageItem { get; }
    public List<long>? MentionedUserIds { get; }
    public bool ClearDraft { get; }
    public int GroupItemCount { get; }
    public bool ForwardFromLinkedChannel { get; }

    public StartSendMessageCommand(MessageId aggregateId,
        RequestInfo requestInfo, MessageItem outMessageItem, List<long>? mentionedUserIds = null, bool clearDraft = false, int groupItemCount = 1, bool forwardFromLinkedChannel = false) : base(aggregateId, requestInfo)
    {
        OutMessageItem = outMessageItem;
        MentionedUserIds = mentionedUserIds;
        ClearDraft = clearDraft;
        GroupItemCount = groupItemCount;
        ForwardFromLinkedChannel = forwardFromLinkedChannel;
    }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(OutMessageItem.RandomId);
    }
}