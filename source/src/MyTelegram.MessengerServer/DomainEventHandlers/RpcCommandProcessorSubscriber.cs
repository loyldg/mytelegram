//using MyTelegram.Schema.Auth;
//using MyTelegram.Schema.Messages;

//namespace MyTelegram.MessengerServer.DomainEventHandlers;

//public class RpcCommandProcessorSubscriber :
//        ISubscribeSynchronousTo<UserAggregate, UserId, UserCreatedEvent>,
//        ISubscribeSynchronousTo<SignInSaga, SignInSagaId, SignInSuccessEvent>,
//        ISubscribeSynchronousTo<AppCodeAggregate, AppCodeId, SignUpRequiredEvent>,
//        ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, ReadHistoryCompletedEvent>,
//        ISubscribeSynchronousTo<UserAggregate, UserId, UserProfileUpdatedEvent>,
//        ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatCreatedEvent>,
//        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelCreatedEvent>,
//        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ExportChatInviteEvent>,
//        ISubscribeSynchronousTo<ReadChannelHistorySaga, ReadChannelHistorySagaId, ReadChannelHistoryCompletedEvent>,
//        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, StartInviteToChannelEvent>,
//        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, SetDiscussionGroupEvent>,
//        ISubscribeSynchronousTo<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedMessageCompletedEvent>,
//        ISubscribeSynchronousTo<DeleteMessageSaga, DeleteMessageSagaId, DeleteMessagesCompletedEvent>,
//        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelTitleEditedEvent>,
//        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelAboutEditedEvent>,
//        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelDefaultBannedRightsEditedEvent>,
//        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, SlowModeChangedEvent>,
//        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, PreHistoryHiddenChangedEvent>,
//        ISubscribeSynchronousTo<DialogAggregate, DialogId, ChannelHistoryClearedEvent>,
//        ISubscribeSynchronousTo<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedEvent>,
//        ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatDefaultBannedRightsEditedEvent>,
//        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelAdminRightsEditedEvent>,
//        ISubscribeSynchronousTo<DialogAggregate, DialogId, DialogPinChangedEvent>,
//        ISubscribeSynchronousTo<UserAggregate, UserId, UserNameUpdatedEvent>,
//        ISubscribeSynchronousTo<UserAggregate, UserId, UserProfilePhotoChangedEvent>,
//        ISubscribeSynchronousTo<QrCodeAggregate, QrCodeId, QrCodeLoginTokenExportedEvent>,
//        ISubscribeSynchronousTo<QrCodeAggregate, QrCodeId, LoginTokenAcceptedEvent>,
//        ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatAboutEditedEvent>,
//        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelUserNameChangedEvent>,
//        ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent>,
//        ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent>,
//        ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent>
//{
//    private readonly IAckCacheService _ackCacheService;
//    private readonly ICommandBus _commandBus;
//    private readonly ICreatedChatCacheHelper _createdChatCacheHelper;
//    private readonly IEventBus _eventBus;
//    private readonly IIdGenerator _idGenerator;
//    private readonly ILogger<RpcCommandProcessorSubscriber> _logger;
//    private readonly ILoginTokenCacheAppService _loginTokenCacheAppService;
//    private readonly IObjectMessageSender _objectMessageSender;
//    private readonly IPeerHelper _peerHelper;
//    private readonly IQueryProcessor _queryProcessor;
//    private readonly IRpcResultProcessor _rpcResultProcessor;
//    private readonly ITlUpdatesConverter _updatesConverter;

