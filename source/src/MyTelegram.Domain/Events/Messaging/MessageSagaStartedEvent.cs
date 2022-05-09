using MyTelegram.Domain.Sagas;

namespace MyTelegram.Domain.Events.Messaging;

public class MessageSagaStartedEvent : RequestAggregateEvent2<MessageSaga, MessageSagaId>
{
    public MessageItem MessageItem { get; }
    public bool ClearDraft { get; }
    public int GroupItemCount { get; }
    public Guid CorrelationId { get; }

    public MessageSagaStartedEvent(RequestInfo request, MessageItem messageItem, bool clearDraft, int groupItemCount, Guid correlationId) : base(request)
    {
        MessageItem = messageItem;
        ClearDraft = clearDraft;
        GroupItemCount = groupItemCount;
        CorrelationId = correlationId;
    }
}