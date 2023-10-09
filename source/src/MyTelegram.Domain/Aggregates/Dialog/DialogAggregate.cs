namespace MyTelegram.Domain.Aggregates.Dialog;

public class DialogAggregate : MyInMemorySnapshotAggregateRoot<DialogAggregate, DialogId, DialogSnapshot>
{
    private readonly DialogState _state = new();

    public DialogAggregate(DialogId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void ClearChannelHistory(long reqMsgId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChannelHistoryClearedEvent(reqMsgId, _state.TopMessage));
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
        int nextMaxId,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new HistoryClearedEvent(requestInfo,
            _state.OwnerId,
            _state.TopMessage,
            revoke,
            _state.ToPeer,
            messageActionData,
            randomId,
            messageIdListToBeDelete,
            nextMaxId,
            correlationId));
    }

    public void ClearParticipantHistory(Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ParticipantHistoryClearedEvent(_state.OwnerId, _state.TopMessage, correlationId));
    }

    public void Create(
        RequestInfo requestInfo,
        long ownerId,
        Peer toPeer,
        int channelHistoryMinId,
        int topMessageId)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DialogCreatedEvent(ownerId,
            toPeer,
            channelHistoryMinId,
            topMessageId,
            DateTime.UtcNow
        ));
    }

    protected override Task<DialogSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new DialogSnapshot(
            _state.OwnerId,
            _state.TopMessage,
            _state.ReadInboxMaxId,
            _state.ReadOutboxMaxId,
            _state.UnreadCount,
            _state.ToPeer,
            _state.UnreadMark,
            _state.Pinned,
            _state.ChannelHistoryMinId,
            _state.Draft
        ));
    }

    protected override Task LoadSnapshotAsync(DialogSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);
        return Task.CompletedTask;
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
        //Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
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
        Peer toPeer
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
            toPeer));
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

    public void SaveDraft(long reqMsgId,
        string message,
        bool noWebpage,
        int? replyMsgId,
        int date,
        byte[]? entities
    )
    {
        //Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DraftSavedEvent(reqMsgId,
            _state.OwnerId,
            _state.ToPeer,
            new Draft(message,
                noWebpage,
                replyMsgId,
                date,
                entities)));
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

    public void StartDeleteUserMessages(RequestInfo requestInfo,
        bool revoke,
        List<int> messageIds,
        bool isClearHistory,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DeleteUserMessagesStartedEvent(requestInfo,
            revoke,
            _state.ToPeer.PeerId,
            messageIds,
            isClearHistory,
            correlationId));
    }

    public void TogglePinned(long reqMsgId,
        bool pinned)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DialogPinChangedEvent(reqMsgId, _state.OwnerId, pinned));
    }
}
