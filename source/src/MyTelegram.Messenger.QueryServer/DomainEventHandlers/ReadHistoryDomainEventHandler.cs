using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.TLObjectConverters.Interfaces;
using MyTelegram.Services.Extensions;
using MyTelegram.Services.TLObjectConverters;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class ReadHistoryDomainEventHandler : DomainEventHandlerBase,
    ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, ReadHistoryCompletedEvent>,
    ISubscribeSynchronousTo<ReadChannelHistorySaga, ReadChannelHistorySagaId, ReadChannelHistoryCompletedEvent>
{
    private readonly IPeerHelper _peerHelper;
    //private readonly ITlUpdatesConverter _updatesConverter;
    private readonly ILayeredService<IUpdatesConverter> _layeredUpdatesService;

    public ReadHistoryDomainEventHandler(IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService,
        IPeerHelper peerHelper,
        ILayeredService<IUpdatesConverter> layeredUpdatesService) : base(objectMessageSender,
        commandBus,
        idGenerator,
        ackCacheService,
        responseCacheAppService)
    {
        _peerHelper = peerHelper;
        _layeredUpdatesService = layeredUpdatesService;
    }

    public async Task HandleAsync(
        IDomainEvent<ReadChannelHistorySaga, ReadChannelHistorySagaId, ReadChannelHistoryCompletedEvent>
            domainEvent,
        CancellationToken cancellationToken)
    {
        // Console.WriteLine($"Read channel history ok.reqMsgId:{domainEvent.AggregateEvent.ReqMsgId:x2} SenderPeerId={domainEvent.AggregateEvent.SenderPeerId} NeedNotifySender={domainEvent.AggregateEvent.NeedNotifySender}");

        //todo:这里的selfUid不是senderUid
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
                new TBoolTrue(),
                domainEvent.AggregateEvent.SenderPeerId)
     ;

        if (domainEvent.AggregateEvent.NeedNotifySender)
        {
            IUpdate data = domainEvent.AggregateEvent.TopMsgId.HasValue ? new TUpdateReadChannelDiscussionOutbox
            {
                ChannelId = domainEvent.AggregateEvent.ChannelId,
                TopMsgId = domainEvent.AggregateEvent.TopMsgId.Value,
                ReadMaxId = domainEvent.AggregateEvent.MessageId

            } : new TUpdateReadChannelOutbox
            {
                ChannelId = domainEvent.AggregateEvent.ChannelId,
                MaxId = domainEvent.AggregateEvent.MessageId,
            };
            // Console.WriteLine($"{data.GetType().FullName}");
            var updates = new TUpdateShort { Update = data, Date = DateTime.UtcNow.ToTimestamp() };
            await PushUpdatesToPeerAsync(
                    new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId),
                    updates)
         ;
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

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
                affectedMessages,
                domainEvent.AggregateEvent.ReaderUid)
     ;
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
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
            pts: domainEvent.AggregateEvent.ReaderPts
        //ptsType: PtsType.OtherUpdates
        );

        if (!domainEvent.AggregateEvent.IsOut && !domainEvent.AggregateEvent.OutboxAlreadyRead &&
            !_peerHelper.IsBotUser(domainEvent.AggregateEvent.SenderPeerId))
        {
            var readHistoryUpdates =
                _layeredUpdatesService.Converter.ToReadHistoryUpdates(domainEvent.AggregateEvent);

            var toPeer = new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId);
            await PushUpdatesToPeerAsync(
                toPeer,
                readHistoryUpdates,
                domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
                pts: domainEvent.AggregateEvent.ReaderPts
            );
        }
    }
}
