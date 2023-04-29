namespace MyTelegram.Domain.Sagas;

public class DeleteMessageSaga2 :
    MyInMemoryAggregateSaga<DeleteMessageSaga2, DeleteMessageSaga2Id, DeleteMessageSaga2Locator>,
    ISagaIsStartedBy<ChatAggregate, ChatId, DeleteChatMessagesStartedEvent>,
    ISagaIsStartedBy<DialogAggregate, DialogId, DeleteUserMessagesStartedEvent>,
    ISagaHandles<MessageAggregate, MessageId, SelfMessageDeletedEvent>,
    ISagaHandles<MessageAggregate, MessageId, OutboxMessageDeletedEvent>,
    ISagaHandles<MessageAggregate, MessageId, InboxMessageDeletedEvent>,
    IApply<DeleteMessagesCompletedEvent2>
{
    private readonly IIdGenerator _idGenerator;
    private readonly DeleteMessageSaga2State _state = new();

    public DeleteMessageSaga2(DeleteMessageSaga2Id id,
        IEventStore eventStore,
        IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public void Apply(DeleteMessagesCompletedEvent2 aggregateEvent)
    {
        CompleteAsync();
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, InboxMessageDeletedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new DeleteOtherPartyMessageCompletedEvent(false,
            0,
            domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.MessageId));
        await IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId);
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, OutboxMessageDeletedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new DeleteOtherPartyMessageCompletedEvent(true,
            domainEvent.AggregateEvent.InboxItems?.Count ?? 0,
            domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.MessageId));
        await IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId);
        HandleDeleteOtherPartyMessage(true,
            domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.MessageId,
            domainEvent.AggregateEvent.InboxItems);
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, SelfMessageDeletedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var otherPartyMessageCount = 0;
        if (_state.Revoke)
        {
            otherPartyMessageCount = domainEvent.AggregateEvent.InboxItems?.Count ?? 0;
            //if (domainEvent.AggregateEvent.IsOut)
            //{
            //    otherPartyMessageCount = domainEvent.AggregateEvent.InboxItems?.Count ?? 0;
            //}
            //else
            //{
            //    if (_state.ChatMemberCount > 0)
            //    {
            //        otherPartyMessageCount = _state.ChatMemberCount;
            //    }
            //}
        }

        Emit(new DeleteSelfMessageCompletedEvent(
            domainEvent.AggregateEvent.IsOut,
            otherPartyMessageCount,
            domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.MessageId));
        await IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId);
        HandleDeleteOtherPartyMessage(domainEvent.AggregateEvent.IsOut,
            domainEvent.AggregateEvent.SenderPeerId,
            domainEvent.AggregateEvent.SenderMessageId,
            domainEvent.AggregateEvent.InboxItems);
    }

    public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, DeleteChatMessagesStartedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new DeleteMessageSaga2StartedEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.Revoke,
            domainEvent.AggregateEvent.MessageIds,
            domainEvent.AggregateEvent.ChatCreatorUserId,
            domainEvent.AggregateEvent.ChatMemberCount,
            domainEvent.AggregateEvent.IsClearHistory,
            PeerType.Chat,
            domainEvent.AggregateEvent.CorrelationId));
        DeleteMessagesForSelf(domainEvent.AggregateEvent.RequestInfo.UserId,
            domainEvent.AggregateEvent.MessageIds,
            domainEvent.AggregateEvent.CorrelationId);

        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, DeleteUserMessagesStartedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new DeleteMessageSaga2StartedEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.Revoke,
            domainEvent.AggregateEvent.MessageIds,
            0,
            0,
            domainEvent.AggregateEvent.IsClearHistory,
            PeerType.User,
            domainEvent.AggregateEvent.CorrelationId));
        DeleteMessagesForSelf(domainEvent.AggregateEvent.RequestInfo.UserId,
            domainEvent.AggregateEvent.MessageIds,
            domainEvent.AggregateEvent.CorrelationId);

        return Task.CompletedTask;
    }

    private void DeleteMessagesForSelf(long selfUserId,
        IReadOnlyList<int> messageIdList,
        Guid correlationId)
    {
        foreach (var messageId in messageIdList)
        {
            var command = new DeleteSelfMessageCommand(
                MessageId.Create(selfUserId, messageId),
                messageId,
                correlationId);
            Publish(command);
        }
    }

    private void HandleDeleteMessageCompleted()
    {
        if (_state.IsDeleteMessagesCompleted())
        {
            var selfDeletedItem = _state.GetDeletedBoxItem(_state.RequestInfo.UserId);
            var otherPartyDeletedBoxes = _state.GetDeletedBoxes();
            Emit(new DeleteMessagesCompletedEvent2(_state.RequestInfo,
                PeerType.Chat,
                selfDeletedItem,
                otherPartyDeletedBoxes,
                _state.IsClearHistory
            ));
        }
    }

    private void HandleDeleteOtherPartyMessage(bool isOut,
        long senderUserId,
        int senderMessageId,
        IReadOnlyList<InboxItem>? inboxItems)
    {
        if (!_state.Revoke)
        {
            return;
        }

        var shouldDeleteMessageForEveryone = _state.ToPeerType == PeerType.User || isOut ||
                                             _state.RequestInfo.UserId == _state.ChatCreatorUserId;

        if (!shouldDeleteMessageForEveryone)
        {
            return;
        }

        if (isOut)
        {
            if (inboxItems?.Count > 0)
            {
                foreach (var inboxItem in inboxItems)
                {
                    if (inboxItem.InboxOwnerPeerId == _state.RequestInfo.UserId)
                    {
                        continue;
                    }

                    var command =
                        new DeleteInboxMessageCommand(MessageId.Create(inboxItem.InboxOwnerPeerId,
                                inboxItem.InboxMessageId),
                            _state.CorrelationId);
                    Publish(command);
                }
            }
        }
        else
        {
            // delete outbox message
            var command =
                new DeleteOutboxMessageCommand(MessageId.Create(senderUserId, senderMessageId), _state.CorrelationId);
            Publish(command);
        }
    }

    private async Task IncrementPtsAsync(long peerId)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, peerId);
        Emit(new DeleteMessagePtsIncrementedEvent2(peerId, pts));
        HandleDeleteMessageCompleted();
    }
}