//    public RpcCommandProcessorSubscriber(ILogger<RpcCommandProcessorSubscriber> logger,
//        IRpcResultProcessor rpcResultProcessor,
//        IQueryProcessor queryProcessor,
//        IPeerHelper peerHelper,
//        ICreatedChatCacheHelper createdChatCacheHelper,
//        ILoginTokenCacheAppService loginTokenCacheAppService,
//        IObjectMessageSender objectMessageSender,
//        IEventBus eventBus,
//        ICommandBus commandBus,
//        IIdGenerator idGenerator,
//        IAckCacheService ackCacheService,
//        ITlUpdatesConverter updatesConverter
//    )
//    {
//        _logger = logger;
//        _rpcResultProcessor = rpcResultProcessor;
//        _queryProcessor = queryProcessor;
//        _peerHelper = peerHelper;
//        _createdChatCacheHelper = createdChatCacheHelper;
//        _loginTokenCacheAppService = loginTokenCacheAppService;
//        _objectMessageSender = objectMessageSender;
//        _eventBus = eventBus;
//        _commandBus = commandBus;
//        _idGenerator = idGenerator;
//        _ackCacheService = ackCacheService;
//        _updatesConverter = updatesConverter;
//    }

//    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelAboutEditedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//            new TBoolTrue(),
//            domainEvent.Metadata.SourceId.Value);
//    }

//    public async Task HandleAsync(
//        IDomainEvent<ChannelAggregate, ChannelId, ChannelAdminRightsEditedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.ReqMsgId,
//                domainEvent.AggregateEvent.ChannelId,
//                domainEvent.Metadata.SourceId.Value)
//            .ConfigureAwait(false);
//    }

//    public async Task HandleAsync(
//        IDomainEvent<ChannelAggregate, ChannelId, ChannelDefaultBannedRightsEditedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.ReqMsgId,
//                domainEvent.AggregateEvent.ChannelId,
//                domainEvent.Metadata.SourceId.Value)
//            .ConfigureAwait(false);
//    }

//    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelTitleEditedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        await NotifyUpdateChannelAsync(0, domainEvent.AggregateEvent.ChannelId, domainEvent.Metadata.SourceId.Value)
//            .ConfigureAwait(false);
//    }

//    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelUserNameChangedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, new TBoolTrue());
//    }

//    public async Task HandleAsync(
//        IDomainEvent<ChannelAggregate, ChannelId, PreHistoryHiddenChangedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.ReqMsgId,
//                domainEvent.AggregateEvent.ChannelId,
//                domainEvent.Metadata.SourceId.Value)
//            .ConfigureAwait(false);
//    }

//    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, SetDiscussionGroupEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//            new TBoolTrue(),
//            domainEvent.Metadata.SourceId.Value);
//    }

//    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, SlowModeChangedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.ReqMsgId,
//                domainEvent.AggregateEvent.ChannelId,
//                domainEvent.Metadata.SourceId.Value)
//            .ConfigureAwait(false);
//    }

//    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, StartInviteToChannelEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        _createdChatCacheHelper.Add(domainEvent.AggregateEvent);
//        return Task.CompletedTask;
//    }

//    public Task HandleAsync(
//        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        return NotifyUpdateChannelAsync(domainEvent.AggregateEvent.ReqMsgId,
//            domainEvent.AggregateEvent.ChannelId,
//            null,
//            domainEvent.AggregateEvent.MemberUid);
//    }

//    public async Task HandleAsync(
//        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        // 这里直接从ReadModel获取频道信息
//        var channelReadModel = await _queryProcessor
//            .ProcessAsync(new GetChannelByIdQuery(domainEvent.AggregateEvent.ChannelId), default)
//            .ConfigureAwait(false);
//        var updates = new TUpdates
//        {
//            Chats = new TVector<IChat>(_rpcResultProcessor.ToChannel(channelReadModel,
//                null,
//                domainEvent.AggregateEvent.MemberUid)),
//            Date = domainEvent.AggregateEvent.Date,
//            Seq = 0,
//            Users = new TVector<IUser>(),
//            Updates = new TVector<IUpdate>()
//        };

//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, updates).ConfigureAwait(false);
//        await NotifyUpdateChannelAsync(0, domainEvent.AggregateEvent.ChannelId, null).ConfigureAwait(false);
//    }

