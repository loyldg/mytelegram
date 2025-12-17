namespace MyTelegram.Domain.Sagas;

public class ReadHistoryPtsIncrementedSagaEvent
    (RequestInfo requestInfo, long userId, int pts) : AggregateEvent<ReadHistorySaga, ReadHistorySagaId>,
        IHasRequestInfo
{
    public RequestInfo RequestInfo { get; } = requestInfo;
    public long UserId { get; } = userId;
    public int Pts { get; } = pts;
}

public class ReadHistoryStartedSagaEvent(RequestInfo requestInfo) : AggregateEvent<ReadHistorySaga, ReadHistorySagaId>
{
    public RequestInfo RequestInfo { get; } = requestInfo;
}

public class UpdateInboxMaxIdCompletedSagaEvent(RequestInfo requestInfo, int maxId, int pts, long peerId)
    : RequestAggregateEvent2<ReadHistorySaga, ReadHistorySagaId>(requestInfo)
{
    public int MaxId { get; } = maxId;
    public int Pts { get; } = pts;
    public long PeerId { get; } = peerId;
}

public class UpdateOutboxMaxIdCompletedSagaEvent(long userId, long toPeerId, int maxId, int pts, int ptsCount)
    : AggregateEvent<ReadHistorySaga, ReadHistorySagaId>
{
    public long UserId { get; } = userId;
    public long ToPeerId { get; } = toPeerId;
    public int MaxId { get; } = maxId;
    public int Pts { get; } = pts;
    public int PtsCount { get; } = ptsCount;
}


public class ReadHistorySaga : MyInMemoryAggregateSaga<ReadHistorySaga, ReadHistorySagaId,
        ReadHistorySagaLocator>,
        ISagaIsStartedBy<DialogAggregate, DialogId, ReadInboxMaxIdUpdatedEvent>,
        ISagaHandles<DialogAggregate, DialogId, ReadOutboxMaxIdUpdatedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly ReadHistoryState _state = new();

    public ReadHistorySaga(ReadHistorySagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, ReadInboxMaxIdUpdatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        Emit(new ReadHistoryStartedSagaEvent(domainEvent.AggregateEvent.RequestInfo));
        var pts = await IncrementPtsAsync(domainEvent.AggregateEvent.RequestInfo.UserId);
        Emit(new UpdateInboxMaxIdCompletedSagaEvent(_state.RequestInfo, domainEvent.AggregateEvent.ReadInboxMaxId, pts,
            domainEvent.AggregateEvent.SenderUserId));
        CreateReadingHistory(domainEvent.AggregateEvent.SenderUserId, domainEvent.AggregateEvent.SenderMessageId);
        UpdateReadOutboxMaxId(domainEvent.AggregateEvent.SenderUserId, domainEvent.AggregateEvent.SenderMessageId);
    }

    public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, ReadOutboxMaxIdUpdatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        var pts = await IncrementPtsAsync(domainEvent.AggregateEvent.OwnerUserId);
        Emit(new UpdateOutboxMaxIdCompletedSagaEvent(domainEvent.AggregateEvent.OwnerUserId,
            domainEvent.AggregateEvent.RequestInfo.UserId, domainEvent.AggregateEvent.ReadOutboxMaxId, pts, 1));

        await CompleteAsync(cancellationToken);
    }

    private void UpdateReadOutboxMaxId(long senderUserId, int senderMessageId)
    {
        var command = new UpdateReadOutboxMaxIdCommand(
            DialogId.Create(senderUserId, PeerType.User, _state.RequestInfo.UserId), _state.RequestInfo,
            senderMessageId);
        Publish(command);
    }

    private async Task<int> IncrementPtsAsync(long userId)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, userId);
        Emit(new ReadHistoryPtsIncrementedSagaEvent(_state.RequestInfo, userId, pts));

        return pts;
    }

    private void CreateReadingHistory(long senderUserId, int senderMessageId)
    {
        var command = new CreateReadingHistoryCommand(
            ReadingHistoryId.Create(_state.RequestInfo.UserId, senderUserId, senderMessageId), _state.RequestInfo.UserId,
            senderUserId, senderMessageId, DateTime.UtcNow.ToTimestamp());
        Publish(command);
    }
}