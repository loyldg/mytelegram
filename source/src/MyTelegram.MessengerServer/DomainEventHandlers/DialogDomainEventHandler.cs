namespace MyTelegram.MessengerServer.DomainEventHandlers;

public class DialogDomainEventHandler : DomainEventHandlerBase,
    ISubscribeSynchronousTo<DialogAggregate, DialogId, ChannelHistoryClearedEvent>,
    ISubscribeSynchronousTo<DialogAggregate, DialogId, DialogPinChangedEvent>

{
    public DialogDomainEventHandler(IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService) : base(objectMessageSender,
        commandBus,
        idGenerator,
        ackCacheService,
        responseCacheAppService)
    {
    }

    public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, ChannelHistoryClearedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
                new TBoolTrue(),
                domainEvent.Metadata.SourceId.Value)
            .ConfigureAwait(false);
    }

    public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, DialogPinChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
                new TBoolTrue(),
                domainEvent.Metadata.SourceId.Value,
                domainEvent.AggregateEvent.OwnerPeerId)
            .ConfigureAwait(false);
    }
}
