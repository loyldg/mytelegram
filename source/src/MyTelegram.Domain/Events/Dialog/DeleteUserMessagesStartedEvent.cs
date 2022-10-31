namespace MyTelegram.Domain.Events.Dialog;

public class DeleteUserMessagesStartedEvent : RequestAggregateEvent2<DialogAggregate, DialogId>, IHasCorrelationId
{
    public bool Revoke { get; }
    public long ToUserId { get; }
    public List<int> MessageIds { get; }
    public bool IsClearHistory { get; }
    public Guid CorrelationId { get; }

    public DeleteUserMessagesStartedEvent(RequestInfo requestInfo, bool revoke, long toUserId,
        List<int> messageIds, bool isClearHistory, Guid correlationId) : base(requestInfo)
    {
        Revoke = revoke;
        ToUserId = toUserId;
        MessageIds = messageIds;
        IsClearHistory = isClearHistory;
        CorrelationId = correlationId;
    }
}
