namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class DialogDomainEventHandler(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IObjectMapper objectMapper)
    : DomainEventHandlerBase(objectMessageSender, commandBus, idGenerator, ackCacheService),
        ISubscribeSynchronousTo<DialogAggregate, DialogId, ChannelHistoryClearedEvent>,
        ISubscribeSynchronousTo<DialogAggregate, DialogId, DialogPinChangedEvent>,
        ISubscribeSynchronousTo<DialogFilterAggregate, DialogFilterId, DialogFilterUpdatedEvent>,
        ISubscribeSynchronousTo<DialogFilterAggregate, DialogFilterId, DialogFilterDeletedEvent>,
        ISubscribeSynchronousTo<EditPeerFoldersSaga, EditPeerFoldersSagaId, EditPeerFoldersCompletedSagaEvent>

{
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
            filter = objectMapper.Map<DialogFilter, TDialogFilter>(dialogFilter);
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

        await PushMessageToPeerAsync(new Peer(PeerType.User, requestInfo.UserId), updates, requestInfo.AuthKeyId)
     ;
    }

    public Task HandleAsync(IDomainEvent<EditPeerFoldersSaga, EditPeerFoldersSagaId, EditPeerFoldersCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        var folderPeers = new TVector<IFolderPeer>(domainEvent.AggregateEvent.FolderPeers.Select(p => new TFolderPeer
        {
            FolderId = p.FolderId,
            Peer = p.Peer.ToPeer()
        }));

        var updateFolderPeers = new TUpdateFolderPeers
        {
            FolderPeers = folderPeers,
            Pts = domainEvent.AggregateEvent.Pts,
            PtsCount = domainEvent.AggregateEvent.PtsCount
        };

        var updates = new TUpdates
        {
            Updates = new TVector<IUpdate>(updateFolderPeers),
            Users = [],
            Chats = [],
            Date = DateTime.UtcNow.ToTimestamp(),
        };

        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, updates);
    }
}
