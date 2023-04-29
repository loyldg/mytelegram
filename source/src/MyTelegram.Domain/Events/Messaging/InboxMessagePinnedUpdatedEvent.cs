namespace MyTelegram.Domain.Events.Messaging;

public class InboxMessagePinnedUpdatedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasCorrelationId
{
    public InboxMessagePinnedUpdatedEvent(
        long ownerPeerId,
        int messageId,
        //long channelId,
        bool pinned,
        bool pmOneSide,
        bool silent,
        int date,
        Peer toPeer,
        int pts,
        Guid correlationId)
    {
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
        Pinned = pinned;
        PmOneSide = pmOneSide;
        Silent = silent;
        Date = date;
        ToPeer = toPeer;
        Pts = pts;
        CorrelationId = correlationId;
    }

    public int Date { get; }
    public Peer ToPeer { get; }

    public int MessageId { get; }

    public long OwnerPeerId { get; }
    public bool Pinned { get; }
    public bool PmOneSide { get; }
    public int Pts { get; }
    public bool Silent { get; }
    public Guid CorrelationId { get; }
}
