namespace MyTelegram.Domain.Events.Messaging;

public class DeleteParticipantHistoryStartedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public long OwnerPeerId { get; }
    public List<int> MessageIds { get; }


    public DeleteParticipantHistoryStartedEvent(RequestInfo requestInfo, long ownerPeerId, List<int> messageIds) : base(requestInfo)
    {
        OwnerPeerId = ownerPeerId;
        MessageIds = messageIds;

    }
}