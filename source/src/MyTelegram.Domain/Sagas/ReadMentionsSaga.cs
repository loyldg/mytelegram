namespace MyTelegram.Domain.Sagas;

public class ReadMentionsCompletedSagaEvent(
        RequestInfo requestInfo,
        long userId,
        Peer toPeer,
        int messageId,
        int unreadMentionsCount,
        int pts)
    : RequestAggregateEvent2<ReadMentionsSaga, ReadMentionsSagaId>(requestInfo)
{
    public long UserId { get; } = userId;
    public Peer ToPeer { get; } = toPeer;
    public int MessageId { get; } = messageId;
    public int UnreadMentionsCount { get; } = unreadMentionsCount;
    public int Pts { get; } = pts;
    public int PtsCount { get; } = 0;
}

public class ReadMentionsSaga : MyInMemoryAggregateSaga<ReadMentionsSaga, ReadMentionsSagaId, ReadMentionsSagaLocator>,
    ISagaIsStartedBy<DialogAggregate, DialogId, MentionReadEvent>
{
    private readonly IIdGenerator _idGenerator;

    public ReadMentionsSaga(ReadMentionsSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
    }

    public async Task HandleAsync(
        IDomainEvent<DialogAggregate, DialogId, MentionReadEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var pts = await _idGenerator.NextIdAsync(
            IdType.Pts,
            domainEvent.AggregateEvent.OwnerUserId,
            cancellationToken: cancellationToken);

        Emit(new ReadMentionsCompletedSagaEvent(
            domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.OwnerUserId,
            domainEvent.AggregateEvent.ToPeer,
            domainEvent.AggregateEvent.MessageId,
            domainEvent.AggregateEvent.UnreadMentionsCount,
            pts));

        await CompleteAsync(cancellationToken);
    }
}
