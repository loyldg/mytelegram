namespace MyTelegram.Domain.Aggregates.Dialog;

public class DialogAggregate : MyInMemorySnapshotAggregateRoot<DialogAggregate, DialogId, DialogSnapshot>
{
    private readonly DialogState _state = new();

    public DialogAggregate(DialogId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void StartDeleteUserMessages(RequestInfo requestInfo, bool revoke, List<int> messageIds, bool isClearHistory, Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DeleteUserMessagesStartedEvent(requestInfo, revoke, _state.ToPeer.PeerId, messageIds, isClearHistory, correlationId));
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

    public void ClearHistory(RequestInfo request,
        bool revoke,
        string messageActionData,
        long randomId,
        List<int> messageIdListToBeDelete,
        int nextMaxId,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new HistoryClearedEvent(request,
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

    public void Create(long ownerId,
        Peer toPeer,
        int channelHistoryMinId,
        int topMessageId,
        Guid correlationId)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DialogCreatedEvent(ownerId,
            toPeer,
            channelHistoryMinId,
            topMessageId,
            DateTime.UtcNow,
            correlationId));
    }

    public void MarkDialogAsUnread(bool unread)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DialogUnreadMarkChangedEvent(unread));
    }

    public void OutboxMessageHasRead(long reqMsgId,
        int maxMessageId,
        long ownerPeerId,
        Guid correlationId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (maxMessageId > _state.ReadOutboxMaxId)
        {
            Emit(new OutboxMessageHasReadEvent(reqMsgId,
                maxMessageId,
                ownerPeerId,
                _state.ToPeer,
                correlationId));
        }
        else
        {
            Emit(new OutboxAlreadyReadEvent(correlationId, _state.ReadOutboxMaxId, maxMessageId, _state.ToPeer));
        }
    }

    public void ReadChannelInboxMessage(long reqMsgId,
        long readerUid,
        long channelId,
        int maxId,
        Guid correlationId)
    {
        // Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);

        // When reading channel messages and does not join the channel,
        // the dialog has not been created,no verification required
        Emit(new ReadChannelInboxMessageEvent(reqMsgId,
            readerUid,
            channelId,
            maxId,
            correlationId));
    }

    public void ReadInboxMessage2(RequestInfo request,
        long readerUid,
        long ownerPeerId,
        int maxId,
        Peer toPeer,
        Guid correlationId
    )
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ReadInboxMessage2Event(request,
            readerUid,
            ownerPeerId,
            maxId,
            toPeer,
            correlationId));
    }

    public void ReceiveInboxMessage(
        int messageId,
        long ownerPeerId,
        Peer toPeer,
        Guid correlationId)
    {
        Emit(new InboxMessageReceivedEvent(
            messageId,
            ownerPeerId,
            toPeer,
            correlationId));
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
        int messageId,
        long ownerPeerId,
        //int pts, 
        Peer toPeer,
        bool clearDraft,
        Guid correlationId)
    {
        Emit(new SetOutboxTopMessageSuccessEvent(
            messageId,
            ownerPeerId,
            toPeer,
            clearDraft,
            correlationId));
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

    public void TogglePinned(long reqMsgId,
        bool pinned)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new DialogPinChangedEvent(reqMsgId, _state.OwnerId, pinned));
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
}
