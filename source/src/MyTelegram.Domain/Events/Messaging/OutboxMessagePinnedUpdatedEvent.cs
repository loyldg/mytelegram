namespace MyTelegram.Domain.Events.Messaging;

public class OutboxMessagePinnedUpdatedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>
{
    public OutboxMessagePinnedUpdatedEvent(
        RequestInfo requestInfo,
        long ownerPeerId,
        int messageId,
        //long channelId,
        bool pinned,
        bool pmOneSide,
        bool silent,
        int date,
        IReadOnlyList<InboxItem> inboxItems,
        long senderPeerId,
        int senderMessageId,
        Peer toPeer,
        int pts) : base(requestInfo)
    {
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
        //ChannelId = channelId;
        Pinned = pinned;
        PmOneSide = pmOneSide;
        Silent = silent;
        Date = date;
        InboxItems = inboxItems;
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
        ToPeer = toPeer;
        Pts = pts;

    }

    public int Date { get; }
    public IReadOnlyList<InboxItem> InboxItems { get; }

    public int MessageId { get; }

    public long OwnerPeerId { get; }

    //public long ChannelId { get; }
    public bool Pinned { get; }
    public bool PmOneSide { get; }
    public int Pts { get; }
    public int SenderMessageId { get; }
    public Peer ToPeer { get; }
    public long SenderPeerId { get; }
    public bool Silent { get; }

}