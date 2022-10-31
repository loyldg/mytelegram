namespace MyTelegram.Domain.Aggregates.Dialog;

public class DialogState : AggregateState<DialogAggregate, DialogId, DialogState>,
    IApply<DialogCreatedEvent>,
    IApply<InboxMessageReceivedEvent>,
    IApply<SetOutboxTopMessageSuccessEvent>,
    IApply<ReadInboxMessage2Event>,
    IApply<OutboxMessageHasReadEvent>,
    IApply<DraftSavedEvent>,
    IApply<OutboxAlreadyReadEvent>,
    IApply<ReadChannelInboxMessageEvent>,
    IApply<ChannelHistoryClearedEvent>,
    IApply<HistoryClearedEvent>,
    IApply<ParticipantHistoryClearedEvent>,
    IApply<DialogPinChangedEvent>,
    IApply<PinnedOrderChangedEvent>,
    IApply<DialogUnreadMarkChangedEvent>,
    IApply<DraftClearedEvent>,
    IApply<DeleteUserMessagesStartedEvent>
{
    public int ChannelHistoryMinId { get; private set; }

    public Draft? Draft { get; private set; }
    public long OwnerId { get; private set; }
    public bool Pinned { get; private set; }
    public int ReadInboxMaxId { get; private set; }
    public int ReadOutboxMaxId { get; private set; }
    public Peer ToPeer { get; private set; } = default!;
    public int TopMessage { get; private set; }
    public int UnreadCount { get; private set; }
    public bool UnreadMark { get; private set; }

    public void Apply(ChannelHistoryClearedEvent aggregateEvent)
    {
        ChannelHistoryMinId = aggregateEvent.HistoryMinId;
    }

    public void Apply(DialogCreatedEvent aggregateEvent)
    {
        OwnerId = aggregateEvent.OwnerId;
        ToPeer = aggregateEvent.ToPeer;
        ChannelHistoryMinId = aggregateEvent.ChannelHistoryMinId;
    }

    public void Apply(DialogPinChangedEvent aggregateEvent)
    {
        Pinned = aggregateEvent.Pinned;
    }

    public void Apply(DialogUnreadMarkChangedEvent aggregateEvent)
    {
        UnreadMark = aggregateEvent.UnreadMark;
    }

    public void Apply(DraftClearedEvent aggregateEvent)
    {
        Draft = null;
    }

    public void Apply(DraftSavedEvent aggregateEvent)
    {
        Draft = aggregateEvent.Draft;
    }

    public void Apply(HistoryClearedEvent aggregateEvent)
    {
        ChannelHistoryMinId = aggregateEvent.HistoryMinId;
    }

    public void Apply(InboxMessageReceivedEvent aggregateEvent)
    {
        OwnerId = aggregateEvent.OwnerPeerId;
        UnreadCount++;
        TopMessage = aggregateEvent.MessageId;
        ToPeer = aggregateEvent.ToPeer;
    }

    public void Apply(OutboxAlreadyReadEvent aggregateEvent)
    {
    }

    public void Apply(OutboxMessageHasReadEvent aggregateEvent)
    {
        ReadOutboxMaxId = aggregateEvent.MaxMessageId;
    }

    public void Apply(ParticipantHistoryClearedEvent aggregateEvent)
    {
        ChannelHistoryMinId = aggregateEvent.HistoryMinId;
    }

    public void Apply(PinnedOrderChangedEvent aggregateEvent)
    {
    }

    public void Apply(ReadChannelInboxMessageEvent aggregateEvent)
    {
        if (TopMessage < aggregateEvent.MaxId)
        {
            TopMessage = aggregateEvent.MaxId;
        }
    }

    public void Apply(ReadInboxMessage2Event aggregateEvent)
    {
        ToPeer = aggregateEvent.ToPeer;

        ReadInboxMaxId = aggregateEvent.MaxMessageId;
        var unreadCount = TopMessage - aggregateEvent.MaxMessageId;
        if (unreadCount < 0)
        {
            unreadCount = 0;
        }

        if (TopMessage < aggregateEvent.MaxMessageId)
        {
            TopMessage = aggregateEvent.MaxMessageId;
        }

        UnreadCount = unreadCount;
    }

    public void Apply(SetOutboxTopMessageSuccessEvent aggregateEvent)
    {
        OwnerId = aggregateEvent.OwnerPeerId;
        TopMessage = aggregateEvent.MessageId;
        ToPeer = aggregateEvent.ToPeer;
        if (aggregateEvent.ClearDraft)
        {
            Draft = null;
        }
    }

    public void LoadSnapshot(DialogSnapshot snapshot)
    {
        OwnerId = snapshot.OwnerId;
        TopMessage = snapshot.TopMessage;
        ReadInboxMaxId = snapshot.ReadInboxMaxId;
        ReadOutboxMaxId = snapshot.ReadOutboxMaxId;
        UnreadCount = snapshot.UnreadCount;
        ToPeer = snapshot.ToPeer;
        UnreadMark = snapshot.UnreadMark;
        Pinned = snapshot.Pinned;
        ChannelHistoryMinId = snapshot.ChannelHistoryMinId;
        Draft = snapshot.Draft;
    }
    public void Apply(DeleteUserMessagesStartedEvent aggregateEvent)
    {
    }
}
