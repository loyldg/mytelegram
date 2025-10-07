namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class ReadMentionsDomainEventHandler(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IPushDataFactory pushDataFactory)
    : DomainEventHandlerBase(objectMessageSender, commandBus, idGenerator, ackCacheService, pushDataFactory),
        ISubscribeSynchronousTo<ReadMentionsSaga, ReadMentionsSagaId, ReadMentionsCompletedSagaEvent>
{
    public async Task HandleAsync(
        IDomainEvent<ReadMentionsSaga, ReadMentionsSagaId, ReadMentionsCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var affectedHistory = new TAffectedHistory
        {
            Pts = domainEvent.AggregateEvent.Pts,
            PtsCount = domainEvent.AggregateEvent.PtsCount,
            Offset = 0
        };

        await SendRpcMessageToClientAsync(
            domainEvent.AggregateEvent.RequestInfo,
            affectedHistory,
            domainEvent.AggregateEvent.UserId,
            domainEvent.AggregateEvent.Pts);
    }
}
