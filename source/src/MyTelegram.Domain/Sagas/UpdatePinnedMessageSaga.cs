namespace MyTelegram.Domain.Sagas;

public class UpdatePinnedMessageSaga : MyInMemoryAggregateSaga<UpdatePinnedMessageSaga,
        UpdatePinnedMessageSagaId,
        UpdatePinnedMessageSagaLocator>,
    ISagaIsStartedBy<MessageAggregate, MessageId, UpdatePinnedMessageStartedEvent>,
    ISagaHandles<MessageAggregate, MessageId, OutboxMessagePinnedUpdatedEvent>,
    ISagaHandles<MessageAggregate, MessageId, InboxMessagePinnedUpdatedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly UpdatePinnedMessageState _state = new();

    public UpdatePinnedMessageSaga(UpdatePinnedMessageSagaId id,
        IEventStore eventStore,
        IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, InboxMessagePinnedUpdatedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new UpdateInboxPinnedCompletedEvent(domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.MessageId,
            domainEvent.AggregateEvent.ToPeer));
        await IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId);

        await HandleUpdatePinnedCompletedAsync();
    }

    public async Task HandleAsync(
        IDomainEvent<MessageAggregate, MessageId, OutboxMessagePinnedUpdatedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new UpdateOutboxPinnedCompletedEvent(domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.MessageId,
            domainEvent.AggregateEvent.ToPeer));
        await IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId);
        var aggregateEvent = domainEvent.AggregateEvent;

        foreach (var inboxItem in domainEvent.AggregateEvent.InboxItems)
        {
            if (_state.StartUpdatePinnedOwnerPeerId == inboxItem.InboxOwnerPeerId)
            {
                continue;
            }

            var command = new UpdateInboxMessagePinnedCommand(
                MessageId.Create(inboxItem.InboxOwnerPeerId, /*toPeerId,*/ inboxItem.InboxMessageId),
                aggregateEvent.Pinned,
                aggregateEvent.PmOneSide,
                aggregateEvent.Silent,
                aggregateEvent.Date,
                aggregateEvent.CorrelationId);
            Publish(command);
        }

        await HandleUpdatePinnedCompletedAsync();
    }

    public async Task HandleAsync(
        IDomainEvent<MessageAggregate, MessageId, UpdatePinnedMessageStartedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var inboxCount = domainEvent.AggregateEvent.InboxItems.Count;
        if (domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel)
        {
            inboxCount = 1;
        }

        Emit(new UpdatePinnedMessageSagaStartedEvent(domainEvent.AggregateEvent.RequestInfo,
            !domainEvent.AggregateEvent.IsOut,
            domainEvent.AggregateEvent.Pinned,
            domainEvent.AggregateEvent.PmOneSide,
            domainEvent.AggregateEvent.Silent,
            inboxCount,
            domainEvent.AggregateEvent.MessageId,
            domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.MessageId,
            domainEvent.AggregateEvent.SenderPeerId,
            domainEvent.AggregateEvent.SenderMessageId,
            domainEvent.AggregateEvent.ToPeer,
            domainEvent.AggregateEvent.RandomId,
            domainEvent.AggregateEvent.Date,
            domainEvent.AggregateEvent.MessageActionData,
            domainEvent.AggregateEvent.CorrelationId
        ));

        await IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId);

        if (domainEvent.AggregateEvent.PmOneSide)
        {
            return;
        }

        if (domainEvent.AggregateEvent.IsOut)
        {
            UpdateInboxPinned(domainEvent.AggregateEvent);
        }
        else
        {
            var ownerPeerId = domainEvent.AggregateEvent.SenderPeerId;
            if (domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel)
            {
                ownerPeerId = domainEvent.AggregateEvent.ToPeer.PeerId;
            }

            var command = new UpdateOutboxMessagePinnedCommand(
                MessageId.Create(ownerPeerId,
                    domainEvent.AggregateEvent.SenderMessageId),
                domainEvent.AggregateEvent.Pinned,
                !domainEvent.AggregateEvent.PmOneSide,
                domainEvent.AggregateEvent.Silent,
                domainEvent.AggregateEvent.Date,
                domainEvent.AggregateEvent.CorrelationId);
            Publish(command);
        }
    }

    private Task HandleUpdatePinnedCompletedAsync()
    {
        if (_state.IsCompleted)
        {
            switch (_state.ToPeer.PeerType)
            {
                case PeerType.Channel:
                {
                    var setPinnedMsgIdCommand = new SetPinnedMsgIdCommand(ChannelId.Create(_state.ToPeer.PeerId),
                        _state.RequestInfo.ReqMsgId,
                        _state.PinnedMsgId,
                        _state.Pinned
                    );
                    Publish(setPinnedMsgIdCommand);
                }
                    break;
            }

            if (_state.Pinned)
            {
                var ownerPeerId = _state.StartUpdatePinnedOwnerPeerId;
                var outMessageId = 0;

                var aggregateId = MessageId.CreateWithRandomId(ownerPeerId, _state.RandomId);
                var command = new StartSendMessageCommand(aggregateId,
                    _state.RequestInfo,
                    new MessageItem(new Peer(PeerType.User, ownerPeerId),
                        _state.ToPeer,
                        new Peer(PeerType.User, ownerPeerId),
                        outMessageId,
                        string.Empty,
                        DateTime.UtcNow.ToTimestamp(),
                        _state.RandomId,
                        true,
                        SendMessageType.MessageService,
                        MessageType.Text,
                        MessageSubType.UpdatePinnedMessage,
                        messageActionData: _state.MessageActionData,
                        replyToMsgId: _state.ReplyToMsgId,
                        messageActionType: MessageActionType.PinMessage
                    ),
                    false,
                    1,
                    Guid.NewGuid());

                Publish(command);
            }

            return CompleteAsync();
        }

        return Task.CompletedTask;
    }

    private async Task IncrementPtsAsync(long peerId)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, peerId);
        Emit(new UpdatePinnedBoxPtsCompletedEvent(peerId, pts));
        var item = _state.GetUpdatePinItem(peerId);
        if (item == null)
        {
            throw new InvalidOperationException($"Can not find pinned item,peerId={peerId}");
        }

        var shouldReplyRpcResult =
            (_state.ToPeer.PeerType == PeerType.Channel || _state.RequestInfo.UserId == peerId) && !_state.Pinned;
        Emit(new UpdatePinnedMessageCompletedEvent(_state.RequestInfo.ReqMsgId,
            shouldReplyRpcResult,
            _state.RequestInfo.UserId,
            peerId,
            item.MessageId,
            _state.Pinned,
            _state.PmOneSide,
            pts,
            _state.ToPeer,
            _state.Date));

        if (_state.ToPeer.PeerType == PeerType.Channel)
        {
            await HandleUpdatePinnedCompletedAsync();
        }

        if (_state.PmOneSide)
        {
            await CompleteAsync();
        }
    }

    private void UpdateInboxPinned(UpdatePinnedMessageStartedEvent aggregateEvent)
    {
        foreach (var inboxItem in aggregateEvent.InboxItems)
        {
            if (_state.StartUpdatePinnedOwnerPeerId == inboxItem.InboxOwnerPeerId)
            {
                continue;
            }

            var command = new UpdateInboxMessagePinnedCommand(
                MessageId.Create(inboxItem.InboxOwnerPeerId, inboxItem.InboxMessageId),
                aggregateEvent.Pinned,
                aggregateEvent.PmOneSide,
                aggregateEvent.Silent,
                aggregateEvent.Date,
                aggregateEvent.CorrelationId);
            Publish(command);
        }
    }
}
