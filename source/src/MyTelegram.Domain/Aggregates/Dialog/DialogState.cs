namespace MyTelegram.Domain.Aggregates.Dialog;

public class DialogState : AggregateState<DialogAggregate, DialogId, DialogState>,
    IApply<DialogCreatedEvent>,
    IApply<InboxMessageReceivedEvent>,
    IApply<SetOutboxTopMessageSuccessEvent>,
    IApply<ReadInboxMessage2Event>,
    IApply<OutboxMessageHasReadEvent>,
    IApply<DraftSavedEvent>,
    //IApply<OutboxAlreadyReadEvent>,
    IApply<ReadChannelInboxMessageEvent>,
    IApply<ChannelHistoryClearedEvent>,
    IApply<HistoryClearedEvent>,
    IApply<ParticipantHistoryClearedEvent>,
    IApply<DialogPinChangedEvent>,
    IApply<PinnedOrderChangedEvent>,
    IApply<DialogUnreadMarkChangedEvent>,
    IApply<DraftClearedEvent>,
    //IApply<DeleteUserMessagesStartedEvent>,
    IApply<MentionCreatedEvent>,
    IApply<MentionReadEvent>,
    IApply<UpdateReadChannelOutboxEvent>,
    IApply<UpdateReadChannelInboxEvent>,
    IApply<ReadInboxMaxIdUpdatedEvent>,
    IApply<ReadOutboxMaxIdUpdatedEvent>,
    IApply<TopMessageIdUpdatedEvent>,
    IApply<DialogUpdatedEvent>,
    IApply<DialogFolderUpdatedEvent>
{
    public int ChannelHistoryMinId { get; private set; }
    public Draft? Draft { get; private set; }
    public int? FolderId { get; private set; }
    public long OwnerId { get; private set; }
    public bool Pinned { get; private set; }
    public int ReadInboxMaxId { get; private set; }
    public int ReadOutboxMaxId { get; private set; }
    public Peer ToPeer { get; private set; } = null!;
    public int TopMessageId { get; private set; }
    public int UnreadCount { get; private set; }
    public bool UnreadMark { get; private set; }
    public int UnreadMentionsCount { get; private set; }

    public void Apply(ChannelHistoryClearedEvent aggregateEvent)
    {
        ChannelHistoryMinId = aggregateEvent.HistoryMinId;
    }

    //public void Apply(DeleteUserMessagesStartedEvent aggregateEvent)
    //{
    //}

    public void Apply(DialogCreatedEvent aggregateEvent)
    {
        OwnerId = aggregateEvent.OwnerId;
        ToPeer = aggregateEvent.ToPeer;
        ChannelHistoryMinId = aggregateEvent.ChannelHistoryMinId;
        TopMessageId = aggregateEvent.TopMessageId;
    }

    public void Apply(DialogFolderUpdatedEvent aggregateEvent)
    {
        FolderId = aggregateEvent.FolderId;
    }

    public void Apply(DialogPinChangedEvent aggregateEvent)
    {
        Pinned = aggregateEvent.Pinned;
    }

    public void Apply(DialogUnreadMarkChangedEvent aggregateEvent)
    {
        UnreadMark = aggregateEvent.UnreadMark;
    }

    public void Apply(DialogUpdatedEvent aggregateEvent)
    {
        TopMessageId = aggregateEvent.TopMessageId;
        OwnerId = aggregateEvent.OwnerUserId;
        ToPeer = aggregateEvent.ToPeer;
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
        TopMessageId = aggregateEvent.MessageId;
        ToPeer = aggregateEvent.ToPeer;
    }

    public void Apply(MentionCreatedEvent aggregateEvent)
    {
        UnreadMentionsCount = aggregateEvent.UnreadMentionsCount;
    }

    public void Apply(MentionReadEvent aggregateEvent)
    {
        UnreadMentionsCount = aggregateEvent.UnreadMentionsCount;
    }

    //public void Apply(OutboxAlreadyReadEvent aggregateEvent)
    //{
    //}

    public void Apply(OutboxMessageHasReadEvent aggregateEvent)
    {
        ReadOutboxMaxId = aggregateEvent.MaxMessageId;
        ToPeer = aggregateEvent.ToPeer;
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
        if (TopMessageId < aggregateEvent.MaxId)
        {
            TopMessageId = aggregateEvent.MaxId;
        }
    }

    public void Apply(ReadInboxMaxIdUpdatedEvent aggregateEvent)
    {
        ReadInboxMaxId = aggregateEvent.ReadInboxMaxId;
    }

    public void Apply(ReadInboxMessage2Event aggregateEvent)
    {
        ToPeer = aggregateEvent.ToPeer;
        ReadInboxMaxId = aggregateEvent.MaxMessageId;
        UnreadCount = aggregateEvent.UnreadCount;
        if (TopMessageId < aggregateEvent.MaxMessageId)
        {
            TopMessageId = aggregateEvent.MaxMessageId;
        }
    }

    public void Apply(ReadOutboxMaxIdUpdatedEvent aggregateEvent)
    {
        ReadOutboxMaxId = aggregateEvent.ReadOutboxMaxId;
    }

    public void Apply(SetOutboxTopMessageSuccessEvent aggregateEvent)
    {
        OwnerId = aggregateEvent.OwnerPeerId;
        TopMessageId = aggregateEvent.MessageId;
        ToPeer = aggregateEvent.ToPeer;
        if (aggregateEvent.ClearDraft)
        {
            Draft = null;
        }
    }

    public void Apply(TopMessageIdUpdatedEvent aggregateEvent)
    {
        TopMessageId = aggregateEvent.NewTopMessageId;
    }

    public void Apply(UpdateReadChannelInboxEvent aggregateEvent)
    {
        ReadInboxMaxId = aggregateEvent.MaxId;
    }

    public void Apply(UpdateReadChannelOutboxEvent aggregateEvent)
    {
        ReadOutboxMaxId = aggregateEvent.MaxId;
    }

    public void LoadSnapshot(DialogSnapshot snapshot)
    {
        OwnerId = snapshot.OwnerId;
        TopMessageId = snapshot.TopMessage;
        ReadInboxMaxId = snapshot.ReadInboxMaxId;
        ReadOutboxMaxId = snapshot.ReadOutboxMaxId;
        UnreadCount = snapshot.UnreadCount;
        ToPeer = snapshot.ToPeer;
        UnreadMark = snapshot.UnreadMark;
        Pinned = snapshot.Pinned;
        ChannelHistoryMinId = snapshot.ChannelHistoryMinId;
        Draft = snapshot.Draft;
        UnreadMentionsCount = snapshot.UnreadMentionsCount;
        FolderId = snapshot.FolderId;
    }
}