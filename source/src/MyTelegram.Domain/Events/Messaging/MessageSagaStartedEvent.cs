using MyTelegram.Domain.Sagas;

namespace MyTelegram.Domain.Events.Messaging;

public class MessageSagaStartedEvent : RequestAggregateEvent2<MessageSaga, MessageSagaId>
{
    public MessageSagaStartedEvent(RequestInfo requestInfo,
        MessageItem messageItem,
        bool clearDraft,
        int groupItemCount,
        bool forwardFromLinkedChannel,
        Guid correlationId) : base(requestInfo)
    {
        MessageItem = messageItem;
        ClearDraft = clearDraft;
        GroupItemCount = groupItemCount;
        ForwardFromLinkedChannel = forwardFromLinkedChannel;
        CorrelationId = correlationId;
    }

    public MessageItem MessageItem { get; }
    public bool ClearDraft { get; }
    public int GroupItemCount { get; }
    public bool ForwardFromLinkedChannel { get; }
    public Guid CorrelationId { get; }
}
