namespace MyTelegram.Domain.Sagas;

public class EditMessageSaga : MyInMemoryAggregateSaga<EditMessageSaga, EditMessageSagaId, EditMessageSagaLocator>,
ISagaIsStartedBy<MessageAggregate, MessageId, OutboxMessageEditedEvent>,
        ISagaHandles<MessageAggregate, MessageId, InboxMessageEditedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly EditMessageState _state = new();

    public EditMessageSaga(EditMessageSagaId id, IEventStore eventStore,IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, InboxMessageEditedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new EditInboxMessageStartedEvent(domainEvent.AggregateEvent.InboxOwnerPeerId, domainEvent.AggregateEvent.MessageId));
        await HandleEditInboxCompletedAsync(domainEvent.AggregateEvent.InboxOwnerPeerId,
            domainEvent.AggregateEvent.MessageId, domainEvent.AggregateEvent.ToPeer).ConfigureAwait(false);
        await HandleEditCompletedAsync().ConfigureAwait(false);
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, OutboxMessageEditedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new EditOutboxMessageStartedEvent(
            domainEvent.AggregateEvent.Request,
            domainEvent.AggregateEvent.OldMessageItem,
            domainEvent.AggregateEvent.MessageId,
            domainEvent.AggregateEvent.NewMessage,
            domainEvent.AggregateEvent.EditDate,
            domainEvent.AggregateEvent.InboxItems?.Count ?? 0,
            domainEvent.AggregateEvent.Entities,
            domainEvent.AggregateEvent.Media
        ));

        await HandleEditOutboxCompletedAsync(domainEvent.AggregateEvent.OldMessageItem.OwnerPeer.PeerId).ConfigureAwait(false);

        EditInbox(domainEvent.AggregateEvent);
        await HandleEditCompletedAsync().ConfigureAwait(false);
    }

    private void EditInbox(OutboxMessageEditedEvent aggregateEvent)
    {
        if (aggregateEvent.InboxItems?.Count > 0)
        {
            foreach (var inboxItem in aggregateEvent.InboxItems)
            {
                var command = new EditInboxMessageCommand(
                    MessageId.Create(inboxItem.InboxOwnerPeerId, inboxItem.InboxMessageId),
                    inboxItem.InboxMessageId,
                    aggregateEvent.NewMessage,
                    aggregateEvent.EditDate,
                    aggregateEvent.Entities,
                    aggregateEvent.Media,
                    aggregateEvent.CorrelationId
                );
                Publish(command);
            }
        }
    }

    private Task HandleEditCompletedAsync()
    {
        if (_state.IsCompleted)
        {
            return CompleteAsync();
        }
        return Task.CompletedTask;
    }

    private async Task HandleEditInboxCompletedAsync(long inboxOwnerPeerId,
        int inboxMessageId,
        Peer toPeer)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, inboxOwnerPeerId).ConfigureAwait(false);

        Emit(new InboxMessageEditCompletedEvent(inboxOwnerPeerId,
            _state.OldMessageItem.SenderPeer.PeerId,
            inboxMessageId,
            _state.NewMessage,
            _state.EditDate,
            pts,
            toPeer,
            _state.Entities,
            _state.Media));
    }
    private async Task HandleEditOutboxCompletedAsync(long outboxOwnerPeerId)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, outboxOwnerPeerId).ConfigureAwait(false);
        var item = _state.OldMessageItem;
        Emit(new OutboxMessageEditCompletedEvent(_state.Request,
            outboxOwnerPeerId,
            item.SenderPeer.PeerId,
            _state.SenderMessageId,
            item.Post,
            item.Views,
            _state.NewMessage,
            pts,
            _state.EditDate,
            item.ToPeer,
            _state.Entities,
            _state.Media));
    }
}
