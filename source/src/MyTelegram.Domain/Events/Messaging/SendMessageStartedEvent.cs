namespace MyTelegram.Domain.Events.Messaging;

public class SendMessageStartedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>
{
    public MessageItem OutMessageItem { get; }
    public List<long>? MentionedUserIds { get; }
    public bool ClearDraft { get; }
    public int GroupItemCount { get; }
    public bool ForwardFromLinkedChannel { get; }


    public SendMessageStartedEvent(RequestInfo requestInfo, MessageItem outMessageItem, List<long>? mentionedUserIds, bool clearDraft, int groupItemCount, bool forwardFromLinkedChannel) : base(requestInfo)
    {
        OutMessageItem = outMessageItem;
        MentionedUserIds = mentionedUserIds;
        ClearDraft = clearDraft;
        GroupItemCount = groupItemCount;
        ForwardFromLinkedChannel = forwardFromLinkedChannel;

    }
}