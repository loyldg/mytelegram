using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.Services.Interfaces;
using MyTelegram.Messenger.TLObjectConverters.Interfaces;
using MyTelegram.Services.TLObjectConverters;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class ChannelDomainEventHandler : DomainEventHandlerBase,
    ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelCreatedEvent>,
    ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelInviteExportedEvent>,
    ISubscribeSynchronousTo<ChannelAggregate, ChannelId, StartInviteToChannelEvent>,
    ISubscribeSynchronousTo<ChannelAggregate, ChannelId, SetDiscussionGroupEvent>,
    ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelTitleEditedEvent>,
    ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelAboutEditedEvent>,
    ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelDefaultBannedRightsEditedEvent>,
    ISubscribeSynchronousTo<ChannelAggregate, ChannelId, SlowModeChangedEvent>,
    ISubscribeSynchronousTo<ChannelAggregate, ChannelId, PreHistoryHiddenChangedEvent>,
    ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelAdminRightsEditedEvent>,
    ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelUserNameChangedEvent>,
    ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent>,
    ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent>,
    ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent>,
    ISubscribeSynchronousTo<InviteToChannelSaga, InviteToChannelSagaId, InviteToChannelCompletedEvent>,
    ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelInviteEditedEvent>
{
    //private readonly ITlChatConverter _chatConverter;
    private readonly IChatEventCacheHelper _chatEventCacheHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IChatConverter> _chatLayeredService;
    private readonly IPhotoAppService _photoAppService;
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;
    private readonly ILayeredService<IUpdatesConverter> _updateLayeredService;

    public ChannelDomainEventHandler(IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService,
        IChatEventCacheHelper chatEventCacheHelper,
        IQueryProcessor queryProcessor,
        ILayeredService<IChatConverter> chatLayeredService, IPhotoAppService photoAppService, IOptions<MyTelegramMessengerServerOptions> options, ILayeredService<IUpdatesConverter> updateLayeredService) : base(objectMessageSender,
        commandBus,
        idGenerator,
        ackCacheService,
        responseCacheAppService)
    {
        _chatEventCacheHelper = chatEventCacheHelper;
        _queryProcessor = queryProcessor;
        _chatLayeredService = chatLayeredService;
        _photoAppService = photoAppService;
        _options = options;
        _updateLayeredService = updateLayeredService;
        //_chatConverter = chatConverter;
    }

    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelAboutEditedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
            new TBoolTrue());
    }

    public async Task HandleAsync(
        IDomainEvent<ChannelAggregate, ChannelId, ChannelAdminRightsEditedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId)
     ;
    }

    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        _chatEventCacheHelper.Add(domainEvent.AggregateEvent);
        return Task.CompletedTask;
    }

    public async Task HandleAsync(
        IDomainEvent<ChannelAggregate, ChannelId, ChannelDefaultBannedRightsEditedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo, domainEvent.AggregateEvent.ChannelId);
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelTitleEditedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo with { ReqMsgId = 0 }, domainEvent.AggregateEvent.ChannelId);
    }

    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelUserNameChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, new TBoolTrue());
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelInviteExportedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var data = _chatLayeredService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer).ToExportedChatInvite(domainEvent.AggregateEvent);
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
            data
        );
    }

    public async Task HandleAsync(
        IDomainEvent<ChannelAggregate, ChannelId, PreHistoryHiddenChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo, domainEvent.AggregateEvent.ChannelId);
    }

    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, SetDiscussionGroupEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
            new TBoolTrue());
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, SlowModeChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo, domainEvent.AggregateEvent.ChannelId);
    }

    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, StartInviteToChannelEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        _chatEventCacheHelper.Add(domainEvent.AggregateEvent);
        return Task.CompletedTask;
    }

    public Task HandleAsync(
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.MemberUid);
    }

    public async Task HandleAsync(
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var channelReadModel = await _queryProcessor
            .ProcessAsync(new GetChannelByIdQuery(domainEvent.AggregateEvent.ChannelId));
        var photoReadModel = await _photoAppService.GetPhotoAsync(channelReadModel.PhotoId);

        var updates = new TUpdates
        {
            Chats = new TVector<IChat>(_chatLayeredService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
                .ToChannel(
                    domainEvent.AggregateEvent.MemberUserId,
                    channelReadModel,
                    photoReadModel,
                null,
                false)),
            Date = domainEvent.AggregateEvent.Date,
            Seq = 0,
            Users = new TVector<IUser>(),
            Updates = new TVector<IUpdate>()
        };

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, updates);
        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo with { ReqMsgId = 0 }, domainEvent.AggregateEvent.ChannelId);
    }

    public Task HandleAsync(
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.MemberUserId);
    }

    public async Task HandleAsync(IDomainEvent<InviteToChannelSaga, InviteToChannelSagaId, InviteToChannelCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.Broadcast)
        {
            var updates = new TUpdateShort
            {
                Date = DateTime.UtcNow.ToTimestamp(),
                Update = new TUpdateChannel
                {
                    ChannelId = domainEvent.AggregateEvent.ChannelId,
                }
            };
            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, updates);

            var item = await GetChannelAsync(domainEvent.AggregateEvent.ChannelId);
            var inviteToChannelDefaultPts = 1;
            foreach (var userId in domainEvent.AggregateEvent.MemberUidList)
            {
                await NotifyUpdateChannelAsync(
                    domainEvent.AggregateEvent.RequestInfo,
                    domainEvent.AggregateEvent.ChannelId,
                    userId,
                    item.Item1,
                    item.Item2,
                    inviteToChannelDefaultPts
                    );
            }
        }
    }

    private async Task<(IChannelReadModel, IPhotoReadModel?)> GetChannelAsync(long channelId)
    {
        var channelReadModel = await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(channelId));
        var photoReadModel = channelReadModel.PhotoId.HasValue ? await _queryProcessor.ProcessAsync(new GetPhotoByIdQuery(channelReadModel.PhotoId.Value)) : null;

        return (channelReadModel, photoReadModel);
    }


    private async Task NotifyUpdateChannelAsync(RequestInfo requestInfo,
        long channelId,
        long memberUserId = 0,
        IChannelReadModel? channelReadModel = null,
        IPhotoReadModel? channelPhotoReadModel = null,
        int pts = 0
        )
    {
        if (channelReadModel == null)
        {
            var item = await GetChannelAsync(channelId);
            channelReadModel = item.Item1;
            channelPhotoReadModel = item.Item2;
        }

        var updates = _updateLayeredService.GetConverter(requestInfo.Layer)
            .ToChannelUpdates(memberUserId, channelReadModel, channelPhotoReadModel);

        var layeredUpdates = _updateLayeredService.GetLayeredData(c =>
              c.ToChannelUpdates(memberUserId, channelReadModel, channelPhotoReadModel));

        await SendRpcMessageToClientAsync(requestInfo, updates);
        if (memberUserId != 0)
        {
            await PushUpdatesToPeerAsync(new Peer(PeerType.Channel, channelId),
                //channelId,
                updates,
                onlySendToUserId: memberUserId,
                layeredData: layeredUpdates, pts: pts);
        }
        else
        {
            await PushUpdatesToPeerAsync(new Peer(PeerType.Channel, channelId),
                //channelId, 
                updates);
        }
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelInviteEditedEvent> domainEvent, CancellationToken cancellationToken)
    {
        var exportedChatInvite = new TExportedChatInvite
        {
            Invite = new Schema.TChatInviteExported
            {
                RequestNeeded = domainEvent.AggregateEvent.RequestNeeded,
                Link = $"{_options.Value.JoinChatDomain}/+{domainEvent.AggregateEvent.Link}",
                AdminId = domainEvent.AggregateEvent.AdminId,
                Date = DateTime.UtcNow.ToTimestamp(),
                StartDate = domainEvent.AggregateEvent.StartDate,
                ExpireDate = domainEvent.AggregateEvent.ExpireDate,
                Permanent = domainEvent.AggregateEvent.Permanent,
                Requested = domainEvent.AggregateEvent.Requested,
                Revoked = domainEvent.AggregateEvent.Revoke,
                Title = domainEvent.AggregateEvent.Title,
                UsageLimit = domainEvent.AggregateEvent.UsageLimit,
                Usage = domainEvent.AggregateEvent.Usage,
            },
            Users = new()
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, exportedChatInvite);
    }
}
