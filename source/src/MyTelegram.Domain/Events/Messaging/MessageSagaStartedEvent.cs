using MyTelegram.Domain.Sagas;

namespace MyTelegram.Domain.Events.Messaging;

public class MessageSagaStartedEvent : RequestAggregateEvent2<MessageSaga, MessageSagaId>
{
    public MessageItem MessageItem { get; }
    public List<long>? MentionedUserIds { get; }
    public bool ClearDraft { get; }
    public int GroupItemCount { get; }
    public bool ForwardFromLinkedChannel { get; }


    public MessageSagaStartedEvent(RequestInfo requestInfo, MessageItem messageItem, List<long>? mentionedUserIds, bool clearDraft, int groupItemCount, bool forwardFromLinkedChannel) : base(requestInfo)
    {
        MessageItem = messageItem;
        MentionedUserIds = mentionedUserIds;
        ClearDraft = clearDraft;
        GroupItemCount = groupItemCount;
        ForwardFromLinkedChannel = forwardFromLinkedChannel;

    }
}