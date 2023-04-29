namespace MyTelegram.Domain.Sagas;

public class DeleteParticipantHistorySaga : MyInMemoryAggregateSaga<DeleteParticipantHistorySaga,
        DeleteParticipantHistorySagaId, DeleteParticipantHistorySagaLocator>,
    ISagaIsStartedBy<ChannelAggregate, ChannelId, DeleteParticipantHistoryStartedEvent>,
    ISagaHandles<MessageAggregate, MessageId, MessageDeletedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly DeleteParticipantHistorySagaState _state = new();

    public DeleteParticipantHistorySaga(DeleteParticipantHistorySagaId id,
        IEventStore eventStore,
        IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, MessageDeletedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        return IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId);
    }

    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, DeleteParticipantHistoryStartedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new DeleteParticipantHistorySagaStartedEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.OwnerPeerId,
            domainEvent.AggregateEvent.MessageIds
        ));
        foreach (var messageId in domainEvent.AggregateEvent.MessageIds)
        {
            var command = new DeleteMessageCommand(MessageId.Create(domainEvent.AggregateEvent.OwnerPeerId, messageId),
                domainEvent.AggregateEvent.CorrelationId);
            Publish(command);
        }

        return Task.CompletedTask;
    }

    private Task HandleDeleteMessageCompletedAsync()
    {
        if (_state.TotalCount != 0 && _state.TotalCount == _state.DeletedCount)
        {
            var nextMaxId = _state.MessageIds.Min();
            Emit(new DeleteParticipantHistoryCompletedEvent(_state.RequestInfo,
                _state.OwnerPeerId,
                _state.MessageIds,
                _state.Pts,
                _state.MessageIds.Count,
                nextMaxId));
            return CompleteAsync();
        }

        return Task.CompletedTask;
    }

    private async Task IncrementPtsAsync(long peerId)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, peerId);
        Emit(new DeleteParticipantHistoryPtsIncrementedEvent(peerId, pts));
        await HandleDeleteMessageCompletedAsync();
    }
}
