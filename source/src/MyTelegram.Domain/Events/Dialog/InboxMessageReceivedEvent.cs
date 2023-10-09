namespace MyTelegram.Domain.Events.Dialog;

public class InboxMessageReceivedEvent : RequestAggregateEvent2<DialogAggregate, DialogId>
{
    public InboxMessageReceivedEvent(
        RequestInfo requestInfo,
        int messageId,
        long ownerPeerId,
        Peer toPeer
    ) : base(requestInfo)
    {
        MessageId = messageId;

        OwnerPeerId = ownerPeerId;
        ToPeer = toPeer;
    }

    public int MessageId { get; }
    public long OwnerPeerId { get; }
    public Peer ToPeer { get; }

}