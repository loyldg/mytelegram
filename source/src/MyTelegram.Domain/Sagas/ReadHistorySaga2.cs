namespace MyTelegram.Domain.Sagas;

public class ReadHistorySaga : MyInMemoryAggregateSaga<ReadHistorySaga, ReadHistorySagaId,
        ReadHistorySagaLocator>,
    ISagaIsStartedBy<DialogAggregate, DialogId, ReadInboxMessage2Event>,
    ISagaHandles<MessageAggregate, MessageId, InboxMessageHasReadEvent>,
    ISagaHandles<DialogAggregate, DialogId, OutboxMessageHasReadEvent>,
    ISagaHandles<DialogAggregate, DialogId, OutboxAlreadyReadEvent>,
    ISagaHandles<ChatAggregate, ChatId, ReadLatestNoneBotOutboxMessageEvent>,
    IApply<ReadHistoryCompletedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly ReadHistoryState _state = new();

    public ReadHistorySaga(ReadHistorySagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public int SenderPts => _state.SenderPts;

    public long SenderPeerId => _state.SenderPeerId;

    public void Apply(ReadHistoryCompletedEvent aggregateEvent)
    {
        CompleteAsync();
    }


    public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ReadLatestNoneBotOutboxMessageEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new ReadHistoryReadLatestNoneBotOutboxMessageEvent(domainEvent.AggregateEvent.SenderPeerId));
        if (domainEvent.AggregateEvent.SenderPeerId != _state.ReaderUid)
        {
            SendReadOutboxMessageCommand(domainEvent.AggregateEvent.SenderPeerId,
                new Peer(PeerType.Chat, domainEvent.AggregateEvent.ChatId),
                domainEvent.AggregateEvent.SenderMessageId);
        }
        else
        {
            HandleReadHistoryCompleted();
        }

        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, OutboxAlreadyReadEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        CreateReadHistory(domainEvent.AggregateEvent.ToPeer.PeerId, domainEvent.AggregateEvent.NewMaxMessageId);

        if (!_state.NeedReadLatestNoneBotOutboxMessage)
        {
            HandleReadHistoryCompleted(true);
        }

        return Task.CompletedTask;
    }

    public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, OutboxMessageHasReadEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new ReadHistoryOutboxHasReadEvent(
            _state.RequestInfo,
            domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.MaxMessageId));
        await IncrementPtsAsync(
            domainEvent.AggregateEvent.OwnerPeerId,
            0,
            0,
            PtsChangeReason.OutboxMessageHasRead);

        CreateReadHistory(domainEvent.AggregateEvent.ToPeer.PeerId, _state.SenderMessageId);
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, InboxMessageHasReadEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var senderIsBot = false;
        var needReadLatestNoneBotOutboxMessage = domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Chat &&
                                                 senderIsBot;
        Emit(new ReadHistoryInboxHasReadEvent(domainEvent.AggregateEvent.IsOut,
            senderIsBot,
            needReadLatestNoneBotOutboxMessage));
        if (!domainEvent.AggregateEvent.IsOut /*&& !domainEvent.AggregateEvent.SenderIsBot*/)
        {
            var toPeerForSender =
                GetToPeerForSender(domainEvent.AggregateEvent.ToPeer, domainEvent.AggregateEvent.ReaderUid);

            SendReadOutboxMessageCommand(domainEvent.AggregateEvent.SenderPeerId,
                toPeerForSender,
                domainEvent.AggregateEvent.SenderMessageId);
        }

        if (needReadLatestNoneBotOutboxMessage)
        {
            var readLatestNoneBotOutboxMessageCommand =
                new ReadLatestNoneBotOutboxMessageCommand(
                    ChatId.Create(domainEvent.AggregateEvent.ToPeer.PeerId),
                    domainEvent.AggregateEvent.RequestInfo,
                    domainEvent.Metadata.SourceId.Value);
            Publish(readLatestNoneBotOutboxMessageCommand);
        }

        HandleReadHistoryCompleted();
        return Task.CompletedTask;
    }

    public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, ReadInboxMessage2Event> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new ReadHistoryStartedEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.MaxMessageId,
            domainEvent.AggregateEvent.ToPeer,
            domainEvent.Metadata.SourceId.Value));

        await IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.ReadCount,
            domainEvent.AggregateEvent.UnreadCount,
            PtsChangeReason.ReadInboxMessage);

        var command = new ReadInboxHistoryCommand(
            MessageId.Create(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.MaxMessageId),
            domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ReaderUserId,
            DateTime.UtcNow.ToTimestamp()
        );

        Publish(command);
    }

    private void CreateReadHistory(long toPeerId,
        int senderMsgId)
    {
        if (_state.ReaderToPeer.PeerType == PeerType.Channel || _state.ReaderToPeer.PeerType == PeerType.Chat)
        {
            var command = new CreateReadingHistoryCommand(ReadingHistoryId.Create(_state.ReaderUid,
                    toPeerId,
                    senderMsgId),
                _state.ReaderUid,
                toPeerId,
                senderMsgId,
                DateTime.UtcNow.ToTimestamp());

            Publish(command);
        }
    }

    private static Peer GetToPeerForSender(Peer readerToPeer,
        long readerPeerId)
    {
        if (readerToPeer.PeerType == PeerType.User)
        {
            return new Peer(PeerType.User, readerPeerId);
        }

        return readerToPeer;
    }

    private void HandleReadHistoryCompleted(bool outboxAlreadyRead = false)
    {
        if (_state.ReadHistoryCompleted || outboxAlreadyRead)
        {
            //Complete();
            Emit(new ReadHistoryCompletedEvent(_state.RequestInfo,
                _state.SenderIsBot,
                _state.ReaderUid,
                _state.ReaderMessageId,
                _state.ReaderPts,
                _state.ReaderToPeer,
                _state.SenderPeerId,
                _state.SenderPts,
                _state.SenderMessageId,
                _state.IsOut,
                outboxAlreadyRead,
                _state.SourceCommandId
            ));
        }
    }

    //private void IncrementPts(long peerId, PtsChangeReason reason, Guid correlationId)
    //{
    //    var incrementPtsCommand = new IncrementPtsCommand(PtsId.Create(peerId), reason, correlationId);
    //    Publish(incrementPtsCommand);
    //}

    private async Task IncrementPtsAsync(long peerId,
        int readCount,
        int unreadCount,
        PtsChangeReason reason)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, peerId);

        var requestInfo = _state.RequestInfo;
        if (reason == PtsChangeReason.OutboxMessageHasRead)
        {
            requestInfo = _state.RequestInfo with { PermAuthKeyId = 0 };
        }

        Emit(new ReadHistoryPtsIncrementEvent(
            requestInfo,
            peerId,
            pts,
            readCount,
            unreadCount,
            reason));
        HandleReadHistoryCompleted();
    }

    //protected override Task LoadSnapshotAsync(ReadHistorySagaSnapshot snapshot,
    //    ISnapshotMetadata metadata,
    //    CancellationToken cancellationToken)
    //{
    //    _state.LoadSnapshot(snapshot);
    //    return Task.CompletedTask;
    //}

    private void SendReadOutboxMessageCommand(
        long senderPeerId,
        Peer toPeer,
        int senderMessageId)
    {
        var senderDialogId = DialogId.Create(senderPeerId, toPeer);
        var outboxMessageHasReadCommand = new OutboxMessageHasReadCommand(senderDialogId,
            _state.RequestInfo,
            senderMessageId,
            senderPeerId,
            toPeer);
        Publish(outboxMessageHasReadCommand);
    }
}