//    public Task HandleAsync(
//        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        return NotifyUpdateChannelAsync(domainEvent.AggregateEvent.ReqMsgId,
//            domainEvent.AggregateEvent.ChannelId,
//            null,
//            domainEvent.AggregateEvent.MemberUid);
//    }

//    public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatAboutEditedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, new TBoolTrue());
//    }

//    public async Task HandleAsync(
//        IDomainEvent<ChatAggregate, ChatId, ChatDefaultBannedRightsEditedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        await NotifyUpdateChatAsync(domainEvent.AggregateEvent.ReqMsgId,
//            domainEvent.AggregateEvent.ChatId,
//            domainEvent.Metadata.SourceId.Value).ConfigureAwait(false);
//    }

//    public async Task HandleAsync(
//        IDomainEvent<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        if (domainEvent.AggregateEvent.IsSelf)
//        {
//            var r = new TAffectedHistory
//            {
//                Pts = domainEvent.AggregateEvent.DeletedBoxItem.Pts,
//                PtsCount = domainEvent.AggregateEvent.DeletedBoxItem.PtsCount,
//                Offset = domainEvent.AggregateEvent.NextMaxId
//            };
//            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//                r,
//                domainEvent.Metadata.SourceId.Value
//            ).ConfigureAwait(false);
//        }

//        var updates = _rpcResultProcessor.ToDeleteMessagesUpdates(domainEvent.AggregateEvent.ToPeerType,
//            domainEvent.AggregateEvent.DeletedBoxItem,
//            DateTime.UtcNow.ToTimestamp());
//        await PushUpdatesToPeerAsync(
//            new Peer(PeerType.User, domainEvent.AggregateEvent.DeletedBoxItem.OwnerPeerId),
//            updates,
//            pts: domainEvent.AggregateEvent.DeletedBoxItem.Pts).ConfigureAwait(false);
//    }

//    public async Task HandleAsync(
//        IDomainEvent<DeleteMessageSaga, DeleteMessageSagaId, DeleteMessagesCompletedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        var date = DateTime.UtcNow.ToTimestamp();
//        IUpdates? channelUpdates = null;
//        Peer channelPeer;
//        if (domainEvent.AggregateEvent.ToPeerType == PeerType.Channel)
//        {
//            channelPeer = new Peer(PeerType.Channel, domainEvent.AggregateEvent.SelfDeletedBoxItem.OwnerPeerId);
//            channelUpdates = _rpcResultProcessor.ToDeleteMessagesUpdates(domainEvent.AggregateEvent.ToPeerType,
//                domainEvent.AggregateEvent.SelfDeletedBoxItem,
//                date);
//            var globalSeqNo = await SavePushUpdatesAsync(
//                channelPeer,
//                channelUpdates.ToBytes(),
//                domainEvent.AggregateEvent.SelfDeletedBoxItem.Pts,
//                PtsType.OtherUpdates,
//                domainEvent.AggregateEvent.Request.AuthKeyId,
//                0,
//                0
//            ).ConfigureAwait(false);
//            await AddRpcGlobalSeqNoForAuthKeyIdAsync(domainEvent.AggregateEvent.Request.ReqMsgId,
//                domainEvent.AggregateEvent.Request.UserId,
//                globalSeqNo).ConfigureAwait(false);
//            await UpdateSelfGlobalSeqNoAfterSendChannelMessageAsync(domainEvent.AggregateEvent.Request.UserId,
//                globalSeqNo).ConfigureAwait(false);
//        }

//        var r = new TAffectedMessages
//        {
//            Pts = domainEvent.AggregateEvent.SelfDeletedBoxItem.Pts,
//            PtsCount = domainEvent.AggregateEvent.SelfDeletedBoxItem.PtsCount
//        };

//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.Request.ReqMsgId,
//            r,
//            domainEvent.Metadata.SourceId.Value,
//            domainEvent.AggregateEvent.Request.UserId,
//            domainEvent.AggregateEvent.SelfDeletedBoxItem.Pts,
//            toPeerType: domainEvent.AggregateEvent.ToPeerType).ConfigureAwait(false);

