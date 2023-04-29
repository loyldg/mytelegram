namespace MyTelegram.Domain.Events.Messaging;

public class SendMessageStartedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>, IHasCorrelationId
{
    public SendMessageStartedEvent(RequestInfo requestInfo,
        MessageItem outMessageItem,
        bool clearDraft,
        int groupItemCount,
        bool forwardFromLinkedChannel,
        Guid correlationId) : base(requestInfo)
    {
        OutMessageItem = outMessageItem;
        ClearDraft = clearDraft;
        GroupItemCount = groupItemCount;
        ForwardFromLinkedChannel = forwardFromLinkedChannel;
        CorrelationId = correlationId;
    }

    public MessageItem OutMessageItem { get; }
    public bool ClearDraft { get; }
    public int GroupItemCount { get; }
    public bool ForwardFromLinkedChannel { get; }
    public Guid CorrelationId { get; }
}
