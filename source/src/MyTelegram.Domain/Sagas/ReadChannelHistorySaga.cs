namespace MyTelegram.Domain.Sagas;

public class ReadChannelHistorySaga : MyInMemoryAggregateSaga<ReadChannelHistorySaga, ReadChannelHistorySagaId,
        ReadChannelHistorySagaLocator>,
    ISagaIsStartedBy<DialogAggregate, DialogId, ReadChannelInboxMessageEvent>,
    ISagaHandles<MessageAggregate, MessageId, InboxMessageHasReadEvent>,
    ISagaHandles<DialogAggregate, DialogId, OutboxMessageHasReadEvent>,
    ISagaHandles<DialogAggregate, DialogId, OutboxAlreadyReadEvent>,
    ISagaHandles<ChannelAggregate, ChannelId, ReadChannelLatestNoneBotOutboxMessageEvent>,
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

    public Task HandleAsync(
        IDomainEvent<ChannelAggregate, ChannelId, ReadChannelLatestNoneBotOutboxMessageEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.LatestNoneBotSenderPeerId != _state.ReaderUid)
        {
            var senderDialog = DialogId.Create(domainEvent.AggregateEvent.LatestNoneBotSenderPeerId,
                PeerType.Channel,
                _state.ChannelId);
            var outboxMessageHasReadCommand = new OutboxMessageHasReadCommand(senderDialog,
                _state.ReqMsgId,
                domainEvent.AggregateEvent.LatestNoneBotSenderMessageId,
                domainEvent.AggregateEvent.LatestNoneBotSenderPeerId,
                domainEvent.AggregateEvent.SourceCommandId,
                domainEvent.AggregateEvent.CorrelationId);
            Publish(outboxMessageHasReadCommand);
        }
        else
        {
            Emit(new ReadChannelHistoryCompletedEvent(_state.ReqMsgId,
                _state.ChannelId,
                domainEvent.AggregateEvent.LatestNoneBotSenderPeerId,
                domainEvent.AggregateEvent.LatestNoneBotSenderMessageId,
                false));
        }

        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, OutboxAlreadyReadEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new ReadChannelHistoryCompletedEvent(_state.ReqMsgId,
            _state.ChannelId,
            0,
            0,
            false));
        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, OutboxMessageHasReadEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new ReadChannelHistoryCompletedEvent(domainEvent.AggregateEvent.ReqMsgId,
            domainEvent.AggregateEvent.ToPeer.PeerId,
            domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.MaxMessageId,
            true));
        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, InboxMessageHasReadEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var needNotifySender = domainEvent.AggregateEvent.SenderPeerId != domainEvent.AggregateEvent.ReaderUid;

        var shouldEmitCompletedEvent = true;
        if (domainEvent.AggregateEvent.SenderPeerId != domainEvent.AggregateEvent.ReaderUid //&&
                                                                                            //!domainEvent.AggregateEvent.SenderIsBot
            )
        {
            shouldEmitCompletedEvent = false;
            var senderDialogId = DialogId.Create(domainEvent.AggregateEvent.SenderPeerId,
                PeerType.Channel,
                domainEvent.AggregateEvent.ToPeer.PeerId);
            var command = new OutboxMessageHasReadCommand(senderDialogId,
                domainEvent.AggregateEvent.ReqMsgId,
                domainEvent.AggregateEvent.MaxMessageId,
                domainEvent.AggregateEvent.SenderPeerId,
                //domainEvent.AggregateEvent.SourceCommandId,
                domainEvent.Metadata.SourceId.Value,
                domainEvent.AggregateEvent.CorrelationId);
            Publish(command);

            CreateReadHistory(domainEvent.AggregateEvent.ToPeer.PeerId, domainEvent.AggregateEvent.SenderMessageId, DateTime.UtcNow.ToTimestamp());
        }

        if (shouldEmitCompletedEvent)
        {
            Emit(new ReadChannelHistoryCompletedEvent(domainEvent.AggregateEvent.ReqMsgId,
                domainEvent.AggregateEvent.ToPeer.PeerId,
                domainEvent.AggregateEvent.SenderPeerId,
                domainEvent.AggregateEvent.MaxMessageId,
                needNotifySender));
        }

        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, ReadChannelInboxMessageEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new ReadChannelHistoryStartedEvent(domainEvent.AggregateEvent.ReqMsgId,
            domainEvent.AggregateEvent.ReaderUid,
            domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.CorrelationId));

        var command = new ReadInboxHistoryCommand(
            MessageId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.MaxId),
            domainEvent.AggregateEvent.ReqMsgId,
            domainEvent.AggregateEvent.ReaderUid,
            domainEvent.AggregateEvent.CorrelationId
        );
        Publish(command);

        return Task.CompletedTask;
    }

    private void CreateReadHistory(long toPeerId,
        int senderMsgId, int date)
    {
        var command = new CreateReadingHistoryCommand(
            ReadingHistoryId.Create(_state.ReaderUid, toPeerId, senderMsgId),
            _state.ReaderUid,
            toPeerId,
            senderMsgId,
            date
            );
        Publish(command);
    }
}
