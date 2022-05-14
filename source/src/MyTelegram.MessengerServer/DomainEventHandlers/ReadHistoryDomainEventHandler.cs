using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.DomainEventHandlers;

public class ReadHistoryDomainEventHandler : DomainEventHandlerBase,
    ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, ReadHistoryCompletedEvent>,
    ISubscribeSynchronousTo<ReadChannelHistorySaga, ReadChannelHistorySagaId, ReadChannelHistoryCompletedEvent>
{
    private readonly IPeerHelper _peerHelper;
    private readonly ITlUpdatesConverter _updatesConverter;

    public ReadHistoryDomainEventHandler(IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService,
        ITlUpdatesConverter updatesConverter,
        IPeerHelper peerHelper) : base(objectMessageSender,
        commandBus,
        idGenerator,
        ackCacheService,
        responseCacheAppService)
    {
        _updatesConverter = updatesConverter;
        _peerHelper = peerHelper;
    }

    public async Task HandleAsync(
        IDomainEvent<ReadChannelHistorySaga, ReadChannelHistorySagaId, ReadChannelHistoryCompletedEvent>
            domainEvent,
        CancellationToken cancellationToken)
    {
        //TestConsole2.WriteLine($"Read channel history ok.reqMsgId:{domainEvent.AggregateEvent.ReqMsgId:x2}");

        //todo:这里的selfUid不是senderUid
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
                new TBoolTrue(),
                domainEvent.Metadata.SourceId.Value,
                domainEvent.AggregateEvent.SenderPeerId)
            .ConfigureAwait(false);

        if (domainEvent.AggregateEvent.NeedNotifySender)
        {
            var data = new TUpdateReadChannelOutbox
            {
                ChannelId = domainEvent.AggregateEvent.ChannelId,
                MaxId = domainEvent.AggregateEvent.MessageId
            };
            var updates = new TUpdateShort { Update = data, Date = DateTime.UtcNow.ToTimestamp() };
            await PushUpdatesToPeerAsync(
                    new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId),
                    updates)
                .ConfigureAwait(false);
        }

        //await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
        //    () => new TUpdateReadChannelOutbox
        //    {
        //        ChannelId = domainEvent.AggregateEvent.
        //    });
    }

    public async Task HandleAsync(
        IDomainEvent<ReadHistorySaga, ReadHistorySagaId, ReadHistoryCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var affectedMessages = new TAffectedMessages { Pts = domainEvent.AggregateEvent.ReaderPts, PtsCount = 1 };

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.Request.ReqMsgId,
                affectedMessages,
                domainEvent.AggregateEvent.SourceCommandId,
                domainEvent.AggregateEvent.ReaderUid)
            .ConfigureAwait(false);
        var peer = domainEvent.AggregateEvent.ReaderToPeer;
        var updateReadHistoryInbox = new TUpdateReadHistoryInbox
        {
            Peer = peer.ToPeer(),
            MaxId = domainEvent.AggregateEvent.ReaderMessageId,
            Pts = domainEvent.AggregateEvent.ReaderPts,
            PtsCount = 1
        };
        var selfOtherDevicesUpdates = new TUpdates
        {
            Updates = new TVector<IUpdate>(updateReadHistoryInbox),
            Users = new TVector<IUser>(),
            Chats = new TVector<IChat>(),
            Date = DateTime.UtcNow.ToTimestamp()
        };
        await PushUpdatesToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.ReaderUid),
            selfOtherDevicesUpdates,
            domainEvent.AggregateEvent.Request.AuthKeyId,
            pts: domainEvent.AggregateEvent.ReaderPts,
            ptsType: PtsType.OtherUpdates
        ).ConfigureAwait(false);

        if (!domainEvent.AggregateEvent.IsOut && !domainEvent.AggregateEvent.OutboxAlreadyRead &&
            !_peerHelper.IsBotUser(domainEvent.AggregateEvent.SenderPeerId))
        {
            var readHistoryUpdates =
                _updatesConverter.ToReadHistoryUpdates(domainEvent.AggregateEvent);
            var toPeer = new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId);
            await PushUpdatesToPeerAsync(
                toPeer,
                readHistoryUpdates,
                domainEvent.AggregateEvent.Request.AuthKeyId,
                pts: domainEvent.AggregateEvent.ReaderPts,
                ptsType: PtsType.OtherUpdates
            ).ConfigureAwait(false);
        }
    }
}
