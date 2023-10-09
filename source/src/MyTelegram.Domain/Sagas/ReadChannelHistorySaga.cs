namespace MyTelegram.Domain.Sagas;

public class ReadChannelHistorySaga : MyInMemoryAggregateSaga<ReadChannelHistorySaga, ReadChannelHistorySagaId,
        ReadChannelHistorySagaLocator>,
    ISagaIsStartedBy<DialogAggregate, DialogId, ReadChannelInboxMessageEvent>,
    //ISagaHandles<MessageAggregate, MessageId, InboxMessageHasReadEvent>,
    ISagaHandles<DialogAggregate, DialogId, OutboxMessageHasReadEvent>,
    ISagaHandles<DialogAggregate, DialogId, OutboxAlreadyReadEvent>,
    //ISagaHandles<ChannelAggregate, ChannelId, ReadChannelLatestNoneBotOutboxMessageEvent>,
    IApply<ReadChannelHistoryCompletedEvent>
{
    private readonly ReadChannelHistorySagaState _state = new();

    public ReadChannelHistorySaga(ReadChannelHistorySagaId id
        , IEventStore eventStore) : base(id, eventStore)
    {
        Register(_state);
    }
    public void Apply(ReadChannelHistoryCompletedEvent aggregateEvent)
    {
        CompleteAsync();
    }

    //public Task HandleAsync(
    //    IDomainEvent<ChannelAggregate, ChannelId, ReadChannelLatestNoneBotOutboxMessageEvent> domainEvent,
    //    ISagaContext sagaContext,
    //    CancellationToken cancellationToken)
    //{
    //    if (domainEvent.AggregateEvent.LatestNoneBotSenderPeerId != _state.ReaderUid)
    //    {
    //        var senderDialog = DialogId.Create(domainEvent.AggregateEvent.LatestNoneBotSenderPeerId,
    //            PeerType.Channel,
    //            _state.ChannelId);
    //        var outboxMessageHasReadCommand = new OutboxMessageHasReadCommand(senderDialog,
    //            _state.RequestInfo,
    //            domainEvent.AggregateEvent.LatestNoneBotSenderMessageId,
    //            domainEvent.AggregateEvent.LatestNoneBotSenderPeerId,
    //            new Peer(PeerType.Channel,_state.ChannelId));
    //        Publish(outboxMessageHasReadCommand);
    //    }
    //    else
    //    {
    //        Emit(new ReadChannelHistoryCompletedEvent(_state.RequestInfo,
    //            _state.ChannelId,
    //            domainEvent.AggregateEvent.LatestNoneBotSenderPeerId,
    //            domainEvent.AggregateEvent.LatestNoneBotSenderMessageId,
    //            false,
    //            _state.TopMsgId
    //            ));
    //    }

    //    return Task.CompletedTask;
    //}

    public Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, OutboxAlreadyReadEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new ReadChannelHistoryCompletedEvent(_state.RequestInfo,
            _state.ChannelId,
            0,
            0,
            false, null));
        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, OutboxMessageHasReadEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new ReadChannelHistoryCompletedEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ToPeer.PeerId,
            domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.MaxMessageId,
            true, _state.TopMsgId));
        return Task.CompletedTask;
    }

    //public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, InboxMessageHasReadEvent> domainEvent,
    //    ISagaContext sagaContext,
    //    CancellationToken cancellationToken)
    //{
    //    var needNotifySender = domainEvent.AggregateEvent.SenderPeerId != domainEvent.AggregateEvent.ReaderUid;

    //    var shouldEmitCompletedEvent = true;
    //    if (domainEvent.AggregateEvent.SenderPeerId != domainEvent.AggregateEvent.ReaderUid //&&
    //                                                                                        //!domainEvent.AggregateEvent.SenderIsBot
    //        )
    //    {
    //        shouldEmitCompletedEvent = false;
    //        var senderDialogId = DialogId.Create(domainEvent.AggregateEvent.SenderPeerId,
    //            PeerType.Channel,
    //            domainEvent.AggregateEvent.ToPeer.PeerId);
    //        var command = new OutboxMessageHasReadCommand(senderDialogId,
    //            domainEvent.AggregateEvent.RequestInfo,
    //            domainEvent.AggregateEvent.MaxMessageId,
    //            domainEvent.AggregateEvent.SenderPeerId,
    //            //domainEvent.AggregateEvent.SourceCommandId,
    //            domainEvent.AggregateEvent.ToPeer);
    //        Publish(command);

    //        CreateReadingHistory(domainEvent.AggregateEvent.ToPeer.PeerId, domainEvent.AggregateEvent.SenderMessageId);
    //    }

    //    if (shouldEmitCompletedEvent)
    //    {
    //        Emit(new ReadChannelHistoryCompletedEvent(domainEvent.AggregateEvent.RequestInfo,
    //            domainEvent.AggregateEvent.ToPeer.PeerId,
    //            domainEvent.AggregateEvent.SenderPeerId,
    //            domainEvent.AggregateEvent.MaxMessageId,
    //            needNotifySender,
    //            _state.TopMsgId
    //            ));
    //    }

    //    return Task.CompletedTask;
    //}

    public Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, ReadChannelInboxMessageEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new ReadChannelHistoryStartedEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ReaderUserId,
            domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.TopMsgId));

        //var command = new ReadInboxHistoryCommand(
        //    MessageId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.MaxId),
        //    domainEvent.AggregateEvent.RequestInfo,
        //    domainEvent.AggregateEvent.ReaderUserId,
        //    DateTime.UtcNow.ToTimestamp()
        //);
        //Publish(command);
        CreateReadingHistory(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.MaxId);
        UpdateSenderDialog(domainEvent.AggregateEvent.SenderUserId, domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.MaxId);

        return Task.CompletedTask;
    }

    private void UpdateSenderDialog(long senderUserId, long channelId, int readOutboxMaxId)
    {
        var command = new OutboxMessageHasReadCommand(DialogId.Create(senderUserId, PeerType.Channel, channelId),
            _state.RequestInfo, readOutboxMaxId, senderUserId, new Peer(PeerType.Channel, channelId));
        Publish(command);
    }

    private void CreateReadingHistory(long toPeerId,
        int senderMsgId)
    {
        var command = new CreateReadingHistoryCommand(
            ReadingHistoryId.Create(_state.ReaderUserId, toPeerId, senderMsgId),
            _state.ReaderUserId,
            toPeerId,
            senderMsgId,
             DateTime.UtcNow.ToTimestamp()
            );
        Publish(command);
    }
}
