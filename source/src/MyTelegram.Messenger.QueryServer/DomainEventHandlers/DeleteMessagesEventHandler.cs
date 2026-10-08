namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class DeleteMessagesEventHandler(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IUpdatesConverterService updatesConverterService)
    : DomainEventHandlerBase(objectMessageSender, commandBus, idGenerator, ackCacheService),
        ISubscribeAsynchronousTo<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteSelfMessagesCompletedSagaEvent>,
        ISubscribeAsynchronousTo<DeleteMessagesSaga4, DeleteMessagesSaga4Id,
            DeleteOtherParticipantMessagesCompletedSagaEvent>,
        ISubscribeAsynchronousTo<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteSelfHistoryCompletedSagaEvent>,
        ISubscribeAsynchronousTo<DeleteMessagesSaga4, DeleteMessagesSaga4Id,
            DeleteOtherParticipantHistoryCompletedSagaEvent>,
        ISubscribeAsynchronousTo<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId,
            DeleteChannelMessagesCompletedSagaEvent>,
        ISubscribeAsynchronousTo<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId,
            DeleteChannelHistoryCompletedSagaEvent>,
        ISubscribeAsynchronousTo<DialogAggregate, DialogId, ChannelHistoryClearedEvent> 
{
    public async Task HandleAsync(
        IDomainEvent<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId, DeleteChannelHistoryCompletedSagaEvent>
            domainEvent, CancellationToken cancellationToken)
    {
        var nextMaxId = domainEvent.AggregateEvent.MessageIds.Min();
        var r = new TAffectedHistory
        {
            Pts = domainEvent.AggregateEvent.Pts,
            PtsCount = domainEvent.AggregateEvent.PtsCount,
            Offset = nextMaxId
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
        var date = DateTime.UtcNow.ToTimestamp();
        var deletedBoxItem = new DeletedBoxItem(domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.Pts,
            domainEvent.AggregateEvent.PtsCount,
            domainEvent.AggregateEvent.MessageIds);
        var updates = updatesConverterService
            .ToDeleteMessagesUpdates(PeerType.Channel,
                deletedBoxItem,
                date);
        //var layeredData = layeredUpdatesService.GetLayeredData(c =>
        //    c.ToDeleteMessagesUpdates(PeerType.Channel,
        //        deletedBoxItem,
        //        date));
        await PushUpdatesToPeerAsync(
            new Peer(PeerType.Channel, domainEvent.AggregateEvent.ChannelId),
            updates,
            pts: domainEvent.AggregateEvent.Pts //,
            //layeredData: layeredData
        );
    }

    public async Task HandleAsync(
        IDomainEvent<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId, DeleteChannelMessagesCompletedSagaEvent>
            domainEvent, CancellationToken cancellationToken)
    {
        var channelId = domainEvent.AggregateEvent.ChannelId;
        var updates = updatesConverterService.ToDeleteMessagesUpdates(PeerType.Channel,
            new DeletedBoxItem(channelId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp());

        var globalSeqNo = await SavePushUpdatesAsync(channelId, updates, domainEvent.AggregateEvent.Pts,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId, chats: new List<long> { channelId });
        await AddRpcGlobalSeqNoForAuthKeyIdAsync(domainEvent.AggregateEvent.RequestInfo.ReqMsgId,
            domainEvent.AggregateEvent.RequestInfo.UserId, globalSeqNo);

        var r = new TAffectedMessages
        {
            Pts = domainEvent.AggregateEvent.Pts,
            PtsCount = domainEvent.AggregateEvent.PtsCount
        };

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r,
            domainEvent.AggregateEvent.RequestInfo.UserId,
            domainEvent.AggregateEvent.Pts,
            PeerType.Channel
        );

        await PushUpdatesToChannelMemberAsync(domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer(),
            channelId.ToChannelPeer(),
            updates,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
            skipSaveUpdates: true
        );
    }

    public Task HandleAsync(
        IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteOtherParticipantHistoryCompletedSagaEvent>
            domainEvent, CancellationToken cancellationToken)
    {
        var updates = updatesConverterService.ToDeleteMessagesUpdates(PeerType.User,
            new DeletedBoxItem(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp());
        //var layeredUpdates = layeredUpdatesService.GetLayeredData(c => c.ToDeleteMessagesUpdates(PeerType.User,
        //    new DeletedBoxItem(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts,
        //        domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
        //    DateTime.UtcNow.ToTimestamp()));

        return PushUpdatesToPeerAsync(domainEvent.AggregateEvent.UserId.ToUserPeer(), updates,
            pts: domainEvent.AggregateEvent.Pts /*, layeredData: layeredUpdates*/);
    }

    public Task HandleAsync(
        IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteOtherParticipantMessagesCompletedSagaEvent>
            domainEvent, CancellationToken cancellationToken)
    {
        var updates = updatesConverterService.ToDeleteMessagesUpdates(PeerType.User,
            new DeletedBoxItem(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp());
        //var layeredUpdates = layeredUpdatesService.GetLayeredData(c => c.ToDeleteMessagesUpdates(PeerType.User,
        //    new DeletedBoxItem(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts,
        //        domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
        //    DateTime.UtcNow.ToTimestamp()));

        return PushUpdatesToPeerAsync(domainEvent.AggregateEvent.UserId.ToUserPeer(), updates,
            pts: domainEvent.AggregateEvent.Pts);
    }

    public async Task HandleAsync(
        IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteSelfHistoryCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        IObject r = new TAffectedHistory
        {
            Pts = domainEvent.AggregateEvent.Pts,
            PtsCount = domainEvent.AggregateEvent.PtsCount,
            Offset = domainEvent.AggregateEvent.Offset
        };
        if (domainEvent.AggregateEvent.IsDeletePhoneCallHistory)
        {
            r = new TAffectedFoundMessages
            {
                Pts = domainEvent.AggregateEvent.Pts,
                PtsCount = domainEvent.AggregateEvent.PtsCount,
                Offset = domainEvent.AggregateEvent.Offset,
                Messages = new TVector<int>(domainEvent.AggregateEvent.MessageIds)
            };
        }

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
        var selfOtherDeviceUpdates = updatesConverterService.ToDeleteMessagesUpdates(PeerType.User,
            new DeletedBoxItem(domainEvent.AggregateEvent.RequestInfo.UserId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp());

        await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer(), selfOtherDeviceUpdates,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId);
    }

    public async Task HandleAsync(
        IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteSelfMessagesCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var r = new TAffectedMessages
        {
            Pts = domainEvent.AggregateEvent.Pts,
            PtsCount = domainEvent.AggregateEvent.PtsCount
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
            r,
            domainEvent.AggregateEvent.RequestInfo.UserId, domainEvent.AggregateEvent.Pts);

        var selfOtherDeviceUpdates = updatesConverterService.ToDeleteMessagesUpdates(PeerType.User,
            new DeletedBoxItem(domainEvent.AggregateEvent.RequestInfo.UserId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp());
        await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer(), selfOtherDeviceUpdates,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId, pts: domainEvent.AggregateEvent.Pts);
    }

    public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, ChannelHistoryClearedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var updateChannelAvailableMessages = new TUpdateChannelAvailableMessages
        {
            ChannelId = domainEvent.AggregateEvent.ChannelId,
            AvailableMinId = domainEvent.AggregateEvent.HistoryMinId
        };
        var updates = new TUpdates
        {
            Updates = new TVector<IUpdate>(updateChannelAvailableMessages),
            Users = new TVector<IUser>(),
            Chats = new TVector<IChat>(),
            Date = DateTime.UtcNow.ToTimestamp(),
            Seq = 0
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, updates);

        await PushMessageToPeerAsync(domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer(), updates,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId);
    }
}