namespace MyTelegram.Domain.Events.Messaging;

public class SendMessageStartedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>, IHasCorrelationId
{
    public MessageItem OutMessageItem { get; }
    public bool ClearDraft { get; }
    public int GroupItemCount { get; }
    public Guid CorrelationId { get; }

    public SendMessageStartedEvent(RequestInfo request, MessageItem outMessageItem, bool clearDraft, int groupItemCount, Guid correlationId) : base(request)
    {
        OutMessageItem = outMessageItem;
        ClearDraft = clearDraft;
        GroupItemCount = groupItemCount;
        CorrelationId = correlationId;
    }
}