//        if (domainEvent.AggregateEvent.ToPeerType == PeerType.Channel)
//        {
//            channelPeer = new Peer(PeerType.Channel, domainEvent.AggregateEvent.SelfDeletedBoxItem.OwnerPeerId);
//            await PushUpdatesToChannelMemberAsync(channelPeer,
//                channelUpdates!,
//                domainEvent.AggregateEvent.Request.AuthKeyId,
//                skipSaveUpdates: true).ConfigureAwait(false);
//        }
//        else
//        {
//            foreach (var deletedBoxItem in domainEvent.AggregateEvent.DeletedBoxItems)
//            {
//                var excludeAuthKeyId = 0L;
//                if (deletedBoxItem.OwnerPeerId == domainEvent.AggregateEvent.SelfDeletedBoxItem.OwnerPeerId)
//                {
//                    excludeAuthKeyId = domainEvent.AggregateEvent.Request.AuthKeyId;
//                }

//                var updates =
//                    _rpcResultProcessor.ToDeleteMessagesUpdates(domainEvent.AggregateEvent.ToPeerType,
//                        deletedBoxItem,
//                        date);
//                await PushUpdatesToPeerAsync(
//                    new Peer(PeerType.User, deletedBoxItem.OwnerPeerId),
//                    updates,
//                    excludeAuthKeyId,
//                    pts: deletedBoxItem.Pts
//                ).ConfigureAwait(false);
//            }
//        }
//    }

//    public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, ChannelHistoryClearedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//                new TBoolTrue(),
//                domainEvent.Metadata.SourceId.Value)
//            .ConfigureAwait(false);
//    }

//    public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, DialogPinChangedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//                new TBoolTrue(),
//                domainEvent.Metadata.SourceId.Value,
//                domainEvent.AggregateEvent.OwnerPeerId)
//            .ConfigureAwait(false);
//    }

//    public async Task HandleAsync(IDomainEvent<QrCodeAggregate, QrCodeId, LoginTokenAcceptedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        var deviceReadModel = await _queryProcessor
//            .ProcessAsync(new GetDeviceByAuthKeyIdQuery(domainEvent.AggregateEvent.QrCodeLoginRequestPermAuthKeyId),
//                default).ConfigureAwait(false);

//        if (deviceReadModel == null)
//        {
//            _logger.LogWarning(
//                "Get device info failed.perm authKeyId={PermAuthKeyId:x2}",
//                domainEvent.AggregateEvent.QrCodeLoginRequestPermAuthKeyId);
//            return;
//        }

//        _loginTokenCacheAppService.AddLoginSuccessAuthKeyIdToCache(
//            domainEvent.AggregateEvent.QrCodeLoginRequestTempAuthKeyId,
//            domainEvent.AggregateEvent.UserId);
//        var authorization = _rpcResultProcessor.ToAuthorization(deviceReadModel);
//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, authorization)
//            .ConfigureAwait(false);

//        var updateShortForLoginWithTokenRequestOwner =
//            new TUpdateShort { Date = DateTime.UtcNow.ToTimestamp(), Update = new TUpdateLoginToken() };

//        await _objectMessageSender
//            .PushSessionMessageToAuthKeyIdAsync(domainEvent.AggregateEvent.QrCodeLoginRequestTempAuthKeyId,
//                updateShortForLoginWithTokenRequestOwner).ConfigureAwait(false);
//        _logger.LogDebug("Accept qr code login token,userId={UserId},qr code client authKeyId={AuthKeyId}",
//            domainEvent.AggregateEvent.UserId,
//            domainEvent.AggregateEvent.QrCodeLoginRequestTempAuthKeyId);
//    }

//    public async Task HandleAsync(
//        IDomainEvent<QrCodeAggregate, QrCodeId, QrCodeLoginTokenExportedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        var r = new TLoginToken
//        {
//            Token = domainEvent.AggregateEvent.Token,
//            Expires = domainEvent.AggregateEvent.ExpireDate
//        };

