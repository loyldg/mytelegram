namespace MyTelegram.Domain.Sagas;

public class DeleteMessageSaga : MyInMemoryAggregateSaga<DeleteMessageSaga, DeleteMessageSagaId, DeleteMessageSagaLocator>,
    ISagaIsStartedBy<MessageAggregate, MessageId, DeleteMessagesStartedEvent>,
    //ISagaIsStartedBy<DialogAggregate, DialogId, HistoryClearedEvent>,
    ISagaHandles<MessageAggregate, MessageId, MessageDeletedEvent>,
    ISagaHandles<MessageAggregate, MessageId, OtherPartyMessageDeletedEvent>,
    //ISagaHandles<PtsAggregate, PtsId, PtsIncrementedEvent>,
    IApply<DeleteMessagesCompletedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly DeleteMessageState _state = new();

    public DeleteMessageSaga(DeleteMessageSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public void Apply(DeleteMessagesCompletedEvent aggregateEvent)
    {
        CompleteAsync();
    }

    // out box message deleted
    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, MessageDeletedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var inboxCount = _state.ToPeer.PeerType == PeerType.User ? 1 : domainEvent.AggregateEvent.InboxItems?.Count ?? 0;
        Emit(new DeleteSingleMessageCompletedEvent(domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.MessageId,
            true,
            inboxCount));
        await IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId)
            .ConfigureAwait(false);
        DeleteOtherPartyMessages(domainEvent.AggregateEvent);
    }

    public async Task HandleAsync(
        IDomainEvent<MessageAggregate, MessageId, OtherPartyMessageDeletedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new DeleteSingleMessageCompletedEvent(domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.MessageId,
            false,
            0));
        await IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId)
            .ConfigureAwait(false);
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, DeleteMessagesStartedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new DeleteMessagesSagaStartedEvent(domainEvent.AggregateEvent.Request,
            domainEvent.AggregateEvent.Revoke,
            domainEvent.AggregateEvent.IdList,
            domainEvent.AggregateEvent.ToPeer,
            false,
            0,
            0,
            null,
            domainEvent.AggregateEvent.ChatCreatorId));
        DeleteMessagesForSelf(domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.IdList,
            domainEvent.AggregateEvent.CorrelationId);
        return Task.CompletedTask;
    }
    private void DeleteMessagesForSelf(long selfUserId,
        IReadOnlyList<int> messageIdList,
        Guid correlationId)
    {
        foreach (var messageId in messageIdList)
        {
            var command = new DeleteMessageCommand(MessageId.Create(selfUserId, messageId),
                correlationId);
            Publish(command);
        }
    }

    private void DeleteOtherPartyMessages(MessageDeletedEvent aggregateEvent)
    {
        // All channel member shares the same message,so only need delete out box message
        if (_state.ToPeer.PeerType == PeerType.Channel)
        {
            return;
        }

        if (!_state.Revoke)
        {
            return;
        }

        // Only message sender can delete all messages when receive peer is not user peer
        if (aggregateEvent.IsOut)
        {
            if (aggregateEvent.InboxItems != null)
            {
                foreach (var inboxItem in aggregateEvent.InboxItems)
                {
                    if (inboxItem.InboxOwnerPeerId == _state.Request.UserId)
                    {
                        continue;
                    }

                    var command = new DeleteOtherPartyMessageCommand(
                        MessageId.Create(inboxItem.InboxOwnerPeerId, inboxItem.InboxMessageId),
                        aggregateEvent.CorrelationId);
                    Publish(command);
                }
            }
        }
        else if (_state.ToPeer.PeerType == PeerType.User)
        {
            var command = new DeleteOtherPartyMessageCommand(
                MessageId.Create(aggregateEvent.SenderPeerId, aggregateEvent.SenderMessageId),
                aggregateEvent.CorrelationId);
            Publish(command);
        }
    }

    private Task HandleDeleteMessageCompletedAsync()
    {
        if (_state.IsMessageDeletedCompleted())
        {
            var selfDeletedItem =
                _state.GetDeletedBoxItem(_state.ToPeer.PeerType == PeerType.Channel
                    ? _state.ToPeer.PeerId
                    : _state.Request.UserId);
            var otherPartyDeletedBoxes = _state.GetDeletedBoxes();
            Emit(new DeleteMessagesCompletedEvent(_state.Request,
                _state.ToPeer.PeerType,
                selfDeletedItem,
                otherPartyDeletedBoxes //,
                                       //_state.IsClearHistory,
                                       //_state.ClearHistoryNextMaxId
            ));

            //if (_state.IsClearHistory && _state.ClearHistoryNextMaxId == 0)
            //{
            //    var ownerPeerId = _state.SelfUserId;
            //    var outMessageId = await IdGeneratorFactory.Default.NextIdAsync(IdType.MessageId, ownerPeerId)
            //        .ConfigureAwait(false);
            //    var command = new CreateOutboxCommand(
            //        _state.ReqMsgId,
            //        _state.SelfAuthKeyId,
            //        MessageBoxId.Create(ownerPeerId, outMessageId),
            //        DialogId.Create(ownerPeerId, _state.ToPeerType, _state.ToPeerId),
            //        ownerPeerId,
            //        ownerPeerId,
            //        _state.ToPeerType,
            //        _state.ToPeerId,
            //        outMessageId,
            //        DateTime.UtcNow.ToTimestamp(),
            //        new MessageData(string.Empty),
            //        null,
            //        _state.RandomId,
            //        SendMessageType.MessageService,
            //        MessageBoxType.Text,
            //        0,
            //        false,
            //        false,
            //        _state.MessageActionData,
            //        Guid.NewGuid(),
            //        MessageActionType.HistoryClear,
            //        MessageBoxSubType.ClearHistory
            //    );
            //    Publish(command);
            //}

            //if (_state.IsClearHistory)
            //{
            //    Emit(new ClearHistoryCompletedEvent(_state.ReqMsgId, selfDeletedItem, otherPartyDeletedBoxes, _state.ClearHistoryNextMaxId));
            //}
            //else
            //{

            //    Emit(new DeleteMessagesCompletedEvent(_state.ReqMsgId,
            //        _state.SelfAuthKeyId,
            //        _state.SelfUserId,
            //        _state.ToPeerType,
            //        selfDeletedItem,
            //        otherPartyDeletedBoxes,
            //        _state.IsClearHistory,
            //        _state.ClearHistoryNextMaxId
            //    ));
            //}
        }

        return Task.CompletedTask;
    }
    private async Task IncrementPtsAsync(long peerId)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, peerId).ConfigureAwait(false);
        Emit(new DeleteMessagePtsIncrementedEvent(peerId, pts));

        await HandleDeleteMessageCompletedAsync().ConfigureAwait(false);
    }
}
