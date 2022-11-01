using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUpdateMessagePoll = MyTelegram.Schema.TUpdateMessagePoll;

namespace MyTelegram.MessengerServer.DomainEventHandlers;

public class VoteDomainEventHandler :
    DomainEventHandlerBase,
    ISubscribeSynchronousTo<VoteSaga, VoteSagaId, VoteSagaCompletedEvent>
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ITlPollConverter _pollConverter;

    public VoteDomainEventHandler(IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService,
        IQueryProcessor queryProcessor,
        ITlPollConverter pollConverter) : base(objectMessageSender, commandBus, idGenerator, ackCacheService,
        responseCacheAppService)
    {
        _queryProcessor = queryProcessor;
        _pollConverter = pollConverter;
    }

    public async Task HandleAsync(IDomainEvent<VoteSaga, VoteSagaId, VoteSagaCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var pollReadModel = await _queryProcessor
            .ProcessAsync(new GetPollQuery(domainEvent.AggregateEvent.ToPeer.PeerId, domainEvent.AggregateEvent.PollId),
                default).ConfigureAwait(false);
        if (pollReadModel != null)
        {
            var selfUpdates = _pollConverter.ToSelfPollUpdates(pollReadModel,
                domainEvent.AggregateEvent.ChosenOptions.ToList());
            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo.ReqMsgId, selfUpdates)
                .ConfigureAwait(false);

            await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.RequestInfo.UserId),
                selfUpdates,
                domainEvent.AggregateEvent.RequestInfo.AuthKeyId).ConfigureAwait(false);

            // 
            var updatesForMember = _pollConverter.ToPollUpdates(pollReadModel, Array.Empty<string>());
            await SendMessageToPeerAsync(domainEvent.AggregateEvent.ToPeer,
                updatesForMember,
                excludeAuthKeyId: domainEvent.AggregateEvent.RequestInfo.AuthKeyId).ConfigureAwait(false);
        }
    }
}