//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r).ConfigureAwait(false);
//    }

//    public async Task HandleAsync(IDomainEvent<SignInSaga, SignInSagaId, SignInSuccessEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        var tempAuthKeyId = domainEvent.AggregateEvent.TempAuthKeyId;

//        await _eventBus.PublishAsync(new UserSignInSuccessEvent(tempAuthKeyId,
//                domainEvent.AggregateEvent.PermAuthKeyId,
//                domainEvent.AggregateEvent.UserId,
//                domainEvent.AggregateEvent.HasPassword ? PasswordState.WaitingForVerify : PasswordState.None))
//            .ConfigureAwait(false);
//        _logger.LogDebug(
//            "########################### User sign in success:{UserId} with tempAuthKeyId:{TempAuthKeyId} permAuthKeyId:{PermAuthKeyId}",
//            domainEvent.AggregateEvent.UserId,
//            domainEvent.AggregateEvent.TempAuthKeyId,
//            domainEvent.AggregateEvent.PermAuthKeyId);

//        if (domainEvent.AggregateEvent.HasPassword)
//        {
//            var rpcError = new TRpcError
//            {
//                ErrorCode = MyTelegramServerDomainConsts.BadRequestErrorCode,
//                ErrorMessage = RpcErrorMessages.SessionPasswordNeeded
//            };
//            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, rpcError).ConfigureAwait(false);
//            return;
//        }

//        var r = _rpcResultProcessor.CreateAuthorization(domainEvent.AggregateEvent);

//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r).ConfigureAwait(false);
//    }

//    public async Task HandleAsync(
//        IDomainEvent<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedMessageCompletedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        var r = _updatesConverter.ToUpdatePinnedMessageUpdates(domainEvent.AggregateEvent);
//        if (domainEvent.AggregateEvent.PmOneSide || domainEvent.AggregateEvent.ShouldReplyRpcResult)
//        {
//            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//                r,
//                domainEvent.Metadata.SourceId.Value,
//                domainEvent.AggregateEvent.SenderPeerId,
//                domainEvent.AggregateEvent.Pts,
//                toPeerType: domainEvent.AggregateEvent.ToPeer.PeerType
//            ).ConfigureAwait(false);
//            await PushUpdatesToPeerAsync(
//                new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId),
//                r,
//                pts: domainEvent.AggregateEvent.Pts).ConfigureAwait(false);
//        }

//        await PushUpdatesToPeerAsync(
//            domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel
//                ? new Peer(PeerType.Channel, domainEvent.AggregateEvent.OwnerPeerId)
//                : new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId),
//            r,
//            excludeUid: domainEvent.AggregateEvent.SenderPeerId,
//            pts: domainEvent.AggregateEvent.Pts).ConfigureAwait(false);
//    }

//    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserNameUpdatedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        var r = _rpcResultProcessor.ToUser(domainEvent.AggregateEvent);

//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r).ConfigureAwait(false);
//    }

//    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserProfilePhotoChangedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        var r = _rpcResultProcessor.ToPhoto(domainEvent.AggregateEvent);

//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r).ConfigureAwait(false);
//    }

//    private async Task NotifyUpdateChannelAsync(long reqMsgId,
//        long channelId,
//        string? sourceId,
//        long memberUid = 0)
//    {
//        var updates = new TUpdateShort
//        {
//            Date = DateTime.UtcNow.ToTimestamp(),
//            Update = new TUpdateChannel { ChannelId = channelId }
//        };
//        // TODO:Should return TUpdates to sender,include channel data
//        if (reqMsgId != 0)
//        {
//            await SendRpcMessageToClientAsync(reqMsgId, updates, sourceId).ConfigureAwait(false);
//        }

