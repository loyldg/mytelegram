namespace MyTelegram.Domain.Sagas.Events;

public class SendMessageSagaStartedEvent : RequestAggregateEvent2<SendMessageSaga, SendMessageSagaId>
{
    public MessageItem MessageItem { get; }
    public List<long>? MentionedUserIds { get; }
    public List<ReplyToMsgItem>? ReplyToMsgItems { get; }
    public bool ClearDraft { get; }
    public int GroupItemCount { get; }
    public long? LinkedChannelId { get; }

    public List<long>? ChatMembers { get; }
    //public bool ForwardFromLinkedChannel { get; }

    public SendMessageSagaStartedEvent(RequestInfo requestInfo, MessageItem messageItem, List<long>? mentionedUserIds,
        List<ReplyToMsgItem>? replyToMsgItems,
        bool clearDraft,
        int groupItemCount,
        long? linkedChannelId,
        List<long>? chatMembers) : base(requestInfo)
    {
        MessageItem = messageItem;
        MentionedUserIds = mentionedUserIds;
        ReplyToMsgItems = replyToMsgItems;
        ClearDraft = clearDraft;
        GroupItemCount = groupItemCount;
        LinkedChannelId = linkedChannelId;
        ChatMembers = chatMembers;
        //ForwardFromLinkedChannel = forwardFromLinkedChannel;
    }
}