using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.TLObjectConverters.Interfaces;
using MyTelegram.Services.TLObjectConverters;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class VoteDomainEventHandler :
    DomainEventHandlerBase,
    ISubscribeSynchronousTo<VoteSaga, VoteSagaId, VoteSagaCompletedEvent>
{
    private readonly IQueryProcessor _queryProcessor;
    //private readonly ITlPollConverter _pollConverter;
    private readonly ILayeredService<IPollConverter> _layeredService;

    public VoteDomainEventHandler(IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService,
        IQueryProcessor queryProcessor,
        ILayeredService<IPollConverter> layeredService) : base(objectMessageSender, commandBus, idGenerator, ackCacheService,
        responseCacheAppService)
    {
        _queryProcessor = queryProcessor;
        _layeredService = layeredService;
    }

    public async Task HandleAsync(IDomainEvent<VoteSaga, VoteSagaId, VoteSagaCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var pollReadModel = await _queryProcessor
            .ProcessAsync(new GetPollQuery(domainEvent.AggregateEvent.ToPeer.PeerId, domainEvent.AggregateEvent.PollId),
                default);
        if (pollReadModel != null)
        {
            var selfUpdates = _layeredService.Converter.ToSelfPollUpdates(pollReadModel,
                domainEvent.AggregateEvent.ChosenOptions.ToList());
            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, selfUpdates)
         ;

            await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.RequestInfo.UserId),
                selfUpdates,
                domainEvent.AggregateEvent.RequestInfo.AuthKeyId);

            // 
            var updatesForMember = _layeredService.Converter.ToPollUpdates(pollReadModel, Array.Empty<string>());
            await SendMessageToPeerAsync(domainEvent.AggregateEvent.ToPeer,
                updatesForMember,
                excludeAuthKeyId: domainEvent.AggregateEvent.RequestInfo.AuthKeyId);
        }
    }
}