//        if (memberUid != 0)
//        {
//            await PushUpdatesToPeerAsync(new Peer(PeerType.User, memberUid), updates).ConfigureAwait(false);
//        }
//        else
//        {
//            await PushUpdatesToPeerAsync(new Peer(PeerType.Channel, channelId), updates).ConfigureAwait(false);
//        }
//    }

//    private async Task NotifyUpdateChatAsync(long reqMsgId,
//        long chatId,
//        string sourceId)
//    {
//        var updates = new TUpdateShort
//        {
//            Date = DateTime.UtcNow.ToTimestamp(),
//            Update = new TUpdateChat { ChatId = chatId }
//        };

//        // TODO:Should return TUpdates to sender,include channel data
//        if (reqMsgId != 0)
//        {
//            await SendRpcMessageToClientAsync(reqMsgId, updates, sourceId).ConfigureAwait(false);
//        }

//        await PushUpdatesToPeerAsync(new Peer(PeerType.Chat, chatId), updates).ConfigureAwait(false);
//    }

//    #region Handlers

//    private async Task SendRpcMessageToClientAsync(
//        long reqMsgId,
//        IObject rpcData,
//        string? sourceId = null,
//        long selfUserId = 0,
//        int pts = 0,
//        PeerType toPeerType = PeerType.User
//    )
//    {
//        if (!string.IsNullOrEmpty(sourceId))
//        {
//            await SaveRpcResultAsync(reqMsgId, sourceId, selfUserId, rpcData).ConfigureAwait(false);
//        }
        
//        if (pts > 0 && selfUserId != 0 && toPeerType != PeerType.Channel)
//        {
//            await _ackCacheService.AddRpcPtsToCacheAsync(reqMsgId, pts, 0, new Peer(PeerType.User, selfUserId))
//                .ConfigureAwait(false);
//        }

//        await _objectMessageSender.SendRpcMessageToClientAsync(reqMsgId, rpcData).ConfigureAwait(false);
//    }

//    private Task SaveRpcResultAsync(long reqMsgId,
//        string sourceId,
//        long peerId,
//        IObject rpcResult)
//    {
//        var command = new CreateRpcResultCommand(RpcResultId.Create(sourceId),
//            reqMsgId,
//            peerId,
//            sourceId,
//            rpcResult.ToBytes());
//        return _commandBus.PublishAsync(command, default);
//    }

//    private Task AddRpcGlobalSeqNoForAuthKeyIdAsync(long reqMsgId,
//        long selfUserId,
//        long globalSeqNo)
//    {
//        return _ackCacheService.AddRpcPtsToCacheAsync(reqMsgId, 0, globalSeqNo, new Peer(PeerType.User, selfUserId));
//    }

//    private Task UpdateSelfGlobalSeqNoAfterSendChannelMessageAsync(long userId,
//        long globalSeqNo)
//    {
//        var updateGlobalSeqNoCommand = new UpdateGlobalSeqNoCommand(PtsId.Create(userId), userId, 0, globalSeqNo);
//        return _commandBus.PublishAsync(updateGlobalSeqNoCommand, default);
//    }

//    private async Task<long> SavePushUpdatesAsync(
//        Peer toPeer,
//        byte[] data,
//        int pts,
//        PtsType ptsType,
//        long excludeAuthKeyId,
//        long excludeUid,
//        long onlySendToThisAuthKeyId,
//        IMessage? newMessage = null)
//    {
//        if (pts == 0)
//        {
//            return 0;
//        }
//        var globalSeqNo = await _idGenerator.NextLongIdAsync(IdType.GlobalSeqNo).ConfigureAwait(false);
//        var dataBytes = newMessage == null ? data : newMessage.ToBytes();
//        var command = new CreatePushUpdatesCommand(PushUpdatesId.Create(
//                toPeer.PeerId,
//                excludeAuthKeyId,
//                excludeUid,
//                onlySendToThisAuthKeyId,
//                pts),
//            toPeer,
//            excludeAuthKeyId,
//            excludeUid,
//            onlySendToThisAuthKeyId,
//            dataBytes,
//            pts,
//            ptsType,
//            globalSeqNo);
//        await _commandBus.PublishAsync(command, default).ConfigureAwait(false);
//        return globalSeqNo;
//    }

