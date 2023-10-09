namespace MyTelegram.Domain.Events.Messaging;

public class InboxMessagePinnedUpdatedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>
{
    public InboxMessagePinnedUpdatedEvent(
        RequestInfo requestInfo,
        long ownerPeerId,
        int messageId,
        //long channelId,
        bool pinned,
        bool pmOneSide,
        bool silent,
        int date,
        Peer toPeer,
        int pts) : base(requestInfo)
    {
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
        Pinned = pinned;
        PmOneSide = pmOneSide;
        Silent = silent;
        Date = date;
        ToPeer = toPeer;
        Pts = pts;

    }

    public int Date { get; }
    public Peer ToPeer { get; }

    public int MessageId { get; }

    public long OwnerPeerId { get; }
    public bool Pinned { get; }
    public bool PmOneSide { get; }
    public int Pts { get; }
    public bool Silent { get; }

}