namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class VoteDomainEventHandler(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IQueryProcessor queryProcessor,
    ISendVoteConverterService sendVoteConverterService
    )
    :
        DomainEventHandlerBase(objectMessageSender, commandBus, idGenerator, ackCacheService),
        ISubscribeSynchronousTo<VoteSaga, VoteSagaId, VoteSagaCompletedSagaEvent>
{
    public async Task HandleAsync(IDomainEvent<VoteSaga, VoteSagaId, VoteSagaCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var pollReadModel = await queryProcessor
            .ProcessAsync(new GetPollQuery(domainEvent.AggregateEvent.PollId), cancellationToken);
        if (pollReadModel != null)
        {
            var selfUpdates = sendVoteConverterService.ToSelfUpdates(pollReadModel,
                domainEvent.AggregateEvent.ChosenOptions.ToList(), domainEvent.AggregateEvent.RequestInfo.Layer);
            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, selfUpdates)
         ;

            await PushMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.RequestInfo.UserId),
                selfUpdates,
                domainEvent.AggregateEvent.RequestInfo.AuthKeyId);

            var updatesForMember = sendVoteConverterService.ToUpdates(pollReadModel, []);
            await PushMessageToPeerAsync(domainEvent.AggregateEvent.ToPeer,
                updatesForMember,
                excludeAuthKeyId: domainEvent.AggregateEvent.RequestInfo.AuthKeyId);
        }
    }
}