//    private async Task PushUpdatesToChannelMemberAsync(Peer channelPeer,
//        IUpdates updates,
//        long excludeAuthKeyId = 0,
//        long excludeUid = 0,
//        long onlySendToThisAuthKeyId = 0,
//        int pts = 0,
//        PtsType ptsType = PtsType.Unknown,
//        bool skipSaveUpdates = false)
//    {
//        var globalSeqNo = 0L;
//        if (!skipSaveUpdates)
//        {
//            globalSeqNo = await SavePushUpdatesAsync(
//                channelPeer,
//                updates.ToBytes(),
//                pts, // channel only use globalSeqNo,pass 0 to pts
//                ptsType,
//                excludeAuthKeyId,
//                excludeUid,
//                onlySendToThisAuthKeyId).ConfigureAwait(false);
//        }

//        await _objectMessageSender.PushMessageToPeerAsync(channelPeer,
//            updates,
//            excludeAuthKeyId,
//            excludeUid,
//            onlySendToThisAuthKeyId,
//            pts,
//            ptsType,
//            globalSeqNo).ConfigureAwait(false);
//    }

//    private async Task PushUpdatesToPeerAsync(Peer toPeer,
//        IUpdates updates,
//        long excludeAuthKeyId = 0,
//        long excludeUid = 0,
//        long onlySendToThisAuthKeyId = 0,
//        int pts = 0,
//        PtsType ptsType = PtsType.Unknown,
//        IMessage? newMessage = null)
//    {
//        long globalSeqNo = 0;
//        if (pts > 0)
//        {
//            var dataBytes = updates.ToBytes();
//            globalSeqNo = await SavePushUpdatesAsync(
//                toPeer,
//                dataBytes,
//                pts,
//                ptsType,
//                excludeAuthKeyId,
//                excludeUid,
//                onlySendToThisAuthKeyId,
//                newMessage).ConfigureAwait(false);
//        }

//        await _objectMessageSender.PushMessageToPeerAsync(toPeer,
//            updates,
//            excludeAuthKeyId,
//            excludeUid,
//            onlySendToThisAuthKeyId,
//            pts,
//            ptsType,
//            globalSeqNo).ConfigureAwait(false);
//    }

//    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        _logger.LogInformation(
//            "User created,userId={UserId},phoneNumber={PhoneNumber},firstName={FirstName},lastName={LastName}",
//            domainEvent.AggregateEvent.UserId,
//            domainEvent.AggregateEvent.PhoneNumber,
//            domainEvent.AggregateEvent.FirstName,
//            domainEvent.AggregateEvent.LastName
//        );

//        await _eventBus.PublishAsync(new UserSignUpSuccessIntegrationEvent(domainEvent.AggregateEvent.Request.AuthKeyId,
//            domainEvent.AggregateEvent.Request.PermAuthKeyId,
//            domainEvent.AggregateEvent.UserId)).ConfigureAwait(false);

//        var r = _rpcResultProcessor.CreateAuthorizationFromUser(domainEvent.AggregateEvent);
//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.Request.ReqMsgId,
//            r,
//            domainEvent.Metadata.SourceId.Value,
//            domainEvent.AggregateEvent.UserId).ConfigureAwait(false);
//    }

//    public async Task HandleAsync(
//        IDomainEvent<ReadHistorySaga, ReadHistorySagaId, ReadHistoryCompletedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        var affectedMessages = new TAffectedMessages { Pts = domainEvent.AggregateEvent.ReaderPts, PtsCount = 1 };

