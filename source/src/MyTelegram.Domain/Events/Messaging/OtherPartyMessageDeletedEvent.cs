namespace MyTelegram.Domain.Events.Messaging;

public class OtherPartyMessageDeletedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>
{
    public OtherPartyMessageDeletedEvent(
        RequestInfo requestInfo,
        long ownerPeerId,
        int messageId) : base(requestInfo)
    {
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;

    }

    public int MessageId { get; }

    public long OwnerPeerId { get; }

}