namespace MyTelegram.Domain.Events.Messaging;

public class DeleteParticipantHistoryStartedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>, IHasCorrelationId
{
    public long OwnerPeerId { get; }
    public List<int> MessageIds { get; }
    public Guid CorrelationId { get; }

    public DeleteParticipantHistoryStartedEvent(RequestInfo requestInfo, long ownerPeerId, List<int> messageIds, Guid correlationId) : base(requestInfo)
    {
        OwnerPeerId = ownerPeerId;
        MessageIds = messageIds;
        CorrelationId = correlationId;
    }
}