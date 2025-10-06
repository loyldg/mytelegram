namespace MyTelegram.Domain.Aggregates.Dialog;

public class DialogAggregate : MyInMemorySnapshotAggregateRoot<DialogAggregate, DialogId, DialogSnapshot>
{
    private readonly DialogState _state = new();

    public DialogAggregate(DialogId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void UpdateDialogFolder(RequestInfo requestInfo, int? folder)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DialogFolderUpdatedEvent(requestInfo, _state.OwnerId, _state.ToPeer, folder));
    }

    public void UpdateDialog(RequestInfo requestInfo, long ownerUserId, Peer toPeer, int topMessageId, int pts, int? defaultHistoryTtl)
    {
        Emit(new DialogUpdatedEvent(requestInfo, ownerUserId, toPeer, topMessageId, pts, IsNew, defaultHistoryTtl));
    }

    public void ClearChannelHistory(RequestInfo requestInfo, int availableMinId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChannelHistoryClearedEvent(requestInfo, _state.ToPeer.PeerId, availableMinId));
    }

    public void ClearDraft()
    {
        if (!IsNew)
        {
            Emit(new DraftClearedEvent());
        }
    }

    public void ClearHistory(RequestInfo requestInfo,
        bool revoke,
        string messageActionData,
        long randomId,
        List<int> messageIdListToBeDelete,
        int nextMaxId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new HistoryClearedEvent(requestInfo,
            _state.OwnerId,
            _state.TopMessageId,
            revoke,
            _state.ToPeer,
            messageActionData,
            randomId,
            messageIdListToBeDelete,
            nextMaxId
        ));
    }

    public void ClearParticipantHistory(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ParticipantHistoryClearedEvent(requestInfo, _state.OwnerId, _state.TopMessageId));
    }

    public void Create(
        RequestInfo requestInfo,
        long ownerId,
        Peer toPeer,
        int channelHistoryMinId,
        int topMessageId)
    {
        //Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DialogCreatedEvent(ownerId,
            toPeer,
            channelHistoryMinId,
            topMessageId,
            DateTime.UtcNow
        ));
    }

    public void CreateMention(int messageId)
    {
        Emit(new MentionCreatedEvent(_state.OwnerId, _state.ToPeer, messageId, _state.UnreadMentionsCount + 1));
    }

    public void MarkDialogAsUnread(bool unread)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DialogUnreadMarkChangedEvent(unread));
    }

    public void OutboxMessageHasRead(RequestInfo requestInfo,
        int maxMessageId,
        long ownerPeerId,
        Peer toPeer
    )
    {
        if (maxMessageId > _state.ReadOutboxMaxId)
        {
            Emit(new OutboxMessageHasReadEvent(requestInfo,
                maxMessageId,
                ownerPeerId,
                toPeer));
        }
        else
        {
            Emit(new OutboxAlreadyReadEvent(requestInfo, _state.ReadOutboxMaxId, maxMessageId, _state.ToPeer));
        }
    }

    public void ReadChannelInboxMessage(RequestInfo requestInfo,
        long readerUserId,
        long channelId,
        int maxId,
        long senderUserId,
        int? topMsgId)
    {
        // Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

        // When user reading channel messages and does not join the channel,
        // the dialog has not been created,no verification required
        Emit(new ReadChannelInboxMessageEvent(requestInfo,
            readerUserId,
            channelId,
            maxId,
            senderUserId,
            topMsgId));
    }

    public void ReadInboxMessage2(RequestInfo requestInfo,
        long readerUserId,
        long ownerPeerId,
        int maxId,
        int unreadCount,
        Peer toPeer,
        int date
    )
    {
        //Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        //var unreadCount = _state.TopMessage - maxId;
        //if (unreadCount < 0)
        //{
        //    unreadCount = 0;
        //}

        var readCount = _state.UnreadCount - unreadCount;
        if (readCount < 0)
        {
            readCount = 0;
        }

        Emit(new ReadInboxMessage2Event(requestInfo,
            readerUserId,
            ownerPeerId,
            maxId,
            readCount,
            unreadCount,
            toPeer, date));
    }

    public void ReadMention(int messageId, bool readAllMentions = false)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var unreadMentionsCount = readAllMentions ? 0 : _state.UnreadMentionsCount - 1;
        if (unreadMentionsCount < 0)
        {
            unreadMentionsCount = 0;
        }

        Emit(new MentionReadEvent(_state.OwnerId, _state.ToPeer, messageId, unreadMentionsCount));
    }

    public void ReceiveInboxMessage(
        RequestInfo requestInfo,
        int messageId,
        long ownerPeerId,
        Peer toPeer)
    {
        Emit(new InboxMessageReceivedEvent(
            requestInfo,
            messageId,
            ownerPeerId,
            toPeer
        ));
    }

    public void SaveDraft(RequestInfo requestInfo,
        Draft draft
    )
    {
        //Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DraftSavedEvent(requestInfo,
            _state.OwnerId,
            _state.ToPeer,
           draft));
    }

    public void SetOutboxTopMessage(
        //RequestInfo requestInfo,
        int messageId,
        long ownerPeerId,
        //int pts, 
        Peer toPeer,
        bool clearDraft)
    {
        Emit(new SetOutboxTopMessageSuccessEvent(
            //requestInfo,
            messageId,
            ownerPeerId,
            toPeer,
            clearDraft));
    }

    public void SetPinnedMsgId(int pinnedMsgId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DialogMsgIdPinnedEvent(pinnedMsgId));
    }

    public void SetPinnedOrder(int order)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new PinnedOrderChangedEvent(order));
    }

    public void TogglePinned(RequestInfo requestInfo,
        bool pinned)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DialogPinChangedEvent(requestInfo, _state.OwnerId, pinned));
    }

    public void UpdateReadChannelInbox(RequestInfo requestInfo, long messageSenderUserId, int maxId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UpdateReadChannelInboxEvent(requestInfo, messageSenderUserId, _state.ToPeer.PeerId, maxId));
    }

    public void UpdateReadChannelOutbox(RequestInfo requestInfo, int maxId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new UpdateReadChannelOutboxEvent(requestInfo, _state.OwnerId, _state.ToPeer.PeerId, maxId));
    }

    public void UpdateReadInboxMaxId(RequestInfo requestInfo, int maxId, long senderUserId, int senderMessageId, int unreadCount)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ReadInboxMaxIdUpdatedEvent(requestInfo, _state.OwnerId, _state.ToPeer.PeerId, maxId, senderUserId,
            senderMessageId,
            unreadCount
            ));
    }

    public void UpdateReadOutboxMaxId(RequestInfo requestInfo, int maxId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ReadOutboxMaxIdUpdatedEvent(requestInfo, _state.OwnerId, _state.ToPeer.PeerId, maxId));
    }

    public void UpdateTopMessageId(int newTopMessageId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new TopMessageIdUpdatedEvent(_state.OwnerId, _state.ToPeer, newTopMessageId));
    }
    protected override Task<DialogSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new DialogSnapshot(
            _state.OwnerId,
            _state.TopMessageId,
            _state.ReadInboxMaxId,
            _state.ReadOutboxMaxId,
            _state.UnreadCount,
            _state.ToPeer,
            _state.UnreadMark,
            _state.Pinned,
            _state.ChannelHistoryMinId,
            _state.Draft,
            _state.UnreadMentionsCount,
            _state.FolderId
        ));
    }

    protected override Task LoadSnapshotAsync(DialogSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);
        return Task.CompletedTask;
    }
}