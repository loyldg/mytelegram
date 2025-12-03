namespace MyTelegram.Domain.Aggregates.Dialog;

public class DialogAggregate : MyInMemorySnapshotAggregateRoot<DialogAggregate, DialogId, DialogSnapshot>
{
    private readonly DialogState _state = new();

    public DialogAggregate(DialogId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void UpdateDialogFolder(RequestInfo requestInfo, int? folderId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var ownerUserId = _state.OwnerId;
        Emit(new DialogFolderUpdatedEvent(requestInfo, ownerUserId, _state.ToPeer, folderId));
    }

    public void UpdateDialog(RequestInfo requestInfo, long ownerUserId, Peer toPeer, int topMessageId, int pts, int? defaultHistoryTtl, bool isMonoForum)
    {
        Emit(new DialogUpdatedEvent(requestInfo, ownerUserId, toPeer, topMessageId, pts, IsNew, defaultHistoryTtl, isMonoForum));
    }
	
    public void ClearChannelHistory(RequestInfo requestInfo, int availableMinId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var channelId = _state.ToPeer.PeerId;
        var historyMinId = availableMinId;
        Emit(new ChannelHistoryClearedEvent(requestInfo, channelId, historyMinId));
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
        int historyMinId)
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
            historyMinId
        ));
    }

    public void ClearParticipantHistory(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var historyMinId = _state.TopMessageId;
        Emit(new ParticipantHistoryClearedEvent(requestInfo, _state.OwnerId, historyMinId));
    }

    public void CreateDialog(
        RequestInfo requestInfo,
        long ownerId,
        Peer toPeer,
        int channelHistoryMinId,
        int topMessageId)
    {
        //Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        var creationTime = DateTime.UtcNow;
        Emit(new DialogCreatedEvent(ownerId,
            toPeer,
            channelHistoryMinId,
            topMessageId,
            creationTime
        ));
    }

    public void CreateMention(int messageId)
    {
        var unreadMentionsCount = _state.UnreadMentionsCount + 1;
        var ownerUserId = _state.OwnerId;
        Emit(new MentionCreatedEvent(ownerUserId, _state.ToPeer, messageId, unreadMentionsCount));
    }

    public void MarkDialogAsUnread(bool unread)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var unreadMark = unread;
        Emit(new DialogUnreadMarkChangedEvent(unreadMark));
    }

    public void OutboxMessageHasRead(RequestInfo requestInfo,
        int maxMessageId,
        long ownerPeerId,
        Peer toPeer
    )
    {
        //Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var hasRead = maxMessageId > _state.ReadOutboxMaxId;
        if (_state.ReadOutboxMaxId > maxMessageId)
        {
            maxMessageId = _state.ReadOutboxMaxId;
        }
        Emit(new OutboxMessageHasReadEvent(requestInfo,
            maxMessageId,
            ownerPeerId,
            toPeer,
            hasRead
            ));
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
        int maxMessageId,
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
            maxMessageId,
            readCount,
            unreadCount,
            toPeer, date));
    }

    public void ReadMention(int messageId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var unreadMentionsCount = _state.UnreadMentionsCount - 1;
        if (unreadMentionsCount < 0)
        {
            unreadMentionsCount = 0;
        }
        var ownerUserId = _state.OwnerId;
        Emit(new MentionReadEvent(ownerUserId, _state.ToPeer, messageId, unreadMentionsCount));
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
        Draft draft,
        int? topicId
    )
    {
        //Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var ownerPeerId = _state.OwnerId;
        var peer = _state.ToPeer;
        Emit(new DraftSavedEvent(requestInfo,
            ownerPeerId,
            peer,
           draft, topicId));
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

    public void ToggleDialogPinned(RequestInfo requestInfo,
        bool pinned)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var ownerPeerId = _state.OwnerId;
        Emit(new DialogPinChangedEvent(requestInfo, ownerPeerId, pinned));
    }

    public void UpdateReadChannelInbox(RequestInfo requestInfo, long messageSenderUserId, int maxId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var channelId = _state.ToPeer.PeerId;
        Emit(new UpdateReadChannelInboxEvent(requestInfo, messageSenderUserId, channelId, maxId));
    }

    public void UpdateReadChannelOutbox(RequestInfo requestInfo, int maxId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var messageSenderUserId = _state.OwnerId;
        var channelId = _state.ToPeer.PeerId;
        Emit(new UpdateReadChannelOutboxEvent(requestInfo, messageSenderUserId, channelId, maxId));
    }

    public void UpdateReadInboxMaxId(RequestInfo requestInfo, int maxId, long senderUserId, int senderMessageId, int unreadCount)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var readInboxMaxId = maxId;
        Emit(new ReadInboxMaxIdUpdatedEvent(requestInfo, _state.OwnerId, _state.ToPeer.PeerId, readInboxMaxId, senderUserId,
            senderMessageId,
            unreadCount
            ));
    }

    public void UpdateReadOutboxMaxId(RequestInfo requestInfo, int readOutboxMaxId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var ownerUserId = _state.OwnerId;
        Emit(new ReadOutboxMaxIdUpdatedEvent(requestInfo, ownerUserId, _state.ToPeer.PeerId, readOutboxMaxId));
    }

    public void UpdateDialogTopMessageId(int newTopMessageId)
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