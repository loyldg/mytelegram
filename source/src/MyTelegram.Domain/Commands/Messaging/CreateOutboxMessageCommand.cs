namespace MyTelegram.Domain.Commands.Messaging;

public class CreateOutboxMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public CreateOutboxMessageCommand(MessageId aggregateId,
        RequestInfo requestInfo,
        MessageItem outboxMessageItem,
        List<long>? mentionedUserIds = null,
        List<ReplyToMsgItem>? replyToMsgItems = null,
        bool clearDraft = true,
        int groupItemCount = 1,
        long? linkedChannelId = null,
        List<long>? chatMembers = null) : base(aggregateId, requestInfo)
    {
        OutboxMessageItem = outboxMessageItem;
        MentionedUserIds = mentionedUserIds;
        ReplyToMsgItems = replyToMsgItems;
        ClearDraft = clearDraft;
        GroupItemCount = groupItemCount;
        LinkedChannelId = linkedChannelId;
        ChatMembers = chatMembers;
    }

    //public long ReqMsgId { get; }
    public MessageItem OutboxMessageItem { get; }
    public List<long>? MentionedUserIds { get; }
    public List<ReplyToMsgItem>? ReplyToMsgItems { get; }
    public bool ClearDraft { get; }
    public int GroupItemCount { get; }
    public long? LinkedChannelId { get; }
    public List<long>? ChatMembers { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(RequestInfo.ReqMsgId);
        yield return BitConverter.GetBytes(OutboxMessageItem.RandomId);
    }
}