//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.Request.ReqMsgId,
//                affectedMessages,
//                domainEvent.AggregateEvent.SourceCommandId,
//                domainEvent.AggregateEvent.ReaderUid)
//            .ConfigureAwait(false);
//        var peer = domainEvent.AggregateEvent.ReaderToPeer;
//        var updateReadHistoryInbox = new TUpdateReadHistoryInbox
//        {
//            Peer = peer.ToPeer(),
//            MaxId = domainEvent.AggregateEvent.ReaderMessageId,
//            Pts = domainEvent.AggregateEvent.ReaderPts,
//            PtsCount = 1
//        };
//        var selfOtherDevicesUpdates = new TUpdates
//        {
//            Updates = new TVector<IUpdate>(updateReadHistoryInbox),
//            Users = new TVector<IUser>(),
//            Chats = new TVector<IChat>(),
//            Date = DateTime.UtcNow.ToTimestamp()
//        };
//        await PushUpdatesToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.ReaderUid),
//            selfOtherDevicesUpdates,
//            domainEvent.AggregateEvent.Request.AuthKeyId,
//            pts: domainEvent.AggregateEvent.ReaderPts,
//            ptsType: PtsType.OtherUpdates
//        ).ConfigureAwait(false);

//        if (!domainEvent.AggregateEvent.IsOut && !domainEvent.AggregateEvent.OutboxAlreadyRead &&
//            !_peerHelper.IsBotUser(domainEvent.AggregateEvent.SenderPeerId))
//        {
//            var readHistoryUpdates =
//                _rpcResultProcessor.ToReadHistoryUpdates(domainEvent.AggregateEvent);
//            var toPeer = new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId);
//            await PushUpdatesToPeerAsync(
//                toPeer,
//                readHistoryUpdates,
//                domainEvent.AggregateEvent.Request.AuthKeyId,
//                pts: domainEvent.AggregateEvent.ReaderPts,
//                ptsType: PtsType.OtherUpdates
//            ).ConfigureAwait(false);
//        }
//    }

//    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserProfileUpdatedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        // TODO:Create user from domain events instead of query from read model
//        var user = await _queryProcessor.ProcessAsync(new GetUserByIdQuery(domainEvent.AggregateEvent.UserId),
//            cancellationToken).ConfigureAwait(false);
//        var r = _rpcResultProcessor.ToUser(user!, user!.UserId);

//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//            r,
//            domainEvent.Metadata.SourceId.Value,
//            domainEvent.AggregateEvent.UserId).ConfigureAwait(false);
//    }

//    public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatCreatedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        _createdChatCacheHelper.Add(domainEvent.AggregateEvent);
//        return Task.CompletedTask;
//    }

//    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        _createdChatCacheHelper.Add(domainEvent.AggregateEvent);
//        return Task.CompletedTask;
//    }

//    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ExportChatInviteEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//            _rpcResultProcessor.ToExportedChatInvite(domainEvent.AggregateEvent)
//        ).ConfigureAwait(false);
//    }

//    public async Task HandleAsync(
//        IDomainEvent<ReadChannelHistorySaga, ReadChannelHistorySagaId, ReadChannelHistoryCompletedEvent>
//            domainEvent,
//        CancellationToken cancellationToken)
//    {
//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//                new TBoolTrue(),
//                domainEvent.Metadata.SourceId.Value,
//                domainEvent.AggregateEvent.SenderPeerId)
//            .ConfigureAwait(false);

//        if (domainEvent.AggregateEvent.NeedNotifySender)
//        {
//            var data = new TUpdateReadChannelOutbox
//            {
//                ChannelId = domainEvent.AggregateEvent.ChannelId,
//                MaxId = domainEvent.AggregateEvent.MessageId
//            };
//            var updates = new TUpdateShort { Update = data, Date = DateTime.UtcNow.ToTimestamp() };
//            await PushUpdatesToPeerAsync(
//                    new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId),
//                    updates)
//                .ConfigureAwait(false);
//        }
//    }

//    #endregion 
//    public Task HandleAsync(IDomainEvent<AppCodeAggregate, AppCodeId, SignUpRequiredEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//            _rpcResultProcessor.CreateSignUpAuthorization());
//    }
//}
