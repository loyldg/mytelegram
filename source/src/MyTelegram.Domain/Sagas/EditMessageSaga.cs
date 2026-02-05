namespace MyTelegram.Domain.Sagas;

public class EditMessageSaga : MyInMemoryAggregateSaga<EditMessageSaga, EditMessageSagaId, EditMessageSagaLocator>,
ISagaIsStartedBy<MessageAggregate, MessageId, OutboxMessageEditedEventV2>,
        ISagaHandles<MessageAggregate, MessageId, InboxMessageEditedEventV2>
{
    private readonly IIdGenerator _idGenerator;
    private readonly EditMessageState _state = new();

    public EditMessageSaga(EditMessageSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, InboxMessageEditedEventV2> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new EditInboxMessageStartedSagaEvent(
            domainEvent.AggregateEvent.OldMessageItem,
            domainEvent.AggregateEvent.NewMessageItem));
        await HandleEditInboxCompletedAsync(domainEvent.AggregateEvent.OldMessageItem.OwnerPeer.PeerId);
        await HandleEditCompletedAsync();
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, OutboxMessageEditedEventV2> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new EditOutboxMessageStartedSagaEvent(
            domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.OldMessageItem,
            domainEvent.AggregateEvent.NewMessageItem
        ));

        await HandleEditOutboxCompletedAsync(domainEvent.AggregateEvent.OldMessageItem.OwnerPeer.PeerId);

        EditInbox(domainEvent.AggregateEvent);
        await HandleEditCompletedAsync();
    }

    private void EditInbox(OutboxMessageEditedEventV2 aggregateEvent)
    {
        var newItem = aggregateEvent.NewMessageItem;

        if (aggregateEvent.OldMessageItem.InboxItems?.Count > 0)
        {
            foreach (var inboxItem in aggregateEvent.OldMessageItem.InboxItems)
            {
                var command = new EditInboxMessageCommand(
                    MessageId.Create(inboxItem.InboxOwnerPeerId, inboxItem.InboxMessageId),
                    _state.RequestInfo,
                    inboxItem.InboxMessageId,
                    newItem.Message,
                    newItem.EditDate ?? DateTime.UtcNow.ToTimestamp(),
                    newItem.Entities,
                    newItem.Media,
                    newItem.ReplyMarkup,
                    newItem.InvertMedia,
                    newItem.Hashtags
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

    private async Task HandleEditInboxCompletedAsync(long inboxOwnerPeerId)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, inboxOwnerPeerId);
        var oldMessageItem = _state.OldInboxMessageItem;
        var newMessageItem = _state.NewInboxMessageItem with { Pts = pts };

        Emit(new InboxMessageEditCompletedSagaEvent(oldMessageItem, newMessageItem));
    }
    private async Task HandleEditOutboxCompletedAsync(long outboxOwnerPeerId)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, outboxOwnerPeerId);
        var oldMessageItem = _state.OldMessageItem;
        var newMessageItem = _state.NewMessageItem with { Pts = pts };
        Emit(new OutboxMessageEditCompletedSagaEvent(_state.RequestInfo, oldMessageItem, newMessageItem));
    }
}
