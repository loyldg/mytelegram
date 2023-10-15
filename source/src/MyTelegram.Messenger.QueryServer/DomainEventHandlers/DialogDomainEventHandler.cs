using MyTelegram.Messenger.Services.Caching;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class DialogDomainEventHandler : DomainEventHandlerBase,
    ISubscribeSynchronousTo<DialogAggregate, DialogId, ChannelHistoryClearedEvent>,
    ISubscribeSynchronousTo<DialogAggregate, DialogId, DialogPinChangedEvent>,
    ISubscribeSynchronousTo<DialogFilterAggregate, DialogFilterId, DialogFilterUpdatedEvent>,
    ISubscribeSynchronousTo<DialogFilterAggregate, DialogFilterId, DialogFilterDeletedEvent>

{
    private readonly IObjectMapper _objectMapper;
    public DialogDomainEventHandler(IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService,
        IObjectMapper objectMapper) : base(objectMessageSender,
        commandBus,
        idGenerator,
        ackCacheService,
        responseCacheAppService)
    {
        _objectMapper = objectMapper;
    }

    public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, ChannelHistoryClearedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
                new TBoolTrue())
     ;
    }

    public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, DialogPinChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
                new TBoolTrue(),
                domainEvent.AggregateEvent.OwnerPeerId)
     ;
    }

    public async Task HandleAsync(IDomainEvent<DialogFilterAggregate, DialogFilterId, DialogFilterUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await NotifyDialogFilterUpdatedAsync(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.Filter.Id,
            domainEvent.AggregateEvent.Filter);
    }

    public async Task HandleAsync(IDomainEvent<DialogFilterAggregate, DialogFilterId, DialogFilterDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await NotifyDialogFilterUpdatedAsync(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.FilterId,
            null);
    }

    private async Task NotifyDialogFilterUpdatedAsync(RequestInfo requestInfo,
        int filterId,
        DialogFilter? dialogFilter)
    {
        IDialogFilter? filter = null;
        if (dialogFilter != null)
        {
            filter = _objectMapper.Map<DialogFilter, TDialogFilter>(dialogFilter);
        }

        var updates = new TUpdateShort
        {
            Update = new TUpdateDialogFilter
            {
                Filter = filter,
                Id = filterId,
            },
            Date = DateTime.UtcNow.ToTimestamp(),
        };

        await SendMessageToPeerAsync(new Peer(PeerType.User, requestInfo.UserId), updates, requestInfo.AuthKeyId)
     ;
    }
}
