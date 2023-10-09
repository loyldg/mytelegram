namespace MyTelegram.Domain.Events.Messaging;

public class OutboxMessageCreatedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>
{
    public OutboxMessageCreatedEvent(RequestInfo requestInfo, MessageItem outboxMessageItem,
        List<long>? mentionedUserIds,
        List<ReplyToMsgItem>? replyToMsgItems,
        bool clearDraft, int groupItemCount, long? linkedChannelId,
        List<long>? chatMembers) : base(requestInfo)
    {
        OutboxMessageItem = outboxMessageItem;
        MentionedUserIds = mentionedUserIds;
        ReplyToMsgItems = replyToMsgItems;
        ClearDraft = clearDraft;
        GroupItemCount = groupItemCount;

        LinkedChannelId = linkedChannelId;
        ChatMembers = chatMembers;
    }

    public MessageItem OutboxMessageItem { get; }
    public List<long>? MentionedUserIds { get; }
    public List<ReplyToMsgItem>? ReplyToMsgItems { get; }
    public bool ClearDraft { get; }
    public int GroupItemCount { get; }
    public long? LinkedChannelId { get; }
    public List<long>? ChatMembers { get; }
}