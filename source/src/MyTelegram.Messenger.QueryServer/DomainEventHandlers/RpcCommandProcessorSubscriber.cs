//using MyTelegram.Domain.Aggregates.Contact;
//using MyTelegram.Domain.Aggregates.PhoneCall;
//using MyTelegram.Domain.Aggregates.UserPassword;
//using MyTelegram.Domain.Events.Contact;
//using MyTelegram.Domain.Events.EncryptedChat;
//using MyTelegram.Domain.Events.PhoneCall;
//using MyTelegram.Domain.Events.UserPassword;
//using MyTelegram.MessengerServer.DomainEventHandlers.Converters;
//using MyTelegram.Schema.Auth;
//using MyTelegram.Schema.Contacts;
//using MyTelegram.Schema.Messages;

//namespace MyTelegram.MessengerServer.DomainEventHandlers;

//public class RpcCommandProcessorSubscriber :
//        //ISubscribeSynchronousTo<UserAggregate, UserId, UserCreatedEvent>,
//        ISubscribeSynchronousTo<SignInSaga, SignInSagaId, SignInSuccessEvent>,
//        ISubscribeSynchronousTo<AppCodeAggregate,AppCodeId, SignUpRequiredEvent>,

////ISubscribeSynchronousTo<MessageSaga, MessageSagaId, SendOutboxMessageSuccessEvent>,
////        ISubscribeSynchronousTo<MessageSaga, MessageSagaId, ReceiveInboxMessageSuccessEvent>,
//        //ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, ReadHistoryCompletedEvent>,
//        //ISubscribeSynchronousTo<UserAggregate, UserId, UserProfileUpdatedEvent>,
//        //ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatCreatedEvent>,
//        //ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelCreatedEvent>,
//        //ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ExportChatInviteEvent>,
//        //ISubscribeSynchronousTo<UserNameAggregate, UserNameId, UserNameCheckCompletedEvent>,
//        //ISubscribeSynchronousTo<IncrementViewsSaga, IncrementViewsSagaId, IncrementViewsCompletedEvent>,
//        //ISubscribeSynchronousTo<ReadChannelHistorySaga, ReadChannelHistorySagaId, ReadChannelHistoryCompletedEvent>,
//        //ISubscribeSynchronousTo<ChannelAggregate, ChannelId, StartInviteToChannelEvent>,
//        //ISubscribeSynchronousTo<ChannelAggregate, ChannelId, SetDiscussionGroupEvent>,
//        //ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, OutboxEditCompletedEvent>,
//        //ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, InboxEditCompletedEvent>,
//        //ISubscribeSynchronousTo<MessageBoxAggregate, MessageBoxId, OutboxPinnedUpdatedEvent>,
//        //ISubscribeSynchronousTo<MessageBoxAggregate, MessageBoxId, InboxPinnedUpdatedEvent>,
//        ISubscribeSynchronousTo<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedMessageCompletedEvent>,
//        ISubscribeSynchronousTo<DeleteMessageSaga, DeleteMessageSagaId, DeleteMessagesCompletedEvent>,
//        //ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatTitleEditedEvent>,
//        //ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelTitleEditedEvent>,
//        //ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelAboutEditedEvent>,
//        //ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelDefaultBannedRightsEditedEvent>,
//        //ISubscribeSynchronousTo<ChannelAggregate, ChannelId, SlowModeChangedEvent>,
//        //ISubscribeSynchronousTo<ChannelAggregate, ChannelId, PreHistoryHiddenChangedEvent>,
//        //ISubscribeSynchronousTo<DialogAggregate, DialogId, ChannelHistoryClearedEvent>,
//        ISubscribeSynchronousTo<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedEvent>,
//        //ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatDefaultBannedRightsEditedEvent>,
//        //ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelAdminRightsEditedEvent>,
//        //ISubscribeSynchronousTo<DialogAggregate, DialogId, DialogPinChangedEvent>,
//        //ISubscribeSynchronousTo<PhoneCallSaga, PhoneCallSagaId, RequestCallCompletedEvent>,
//        //ISubscribeSynchronousTo<PhoneCallConfigAggregate, PhoneCallConfigId, PhoneCallReceivedEvent>,
//        //ISubscribeSynchronousTo<PhoneCallSaga, PhoneCallSagaId, AcceptCallCompletedEvent>,
//        //ISubscribeSynchronousTo<PhoneCallSaga, PhoneCallSagaId, ConfirmCallCompletedEvent>,
//        //ISubscribeSynchronousTo<PhoneCallSaga, PhoneCallSagaId, DiscardCallCompletedEvent>,
//        //ISubscribeSynchronousTo<UserPasswordAggregate, UserPasswordId, PasswordEmailConfirmedEvent>,
//        //ISubscribeSynchronousTo<UserPasswordAggregate, UserPasswordId, NewCloudPasswordUpdatedEvent>,
//        //ISubscribeSynchronousTo<UserPasswordAggregate, UserPasswordId, CloudPasswordRemovedEvent>,
//        //ISubscribeSynchronousTo<UserPasswordAggregate, UserPasswordId, RecoveryPasswordRequestedEvent>,
//        //ISubscribeSynchronousTo<UserAggregate, UserId, UserCloudPasswordRemovedEvent>,
//        //ISubscribeSynchronousTo<UserPasswordAggregate, UserPasswordId, VerifyRecoveryPasswordCodeFailedEvent>,
//        //ISubscribeSynchronousTo<UserPasswordAggregate, UserPasswordId, VerifyEmailConfirmCodeFailedEvent>,
//        //ISubscribeSynchronousTo<UserPasswordAggregate, UserPasswordId, RecoveryEmailChangedEvent>,
//        //ISubscribeSynchronousTo<EncryptedChatAggregate, EncryptedChatId, EncryptionRequestedEvent>,
//        //ISubscribeSynchronousTo<EncryptedChatAggregate, EncryptedChatId, EncryptionAcceptedEvent>,
//        //ISubscribeSynchronousTo<SendEncryptedMessageSaga, SendEncryptedMessageSagaId,
//        //    SendEncryptedInboxSuccessEvent>,
//        //ISubscribeSynchronousTo<EncryptedChatAggregate, EncryptedChatId, SetEncryptedTypingCompletedEvent>,
//        //ISubscribeSynchronousTo<EncryptedChatAggregate, EncryptedChatId, EncryptedOutboxHistoryHasReadEvent>,
//        //ISubscribeSynchronousTo<EncryptedChatAggregate, EncryptedChatId, EncryptedChatDiscardedEvent>,
//        //ISubscribeSynchronousTo<ContactAggregate, ContactId, ContactAddedEvent>,
//        //ISubscribeSynchronousTo<ContactAggregate, ContactId, ContactDeletedEvent>,
//        //ISubscribeSynchronousTo<ImportContactsSaga, ImportContactsSagaId, ImportContactsCompletedEvent>,
//        //ISubscribeSynchronousTo<UserAggregate, UserId, UserNameUpdatedEvent>,
//        //ISubscribeSynchronousTo<UserAggregate, UserId, UserProfilePhotoChangedEvent>,
//        //ISubscribeSynchronousTo<QrCodeAggregate, QrCodeId, QrCodeLoginTokenExportedEvent>,
//        //ISubscribeSynchronousTo<QrCodeAggregate, QrCodeId, LoginTokenAcceptedEvent>//,
//        //ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatAboutEditedEvent>//,
//        //ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelUserNameChangedEvent>,
//        //ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent>,
//        //ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent>,
//        //ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent> //,
//                                                                                                 //ISubscribeSynchronousTo<UserPasswordAggregate, UserPasswordId, RecoveryPasswordSuccessEvent>

////
////ISubscribeSynchronousTo<MessageBoxAggregate, MessageBoxId, StartUpdatePinnedMessageEvent>
////ISubscribeSynchronousTo<InviteToChannelSaga,InviteToChannelSagaId,InviteToChannelCompletedEvent>
//{
//    private readonly IAckCacheService _ackCacheService;
//    private readonly ICommandBus _commandBus;
//    private readonly IChatEventCacheHelper _chatEventCacheHelper;
//    private readonly IEventBus _eventBus;
//    private readonly IIdGenerator _idGenerator;
//    private readonly ILogger<RpcCommandProcessorSubscriber> _logger;
//    private readonly ILoginTokenCacheAppService _loginTokenCacheAppService;
//    private readonly IObjectMapper _objectMapper;
//    private readonly IObjectMessageSender _objectMessageSender;
//    private readonly IPeerHelper _peerHelper;
//    private readonly IQueryProcessor _queryProcessor;
//    private readonly IResponseCacheAppService _responseCacheAppService;
//    private readonly IRpcResultProcessor _rpcResultProcessor;
//    private readonly ITlUpdatesConverter _updatesConverter;

//    public RpcCommandProcessorSubscriber(ILogger<RpcCommandProcessorSubscriber> logger,
//        //ISessionAppService sessionAppService,
//        IRpcResultProcessor rpcResultProcessor,
//        //IAuthKeyAppService authKeyAppService,
//        //IRequestCacheAppService requestCacheAppService,
//        //IMessageSender messageSender,
//        IQueryProcessor queryProcessor,
//        IPeerHelper peerHelper,
//        IChatEventCacheHelper chatEventCacheHelper,
//        IObjectMapper objectMapper,
//        IResponseCacheAppService responseCacheAppService,
//        ILoginTokenCacheAppService loginTokenCacheAppService,
//        IObjectMessageSender objectMessageSender,
//        IEventBus eventBus,
//        ICommandBus commandBus,
//        IIdGenerator idGenerator,
//        IAckCacheService ackCacheService,
//        ITlUpdatesConverter updatesConverter //,
//                                         //IRpcResultCacheAppService rpcResultCacheAppService,
//                                         //IPushSeqNoCacheAppService pushSeqNoCacheAppService
//    )
//    {
//        _logger = logger;
//        //_sessionAppService = sessionAppService;
//        _rpcResultProcessor = rpcResultProcessor;
//        //_authKeyAppService = authKeyAppService;
//        //_requestCacheAppService = requestCacheAppService;
//        //_messageSender = messageSender;
//        _queryProcessor = queryProcessor;
//        _peerHelper = peerHelper;
//        _chatEventCacheHelper = chatEventCacheHelper;
//        _objectMapper = objectMapper;
//        _responseCacheAppService = responseCacheAppService;
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
//     ;
//    }

//    //public async Task HandleAsync(
//    //    IDomainEvent<ChannelAggregate, ChannelId, ChannelDefaultBannedRightsEditedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //            domainEvent.AggregateEvent.ChannelId,
//    //            domainEvent.Metadata.SourceId.Value)
//    // ;
//    //    //var updates = new TUpdateShort
//    //    //{
//    //    //    Date = DateTime.UtcNow.ToTimestamp(),
//    //    //    Update = new TUpdateChannel
//    //    //    {
//    //    //        ChannelId = domainEvent.AggregateEvent.ChannelId
//    //    //    }
//    //    //};
//    //    //// todo:这里应该返回TUpdates给发送者,只包含频道信息即可
//    //    //await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, () => updates);
//    //    //await SendMessageToPeerAsync(new Peer(PeerType.Channel, domainEvent.AggregateEvent.ChannelId), updates);
//    //}

//    //public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelTitleEditedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    await NotifyUpdateChannelAsync(0, domainEvent.AggregateEvent.ChannelId, domainEvent.Metadata.SourceId.Value)
//    // ;
//    //}

//    //public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelUserNameChangedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, new TBoolTrue());
//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<ChannelAggregate, ChannelId, PreHistoryHiddenChangedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //            domainEvent.AggregateEvent.ChannelId,
//    //            domainEvent.Metadata.SourceId.Value)
//    // ;
//    //}

//    //public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, SetDiscussionGroupEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //        new TBoolTrue(),
//    //        domainEvent.Metadata.SourceId.Value);
//    //}

//    //public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, SlowModeChangedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //            domainEvent.AggregateEvent.ChannelId,
//    //            domainEvent.Metadata.SourceId.Value)
//    // ;
//    //}

//    ////public Task HandleAsync(IDomainEvent<InviteToChannelSaga, InviteToChannelSagaId, InviteToChannelCompletedEvent> domainEvent,
//    ////    CancellationToken cancellationToken)
//    ////{
//    ////    throw new NotImplementedException();
//    ////}
//    //public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, StartInviteToChannelEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    _chatEventCacheHelper.Add(domainEvent.AggregateEvent);
//    //    return Task.CompletedTask;
//    //}

//    //public Task HandleAsync(
//    //    IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    return NotifyUpdateChannelAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //        domainEvent.AggregateEvent.ChannelId,
//    //        null,
//    //        domainEvent.AggregateEvent.MemberUid);
//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    // 这里直接从ReadModel获取频道信息
//    //    var channelReadModel = await _queryProcessor
//    //        .ProcessAsync(new GetChannelByIdQuery(domainEvent.AggregateEvent.ChannelId), default)
//    // ;
//    //    var updates = new TUpdates
//    //    {
//    //        Chats = new TVector<IChat>(_rpcResultProcessor.ToChannel(channelReadModel,
//    //            null,
//    //            domainEvent.AggregateEvent.MemberUid)),
//    //        Date = domainEvent.AggregateEvent.Date,
//    //        Seq = 0,
//    //        Users = new TVector<IUser>(),
//    //        Updates = new TVector<IUpdate>()
//    //    };

//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, updates);
//    //    await NotifyUpdateChannelAsync(0, domainEvent.AggregateEvent.ChannelId, null);
//    //}

//    //public Task HandleAsync(
//    //    IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    return NotifyUpdateChannelAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //        domainEvent.AggregateEvent.ChannelId,
//    //        null,
//    //        domainEvent.AggregateEvent.MemberUid);
//    //}

//    //public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatAboutEditedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, new TBoolTrue());
//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<ChatAggregate, ChatId, ChatDefaultBannedRightsEditedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    await NotifyUpdateChatAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //        domainEvent.AggregateEvent.ChatId,
//    //        domainEvent.Metadata.SourceId.Value,
//    //        domainEvent.AggregateEvent.DefaultBannedRights);
//    //}

//    //public async Task HandleAsync(IDomainEvent<DeleteMessageSaga, DeleteMessageSagaId, ClearHistoryCompletedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var r = new TAffectedHistory
//    //    {
//    //        Pts = domainEvent.AggregateEvent.SelfDeletedBoxItem.Pts,
//    //        PtsCount = domainEvent.AggregateEvent.SelfDeletedBoxItem.PtsCount,
//    //        Offset = domainEvent.AggregateEvent.NextMaxId,
//    //    };
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, () => r);
//    //}

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
//            //if (domainEvent.AggregateEvent.ToPeerType != PeerType.Channel)
//            //{
//            //    //await AddRpcPtsForAuthKeyIdAsync(domainEvent.AggregateEvent.ReqMsgId,domainEvent.AggregateEvent)
//            //}
//            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//                r,
//                domainEvent.Metadata.SourceId.Value
//            );
//            // Console.WriteLine($"Clear history completed for {domainEvent.AggregateEvent.DeletedBoxItem.OwnerPeerId} reqMsgId:{domainEvent.AggregateEvent.ReqMsgId}");
//        }

//        var updates = _rpcResultProcessor.ToDeleteMessagesUpdates(domainEvent.AggregateEvent.ToPeerType,
//            domainEvent.AggregateEvent.DeletedBoxItem,
//            DateTime.UtcNow.ToTimestamp());
//        await PushUpdatesToPeerAsync(
//            new Peer(PeerType.User, domainEvent.AggregateEvent.DeletedBoxItem.OwnerPeerId),
//            updates,
//            pts: domainEvent.AggregateEvent.DeletedBoxItem.Pts);

//        //var r = new TAffectedHistory
//        //{
//        //    Pts = domainEvent.AggregateEvent.Pts,
//        //    PtsCount = domainEvent.AggregateEvent.PtsCount,
//        //    Offset = 0
//        //};
//        //if (domainEvent.AggregateEvent.ReqMsgId != 0)
//        //{
//        //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, () => r);
//        //}
//        ////else
//        ////{
//        ////    await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId), r);
//        ////}
//    }

//    public async Task HandleAsync(IDomainEvent<ContactAggregate, ContactId, ContactAddedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        var r = new TUpdates
//        {
//            Chats = new TVector<IChat>(),
//            Date = DateTime.UtcNow.ToTimestamp(),
//            Seq = 0,
//            Updates = new TVector<IUpdate>(new TUpdatePeerSettings
//            {
//                Peer = new TPeerUser { UserId = domainEvent.AggregateEvent.TargetUid },
//                Settings = new Schema.TPeerSettings { NeedContactsException = false }
//            }),
//            Users = new TVector<IUser>()
//        };
//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r);
//    }

//    public async Task HandleAsync(IDomainEvent<ContactAggregate, ContactId, ContactDeletedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        var r = new TUpdates
//        {
//            Chats = new TVector<IChat>(),
//            Date = DateTime.UtcNow.ToTimestamp(),
//            Seq = 0,
//            Updates = new TVector<IUpdate>(new TUpdatePeerSettings
//            {
//                Peer = new TPeerUser { UserId = domainEvent.AggregateEvent.TargetUid },
//                Settings = new Schema.TPeerSettings()
//            }),
//            Users = new TVector<IUser>()
//        };
//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r);
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
//                domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
//                0,
//                0
//            );
//            await AddRpcGlobalSeqNoForAuthKeyIdAsync(domainEvent.AggregateEvent.RequestInfo.ReqMsgId,
//                domainEvent.AggregateEvent.RequestInfo.UserId,
//                globalSeqNo);
//            await UpdateSelfGlobalSeqNoAfterSendChannelMessageAsync(domainEvent.AggregateEvent.RequestInfo.UserId,
//                globalSeqNo);
//        }

//        var r = new TAffectedMessages
//        {
//            Pts = domainEvent.AggregateEvent.SelfDeletedBoxItem.Pts,
//            PtsCount = domainEvent.AggregateEvent.SelfDeletedBoxItem.PtsCount
//        };

//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo.ReqMsgId,
//            r,
//            domainEvent.Metadata.SourceId.Value,
//            domainEvent.AggregateEvent.RequestInfo.UserId,
//            domainEvent.AggregateEvent.SelfDeletedBoxItem.Pts,
//            toPeerType: domainEvent.AggregateEvent.ToPeerType);

//        if (domainEvent.AggregateEvent.ToPeerType == PeerType.Channel)
//        {
//            channelPeer = new Peer(PeerType.Channel, domainEvent.AggregateEvent.SelfDeletedBoxItem.OwnerPeerId);
//            await PushUpdatesToChannelMemberAsync(channelPeer,
//                channelUpdates!,
//                domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
//                skipSaveUpdates: true);
//        }
//        else
//        {
//            foreach (var deletedBoxItem in domainEvent.AggregateEvent.DeletedBoxItems)
//            {
//                var excludeAuthKeyId = 0L;
//                if (deletedBoxItem.OwnerPeerId == domainEvent.AggregateEvent.SelfDeletedBoxItem.OwnerPeerId)
//                {
//                    excludeAuthKeyId = domainEvent.AggregateEvent.RequestInfo.AuthKeyId;
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
//                );
//            }
//        }
//    }

//    //public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, ChannelHistoryClearedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //            new TBoolTrue(),
//    //            domainEvent.Metadata.SourceId.Value)
//    // ;
//    //}

//    //public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, DialogPinChangedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //            new TBoolTrue(),
//    //            domainEvent.Metadata.SourceId.Value,
//    //            domainEvent.AggregateEvent.OwnerPeerId)
//    // ;
//    //}

//    //public Task HandleAsync(IDomainEvent<EditMessageSaga, EditMessageSagaId, InboxEditCompletedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var toPeer =
//    //        new Peer(domainEvent.AggregateEvent.ToPeerType == PeerType.Channel ? PeerType.Channel : PeerType.User,
//    //            domainEvent.AggregateEvent.OwnerPeerId);

//    //    return PushUpdatesToPeerAsync(
//    //        //new Peer(domainEvent.AggregateEvent.ToPeerType, domainEvent.AggregateEvent.OwnerPeerId),
//    //        toPeer,
//    //        _rpcResultProcessor.ToEditUpdates(domainEvent.AggregateEvent),
//    //        pts: domainEvent.AggregateEvent.Pts,
//    //        ptsType: PtsType.NewMessages);
//    //}

//    //private async Task ReplyRpcResultAndNotifyPeerAsync(
//    //    long reqMsgId,
//    //    Peer toPeer,
//    //    IUpdates selfUpdates,
//    //    IUpdates otherPeerUpdates,
//    //    int pts,
//    //    PtsType ptsType,
//    //    long selfUserId,
//    //    long excludeAuthKeyId, IMessage newMessage = null)
//    //{
//    //    var globalSeqNo = await SavePushUpdatesAsync(toPeer,
//    //        otherPeerUpdates.ToBytes(),
//    //        pts,
//    //        ptsType,
//    //        excludeAuthKeyId,
//    //        0,
//    //        0,
//    //        newMessage);
//    //    // reply rpc message
//    //    await SendRpcMessageToClientAsync(reqMsgId, selfUpdates);
//    //    // notify self other devices
//    //    await _objectMessageSender.PushMessageToPeerAsync(new Peer(PeerType.User, selfUserId),
//    //        selfUpdates,
//    //        excludeAuthKeyId,
//    //        pts: pts,
//    //        ptsType: ptsType,
//    //        globalSeqNo: globalSeqNo);
//    //    // notify other peer

//    //    await PushUpdatesToPeerAsync(toPeer,
//    //        otherPeerUpdates,
//    //        excludeAuthKeyId,
//    //        pts: pts,
//    //        ptsType: ptsType,
//    //        newMessage: newMessage);

//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<EditMessageSaga, EditMessageSagaId, OutboxEditCompletedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var r = _rpcResultProcessor.ToEditUpdates(domainEvent.AggregateEvent, domainEvent.AggregateEvent.SenderPeerId);
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //        r,
//    //        domainEvent.Metadata.SourceId.Value,
//    //        domainEvent.AggregateEvent.SenderPeerId,
//    //        domainEvent.AggregateEvent.Pts,
//    //        PtsType.NewMessages,
//    //        toPeerType: domainEvent.AggregateEvent.ToPeerType
//    //    );
//    //    await PushUpdatesToPeerAsync(
//    //        new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId),
//    //        r,
//    //        domainEvent.AggregateEvent.SelfAuthKeyId,
//    //        pts: domainEvent.AggregateEvent.Pts,
//    //        ptsType: PtsType.NewMessages);

//    //    if (domainEvent.AggregateEvent.ToPeerType == PeerType.Channel)
//    //    {
//    //        await PushUpdatesToPeerAsync(
//    //                new Peer(PeerType.Channel, domainEvent.AggregateEvent.ToPeerId),
//    //                _rpcResultProcessor.ToEditUpdates(domainEvent.AggregateEvent, 0),
//    //                pts: domainEvent.AggregateEvent.Pts,
//    //                ptsType: PtsType.NewMessages)
//    //     ; // selfUserId=0 for channel member,selfUserId=0 means out=false
//    //    }
//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<ImportContactsSaga, ImportContactsSagaId, ImportContactsCompletedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var importedContacts = domainEvent.AggregateEvent.PhoneContacts
//    //        .Where(p => p.UserId > 0)
//    //        .Select(p => new TImportedContact { ClientId = p.ClientId, UserId = p.UserId }).ToList();

//    //    var r = new TImportedContacts
//    //    {
//    //        Imported = new TVector<IImportedContact>(importedContacts),
//    //        PopularInvites = new TVector<IPopularContact>(),
//    //        RetryContacts = new TVector<long>(),
//    //        Users = new TVector<IUser>()
//    //    };
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r);
//    //}

//    //public async Task HandleAsync(IDomainEvent<QrCodeAggregate, QrCodeId, LoginTokenAcceptedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    // 这里最好不要从ReadModel读取设备信息
//    //    var deviceReadModel = await _queryProcessor
//    //        .ProcessAsync(new GetDeviceByAuthKeyIdQuery(domainEvent.AggregateEvent.QrCodeLoginRequestPermAuthKeyId),
//    //            default);

//    //    if (deviceReadModel == null)
//    //    {
//    //        _logger.LogWarning(
//    //            "Get device info failed.perm authKeyId={PermAuthKeyId:x2}",
//    //            domainEvent.AggregateEvent.QrCodeLoginRequestPermAuthKeyId);
//    //        return;
//    //    }

//    //    _loginTokenCacheAppService.AddLoginSuccessAuthKeyIdToCache(
//    //        domainEvent.AggregateEvent.QrCodeLoginRequestTempAuthKeyId,
//    //        domainEvent.AggregateEvent.UserId);
//    //    var authorization = _rpcResultProcessor.ToAuthorization(deviceReadModel);
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, authorization)
//    // ;

//    //    var updateShortForLoginWithTokenRequestOwner =
//    //        new TUpdateShort { Date = DateTime.UtcNow.ToTimestamp(), Update = new TUpdateLoginToken() };
//    //    // send updates to qr code client

//    //    await _objectMessageSender
//    //        .PushSessionMessageToAuthKeyIdAsync(domainEvent.AggregateEvent.QrCodeLoginRequestTempAuthKeyId,
//    //            updateShortForLoginWithTokenRequestOwner);
//    //    //await PushUpdatesToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.UserId),
//    //    //    updateShortForLoginWithTokenRequestOwner,
//    //    //    excludeUid:-1,
//    //    //    onlySendToThisAuthKeyId: domainEvent.AggregateEvent.QrCodeLoginRequestTempAuthKeyId);
//    //    _logger.LogDebug("Accept qr code login token,userId={UserId},qr code client authKeyId={AuthKeyId}",
//    //        domainEvent.AggregateEvent.UserId,
//    //        domainEvent.AggregateEvent.QrCodeLoginRequestTempAuthKeyId);
//    //    //await SendMessageToAuthKeyIdAsync(domainEvent.AggregateEvent.QrCodeLoginRequestTempAuthKeyId,
//    //    //    updateShortForLoginWithTokenRequestOwner);
//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<QrCodeAggregate, QrCodeId, QrCodeLoginTokenExportedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var r = new TLoginToken
//    //    {
//    //        Token = domainEvent.AggregateEvent.Token,
//    //        Expires = domainEvent.AggregateEvent.ExpireDate
//    //    };

//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r);
//    //}

//    public async Task HandleAsync(IDomainEvent<SignInSaga, SignInSagaId, SignInSuccessEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        //if (domainEvent.AggregateEvent.SignUpRequired)
//        //{
//        //    _logger.LogDebug("SignUpRequired userId={UserId} phoneUmber={PhoneNumber}",
//        //        domainEvent.AggregateEvent.UserId,
//        //        domainEvent.AggregateEvent.PhoneNumber);
//        //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//        //        _rpcResultProcessor.CreateSignUpAuthorization());
//        //    return;
//        //}

//        var tempAuthKeyId = domainEvent.AggregateEvent.TempAuthKeyId;
//        //await _sessionAppService.BindUserIdToSessionAsync(tempAuthKeyId, domainEvent.AggregateEvent.UserId);
//        //_sessionAppService.SetSessionPasswordState(domainEvent.AggregateEvent.TempAuthKeyId, domainEvent.AggregateEvent.HasPassword ? PasswordState.WaitingForVerify : PasswordState.None);

//        await _eventBus.PublishAsync(new UserSignInSuccessEvent(tempAuthKeyId,
//                domainEvent.AggregateEvent.PermAuthKeyId,
//                domainEvent.AggregateEvent.UserId,
//                domainEvent.AggregateEvent.HasPassword ? PasswordState.WaitingForVerify : PasswordState.None))
//     ;
//        _logger.LogDebug(
//            "########################### User sign in success:{UserId} with tempAuthKeyId:{TempAuthKeyId} permAuthKeyId:{PermAuthKeyId}",
//            domainEvent.AggregateEvent.UserId,
//            domainEvent.AggregateEvent.TempAuthKeyId,
//            domainEvent.AggregateEvent.PermAuthKeyId);

//        if (domainEvent.AggregateEvent.HasPassword)
//        {
//            //throw new BadRequestException("SESSION_PASSWORD_NEEDED");
//            //ThrowHelper.ThrowUserFriendlyException("SESSION_PASSWORD_NEEDED");
//            var rpcError = new TRpcError
//            {
//                ErrorCode = MyTelegramServerDomainConsts.BadRequestErrorCode,
//                ErrorMessage = "SESSION_PASSWORD_NEEDED"
//            };
//            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, rpcError);
//            return;
//        }

//        var r = _rpcResultProcessor.CreateAuthorization(domainEvent.AggregateEvent);

//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r);
//    }

//    //public Task HandleAsync(IDomainEvent<MessageBoxAggregate, MessageBoxId, OutboxPinnedUpdatedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var updatePinnedMessages = new TUpdatePinnedMessages
//    //    {
//    //        Messages = new TVector<int>(domainEvent.AggregateEvent.MessageId),
//    //        Pts = domainEvent.AggregateEvent.Pts + 1,
//    //        Peer = new Peer(domainEvent.AggregateEvent.ToPeerType, domainEvent.AggregateEvent.ToPeerId).ToPeer(),
//    //        Pinned = true,
//    //        PtsCount = 1,
//    //    };
//    //    var updates = new TUpdates
//    //    {
//    //        Chats = new TVector<IChat>(),
//    //        Date = domainEvent.AggregateEvent.Date,
//    //        Updates = new TVector<IUpdate>(updatePinnedMessages),
//    //        Seq = 0,
//    //        Users = new TVector<IUser>()
//    //    };

//    //    SendMessageToPeerAsync(new Peer(domainEvent.AggregateEvent.ToPeerType, domainEvent.AggregateEvent.OwnerPeerId),
//    //        updates);
//    //    return Task.CompletedTask;
//    //}

//    //public Task HandleAsync(IDomainEvent<MessageBoxAggregate, MessageBoxId, InboxPinnedUpdatedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var updatePinnedMessages = new TUpdatePinnedMessages
//    //    {
//    //        Messages = new TVector<int>(domainEvent.AggregateEvent.MessageId),
//    //        Pts = domainEvent.AggregateEvent.Pts + 1,
//    //        Peer = new Peer(domainEvent.AggregateEvent.ToPeerType, domainEvent.AggregateEvent.ToPeerId).ToPeer(),
//    //        Pinned = true,
//    //        PtsCount = 1,
//    //    };
//    //    var updates = new TUpdates
//    //    {
//    //        Chats = new TVector<IChat>(),
//    //        Date = domainEvent.AggregateEvent.Date,
//    //        Updates = new TVector<IUpdate>(updatePinnedMessages),
//    //        Seq = 0,
//    //        Users = new TVector<IUser>()
//    //    };

//    //    SendMessageToPeerAsync(new Peer(domainEvent.AggregateEvent.ToPeerType, domainEvent.AggregateEvent.OwnerPeerId),
//    //        updates);
//    //    return Task.CompletedTask;
//    //}

//    //public Task HandleAsync(IDomainEvent<MessageBoxAggregate, MessageBoxId, StartUpdatePinnedMessageEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    throw new NotImplementedException();
//    //}
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
//                PtsType.OtherUpdates,
//                toPeerType: domainEvent.AggregateEvent.ToPeer.PeerType
//            );
//            await PushUpdatesToPeerAsync(
//                new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId),
//                r,
//                pts: domainEvent.AggregateEvent.Pts);
//        }

//        //else
//        {
//            await PushUpdatesToPeerAsync(
//                domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel
//                    ? new Peer(PeerType.Channel, domainEvent.AggregateEvent.OwnerPeerId)
//                    : new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId),
//                r,
//                excludeUid: domainEvent.AggregateEvent.SenderPeerId,
//                pts: domainEvent.AggregateEvent.Pts);
//        }
//    }

//    //public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserNameUpdatedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var r = _rpcResultProcessor.ToUser(domainEvent.AggregateEvent);

//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r);
//    //}

//    //public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserProfilePhotoChangedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var r = _rpcResultProcessor.ToPhoto(domainEvent.AggregateEvent);

//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r);
//    //}

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
//        // todo:这里应该返回TUpdates给发送者,只包含频道信息即可
//        if (reqMsgId != 0)
//        {
//            await SendRpcMessageToClientAsync(reqMsgId, updates, sourceId);
//        }

//        if (memberUid != 0)
//        {
//            await PushUpdatesToPeerAsync(new Peer(PeerType.User, memberUid), updates);
//        } else
//        {
//            await PushUpdatesToPeerAsync(new Peer(PeerType.Channel, channelId), updates);
//        }
//    }

//    private async Task NotifyUpdateChatAsync(long reqMsgId,
//        long chatId,
//        string sourceId,
//        ChatBannedRights rights)
//    {
//        //var updates = new TUpdates()
//        //{
//        //    Date = DateTime.UtcNow.ToTimestamp(),
//        //    Updates = new TVector<IUpdate>(new TUpdateChat
//        //    {
//        //        ChatId = chatId
//        //    }),
//        //    Chats = new TVector<IChat>(),
//        //    Users = new TVector<IUser>(),
//        //    //Update = new TUpdateChat
//        //    //{
//        //    //    ChatId=chatId
//        //    //}
//        //};
//        var updates = new TUpdateShort
//        {
//            Date = DateTime.UtcNow.ToTimestamp(),
//            Update = new TUpdateChat { ChatId = chatId }
//            //Update = new TUpdateChatDefaultBannedRights
//            //{
//            //    DefaultBannedRights = _objectMapper.Map<ChatBannedRights, TChatBannedRights>(rights),
//            //    Peer = new TPeerChat { ChatId = chatId },
//            //    Version = 0
//            //}
//        };

//        // todo:这里应该返回TUpdates给发送者,只包含群信息即可
//        if (reqMsgId != 0)
//        {
//            await SendRpcMessageToClientAsync(reqMsgId, updates, sourceId);
//        }

//        await PushUpdatesToPeerAsync(new Peer(PeerType.Chat, chatId), updates);
//    }

//    #region Handlers

//    private async Task SendRpcMessageToClientAsync(
//        long reqMsgId,
//        IObject rpcData,
//        string? sourceId = null,
//        long selfUserId = 0,
//        int pts = 0,
//        PtsType ptsType = PtsType.Unknown,
//        //long selfUserId = 0,
//        long selfPermAuthKeyId = 0,
//        PeerType toPeerType = PeerType.User
//    )
//    {
//        if (!string.IsNullOrEmpty(sourceId))
//        {
//            await SaveRpcResultAsync(reqMsgId, sourceId, selfUserId, rpcData);
//        }

//        // todo:考虑推送不包含pts信息的Updates给客户端
//        if (pts > 0 && selfUserId != 0 && toPeerType != PeerType.Channel)
//        {
//            //var eventData = new CreatePushMessageEvent(new Peer(PeerType.User, selfUserId), rpcData.ToBytes(), pts, 0, ptsType);
//            //await _eventBus.PublishAsync(eventData);
//            await _ackCacheService.AddRpcPtsToCacheAsync(reqMsgId, pts, 0, new Peer(PeerType.User, selfUserId))
//         ;
//        }

//        await _objectMessageSender.SendRpcMessageToClientAsync(reqMsgId, rpcData);
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

//    private Task SaveEncryptedPushUpdatesAsync(SendEncryptedInboxSuccessEvent aggregateEvent)
//    {
//        //var createEncryptedPushMessage = new CreateEncryptedPushMessageEvent(
//        //    aggregateEvent.OtherPartUid,
//        //    aggregateEvent.Data,
//        //    aggregateEvent.Qts,
//        //    aggregateEvent.OtherPartPermAuthKeyId);
//        //await _eventBus.PublishAsync(createEncryptedPushMessage);

//        var command = new CreateEncryptedPushUpdatesCommand(
//            PushUpdatesId.CreateEncryptedPushId(aggregateEvent.OtherPartUid,
//                aggregateEvent.OtherPartPermAuthKeyId,
//                aggregateEvent.Qts),
//            aggregateEvent.OtherPartUid,
//            aggregateEvent.Data,
//            aggregateEvent.Qts,
//            aggregateEvent.OtherPartPermAuthKeyId
//        );

//        return _commandBus.PublishAsync(command, default);
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
//            return 0; //Task.CompletedTask;
//        }

//        ////var eventData = new CreatePushMessageEvent(toPeer, newMessage == null ? data : newMessage.ToBytes(), pts, onlyForThisAuthKeyId, ptsType);
//        ////await _eventBus.PublishAsync(eventData);
//        ////return 0;
//        var globalSeqNo = await _idGenerator.NextLongIdAsync(IdType.GlobalSeqNo);
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
//        await _commandBus.PublishAsync(command, default);
//        return globalSeqNo;
//    }

//    private async Task<long> PushUpdatesToChannelSingleMemberAsync(
//        Peer channelMemberPeer,
//        IUpdates updates,
//        long excludeAuthKeyId = 0,
//        long excludeUid = 0,
//        long onlySendToThisAuthKeyId = 0,
//        int pts = 0,
//        PtsType ptsType = PtsType.Unknown)
//    {
//        var globalSeqNo = await SavePushUpdatesAsync(
//            channelMemberPeer,
//            updates.ToBytes(),
//            pts,
//            ptsType,
//            excludeAuthKeyId,
//            excludeUid,
//            onlySendToThisAuthKeyId);
//        await _objectMessageSender.PushMessageToPeerAsync(channelMemberPeer,
//            updates,
//            excludeAuthKeyId,
//            excludeUid,
//            onlySendToThisAuthKeyId,
//            pts,
//            ptsType,
//            globalSeqNo);
//        return globalSeqNo;
//    }

//    private async Task<long> PushUpdatesToChannelMemberAsync(
//        Peer channelPeer,
//        //IEnumerable<Peer> messageReceivePeerList,
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
//                pts, //频道只使用GlobalSeqNo,pts直接传0
//                ptsType,
//                excludeAuthKeyId,
//                excludeUid,
//                onlySendToThisAuthKeyId);
//        }

//        await _objectMessageSender.PushMessageToPeerAsync(channelPeer,
//            updates,
//            excludeAuthKeyId,
//            excludeUid,
//            onlySendToThisAuthKeyId,
//            pts,
//            ptsType,
//            globalSeqNo);
//        return globalSeqNo;
//    }

//    public async Task<long> PushUpdatesToPeerAsync(
//        Peer toPeer,
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
//                //new TVector<IObject>(dataList).ToBytes(),
//                dataBytes,
//                pts,
//                ptsType,
//                excludeAuthKeyId,
//                excludeUid,
//                onlySendToThisAuthKeyId,
//                newMessage);
//        }

//        await _objectMessageSender.PushMessageToPeerAsync(toPeer,
//            updates,
//            excludeAuthKeyId,
//            excludeUid,
//            onlySendToThisAuthKeyId,
//            pts,
//            ptsType,
//            globalSeqNo);
//        return globalSeqNo;
//    }

//    private async Task SendMessageToPeerAsync(Peer toPeer,
//        IObject data,
//        long excludeAuthKeyId = 0,
//        long excludeUid = 0,
//        long onlySendToThisAuthKeyId = 0,
//        int pts = 0,
//        PtsType ptsType = PtsType.Unknown)
//    {
//        var globalSeqNo = await SavePushUpdatesAsync(
//            toPeer,
//            data.ToBytes(),
//            pts,
//            ptsType,
//            excludeAuthKeyId,
//            excludeUid,
//            onlySendToThisAuthKeyId);
//        await _objectMessageSender.PushMessageToPeerAsync(toPeer,
//            data,
//            excludeAuthKeyId,
//            excludeUid,
//            onlySendToThisAuthKeyId,
//            pts,
//            ptsType,
//            globalSeqNo);
//    }

//    private async Task ProcessSendMultiMediaResponseAsync(long selfUserId,
//        long selfPermAuthKeyId,
//        long reqMsgId,
//        IUpdates updates,
//        string sourceId,
//        int count,
//        int pts,
//        PtsType ptsType,
//        Peer toPeer)
//    {
//        if (count > 1)
//        {
//            var addedCount = _responseCacheAppService.AddToCache(reqMsgId, updates);
//            if (addedCount == count)
//            {
//                if (_responseCacheAppService.TryRemoveResponseList(reqMsgId, out var responseList))
//                {
//                    var updatesAllInOne = new TUpdates
//                    {
//                        Updates = new TVector<IUpdate>(),
//                        Users = new TVector<IUser>(),
//                        Chats = new TVector<IChat>(),
//                        Date = DateTime.UtcNow.ToTimestamp()
//                    };
//                    foreach (var allUpdate in responseList)
//                    {
//                        if (allUpdate is TUpdates updatesItem)
//                        {
//                            foreach (var update in updatesItem.Updates)
//                            {
//                                updatesAllInOne.Updates.Add(update);
//                            }
//                        }
//                    }

//                    await SendRpcMessageToClientAsync(reqMsgId,
//                        updatesAllInOne,
//                        sourceId,
//                        selfUserId,
//                        pts,
//                        ptsType,
//                        selfPermAuthKeyId,
//                        toPeer.PeerType);
//                }
//            }
//        } else
//        {
//            await SendRpcMessageToClientAsync(reqMsgId,
//                updates,
//                sourceId,
//                selfUserId,
//                pts,
//                ptsType,
//                selfPermAuthKeyId,
//                toPeer.PeerType);
//        }
//    }

//    //public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    _logger.LogInformation(
//    //        "User created,userId={UserId},phoneNumber={PhoneNumber},firstName={FirstName},lastName={LastName}",
//    //        domainEvent.AggregateEvent.UserId,
//    //        domainEvent.AggregateEvent.PhoneNumber,
//    //        domainEvent.AggregateEvent.FirstName,
//    //        domainEvent.AggregateEvent.LastName
//    //    );

//    //    await _eventBus.PublishAsync(new UserSignUpSuccessIntegrationEvent(domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
//    //        domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId,
//    //        domainEvent.AggregateEvent.UserId));

//    //    var r = _rpcResultProcessor.CreateAuthorizationFromUser(domainEvent.AggregateEvent);
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo.ReqMsgId,
//    //        r,
//    //        domainEvent.Metadata.SourceId.Value,
//    //        domainEvent.AggregateEvent.UserId);
//    //}

//    //// SendMessage Outbox
//    //public async Task HandleAsync(
//    //    IDomainEvent<MessageSaga, MessageSagaId, SendOutboxMessageSuccessEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    await Task.Run(async () => {
//    //        //var sw = Stopwatch.StartNew();
//    //        //TestConsoleLogger.WriteLine($"start send outbox result to client:{domainEvent.AggregateEvent.ReqMsgId}");
//    //        var ptsType = PtsType.OtherUpdates;
//    //        IMessage? newMessage = null;
//    //        if (domainEvent.AggregateEvent.SubType == MessageBoxSubType.Normal ||
//    //            domainEvent.AggregateEvent.SubType == MessageBoxSubType.ForwardMessage)
//    //        {
//    //            ptsType = PtsType.NewMessages;
//    //            //newMessage=_rpcResultProcessor.tomes
//    //        }

//    //        switch (domainEvent.AggregateEvent.SubType)
//    //        {
//    //            case MessageBoxSubType.Normal:
//    //            case MessageBoxSubType.ForwardMessage:
//    //            case MessageBoxSubType.AddChatUser:
//    //            case MessageBoxSubType.DeleteChatUser:
//    //            case MessageBoxSubType.ClearHistory:
//    //                {
//    //                    IUpdates? channelUpdates = null;
//    //                    var globalSeqNo = 0L;
//    //                    // Console.WriteLine($"Send:userId={domainEvent.AggregateEvent.SenderPeerId} pts={domainEvent.AggregateEvent.Pts}");
//    //                    if (!_peerHelper.IsBotUser(domainEvent.AggregateEvent.SenderPeerId))
//    //                    {
//    //                        if (domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel)
//    //                        {
//    //                            //这里提前保存频道的推送Updates,是为了可以在回复发送消息客户端之前,可以将globalSeqNo加入缓存中,用于同步频道消息的GlobalSeqNo
//    //                            channelUpdates =
//    //                                _rpcResultProcessor.ToChannelMessageUpdates(domainEvent.AggregateEvent);
//    //                            globalSeqNo = await SavePushUpdatesAsync(domainEvent.AggregateEvent.ToPeer,
//    //                                channelUpdates.ToBytes(),
//    //                                domainEvent.AggregateEvent.Pts,
//    //                                ptsType,
//    //                                domainEvent.AggregateEvent.SelfAuthKeyId,
//    //                                0,
//    //                                0);
//    //                            await AddRpcGlobalSeqNoForAuthKeyIdAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //                                domainEvent.AggregateEvent.SenderPeerId,
//    //                                globalSeqNo);
//    //                        }

//    //                        if (domainEvent.AggregateEvent.ReqMsgId == 0)
//    //                        {
//    //                            await PushUpdatesToPeerAsync(
//    //                                    new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId),
//    //                                    _rpcResultProcessor.ToSelfUpdates(domainEvent.AggregateEvent),
//    //                                    pts: domainEvent.AggregateEvent.Pts,
//    //                                    ptsType: ptsType)
//    //                         ;
//    //                        } else
//    //                        {
//    //                            var r = _rpcResultProcessor.ToSelfUpdates(domainEvent.AggregateEvent);
//    //                            await ProcessSendMultiMediaResponseAsync(
//    //                                domainEvent.AggregateEvent.SenderPeerId,
//    //                                domainEvent.AggregateEvent.SelfPermAuthKeyId,
//    //                                domainEvent.AggregateEvent.ReqMsgId,
//    //                                r,
//    //                                domainEvent.Metadata.SourceId.Value,
//    //                                //domainEvent.AggregateEvent.SenderPeerId,
//    //                                domainEvent.AggregateEvent.GroupItemCount,
//    //                                domainEvent.AggregateEvent.Pts,
//    //                                ptsType,
//    //                                domainEvent.AggregateEvent.ToPeer
//    //                            );
//    //                        }
//    //                    }

//    //                    // 当发送到频道消息时，使用共同的Outbox，不会单独给每一个成员写入Inbox，所以需要单独推送频道消息给成员
//    //                    // 私聊消息和普通群消息需要单独推送给当前用户其他设备
//    //                    if (domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel)
//    //                    {
//    //                        await PushUpdatesToChannelMemberAsync(domainEvent.AggregateEvent.ToPeer,
//    //                            channelUpdates!,
//    //                            domainEvent.AggregateEvent.SelfAuthKeyId,
//    //                            domainEvent.AggregateEvent.SenderPeerId,
//    //                            skipSaveUpdates: true);
//    //                        await UpdateSelfGlobalSeqNoAfterSendChannelMessageAsync(domainEvent.AggregateEvent.SenderPeerId,
//    //                            globalSeqNo);
//    //                    }

//    //                    //else
//    //                    // {
//    //                    if (ptsType == PtsType.NewMessages)
//    //                    {
//    //                        newMessage = _rpcResultProcessor.ToMessage(domainEvent.AggregateEvent,
//    //                            domainEvent.AggregateEvent.SenderPeerId);
//    //                    }

//    //                    await PushUpdatesToPeerAsync(
//    //                            new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId),
//    //                            _rpcResultProcessor.ToSelfOtherDeviceUpdates(domainEvent.AggregateEvent),
//    //                            domainEvent.AggregateEvent.SelfAuthKeyId,
//    //                            pts: domainEvent.AggregateEvent.Pts,
//    //                            ptsType: ptsType,
//    //                            newMessage: newMessage)
//    //                 ;
//    //                    // Console.WriteLine($"Push self other devices,userId={domainEvent.AggregateEvent.SenderPeerId} msgId={domainEvent.AggregateEvent.MessageId} pts={domainEvent.AggregateEvent.Pts} toPeer={domainEvent.AggregateEvent.ToPeer}");
//    //                    //}
//    //                }
//    //                break;
//    //            case MessageBoxSubType.CreateChat:
//    //                {
//    //                    if (_createdChatCacheHelper.TryGetValue(domainEvent.AggregateEvent.ToPeer.PeerId,
//    //                            out var eventData))
//    //                    {
//    //                        var r = _rpcResultProcessor.ToCreateChatUpdates(eventData, domainEvent.AggregateEvent);
//    //                        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //                            r,
//    //                            domainEvent.Metadata.SourceId.Value,
//    //                            domainEvent.AggregateEvent.SenderPeerId);
//    //                        // 给自己的其他设备推送消息
//    //                        await PushUpdatesToPeerAsync(
//    //                                new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId),
//    //                                r,
//    //                                domainEvent.AggregateEvent.SelfAuthKeyId,
//    //                                pts: domainEvent.AggregateEvent.Pts,
//    //                                ptsType: ptsType)
//    //                     ;
//    //                    }
//    //                }
//    //                break;
//    //            case MessageBoxSubType.CreateChannel:
//    //                {
//    //                    if (_createdChatCacheHelper.TryRemove(domainEvent.AggregateEvent.ToPeer.PeerId,
//    //                            out ChannelCreatedEvent? eventData))
//    //                    {
//    //                        var r = _rpcResultProcessor.ToCreateChannelUpdates(eventData, domainEvent.AggregateEvent);
//    //                        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //                            r,
//    //                            domainEvent.Metadata.SourceId.Value,
//    //                            domainEvent.AggregateEvent.SenderPeerId);
//    //                        // Notify self other devices
//    //                        await PushUpdatesToChannelSingleMemberAsync(
//    //                            new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId),
//    //                            r,
//    //                            domainEvent.AggregateEvent.SelfAuthKeyId,
//    //                            pts: domainEvent.AggregateEvent.Pts,
//    //                            ptsType: ptsType
//    //                        );

//    //                        //await PushUpdatesToChannelMemberAsync()
//    //                    } else
//    //                    {
//    //                        _logger.LogWarning("Can not find create channel cache info,channelId={ChannelId}",
//    //                            domainEvent.AggregateEvent.ToPeer.PeerId);
//    //                    }
//    //                }
//    //                break;

//    //            case MessageBoxSubType.InviteToChannel:
//    //                {
//    //                    if (_createdChatCacheHelper.TryRemove(domainEvent.AggregateEvent.ToPeer.PeerId,
//    //                            out StartInvitoToChannelEvent? startInviteToChannelEvent))
//    //                    {
//    //                        // todo:create channel info from domain events
//    //                        var channelReadModel = await _queryProcessor
//    //                            .ProcessAsync(new GetChannelByIdQuery(domainEvent.AggregateEvent.ToPeer.PeerId),
//    //                                cancellationToken);

//    //                        var r = _rpcResultProcessor.ToInviteToChannelUpdates(domainEvent.AggregateEvent,
//    //                            startInviteToChannelEvent,
//    //                            channelReadModel,
//    //                            true);

//    //                        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //                            r,
//    //                            domainEvent.Metadata.SourceId.Value,
//    //                            domainEvent.AggregateEvent.SenderPeerId);

//    //                        // Notify self other devices
//    //                        await PushUpdatesToChannelSingleMemberAsync(
//    //                            new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId),
//    //                            r,
//    //                            domainEvent.AggregateEvent.SelfAuthKeyId
//    //                        );

//    //                        // Notify channel members
//    //                        var updatesForChannelMember = _rpcResultProcessor.ToInviteToChannelUpdates(
//    //                            domainEvent.AggregateEvent,
//    //                            startInviteToChannelEvent,
//    //                            channelReadModel,
//    //                            false);
//    //                        await PushUpdatesToChannelMemberAsync(domainEvent.AggregateEvent.ToPeer,
//    //                            updatesForChannelMember,
//    //                            excludeUid: domainEvent.AggregateEvent.SenderPeerId,
//    //                            pts: domainEvent.AggregateEvent.Pts,
//    //                            ptsType: ptsType);
//    //                    }
//    //                }
//    //                break;
//    //            case MessageBoxSubType.UpdatePinnedMessage:
//    //                {
//    //                    var r0 = _rpcResultProcessor.ToUpdatePinnedMessageUpdates(domainEvent.AggregateEvent);
//    //                    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //                                r0,
//    //                                domainEvent.Metadata.SourceId.Value,
//    //                                domainEvent.AggregateEvent.SenderPeerId,
//    //                                domainEvent.AggregateEvent.Pts,
//    //                                ptsType,
//    //                                domainEvent.AggregateEvent.SelfPermAuthKeyId,
//    //                                domainEvent.AggregateEvent.ToPeer.PeerType
//    //                            )
//    //                            .ConfigureAwait(false)
//    //                        ;

//    //                    await PushUpdatesToPeerAsync(
//    //                        new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId),
//    //                        r0,
//    //                        domainEvent.AggregateEvent.SelfAuthKeyId,
//    //                        pts: domainEvent.AggregateEvent.Pts,
//    //                        ptsType: ptsType);

//    //                    if (domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel)
//    //                    {
//    //                        var r = _rpcResultProcessor.ToUpdatePinnedMessageServiceUpdates(domainEvent.AggregateEvent);
//    //                        await PushUpdatesToPeerAsync(
//    //                            domainEvent.AggregateEvent.ToPeer,
//    //                            r,
//    //                            domainEvent.AggregateEvent.SelfAuthKeyId,
//    //                            0,
//    //                            0,
//    //                            domainEvent.AggregateEvent.Pts,
//    //                            ptsType);
//    //                    }
//    //                }
//    //                break;

//    //            default:
//    //                throw new ArgumentOutOfRangeException();
//    //        }

//    //        //sw.Stop();
//    //        //TestConsole.WriteLine($"{sw.Elapsed} [5]Process domain events");
//    //    },
//    //        cancellationToken);
//    //}

//    //// SendMessage Inbox
//    //public async Task HandleAsync(
//    //    IDomainEvent<MessageSaga, MessageSagaId, ReceiveInboxMessageSuccessEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    await Task.Run(async () => {
//    //        //var sw = Stopwatch.StartNew();
//    //        if (!_peerHelper.IsBotUser(domainEvent.AggregateEvent.OwnerPeerId))
//    //        {
//    //            var ptsType = PtsType.OtherUpdates;
//    //            if (domainEvent.AggregateEvent.SubType == MessageBoxSubType.Normal ||
//    //                domainEvent.AggregateEvent.SubType == MessageBoxSubType.ForwardMessage)
//    //            {
//    //                ptsType = PtsType.NewMessages;
//    //            }

//    //            IMessage? newMessage = null;

//    //            switch (domainEvent.AggregateEvent.SubType)
//    //            {
//    //                case MessageBoxSubType.Normal:
//    //                case MessageBoxSubType.AddChatUser:
//    //                case MessageBoxSubType.DeleteChatUser:
//    //                case MessageBoxSubType.ClearHistory:
//    //                    {
//    //                        var targetPeer = domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel
//    //                                ? domainEvent.AggregateEvent.ToPeer
//    //                                : new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId)
//    //                            ;

//    //                        var r = _rpcResultProcessor.ToReceiveMessageBoxUpdates(domainEvent.AggregateEvent);
//    //                        if (r is not TUpdates)
//    //                        {
//    //                            newMessage = _rpcResultProcessor.ToMessage(domainEvent.AggregateEvent);
//    //                        }

//    //                        //TestConsoleLogger.WriteLine($"send push message to peer.{targetPeer}");
//    //                        await PushUpdatesToPeerAsync(
//    //                            targetPeer,
//    //                            r,
//    //                            excludeUid: domainEvent.AggregateEvent.SenderPeerId,
//    //                            pts: domainEvent.AggregateEvent.Pts,
//    //                            ptsType: ptsType,
//    //                            newMessage: newMessage);
//    //                        // Console.WriteLine($"Receive inbox msg:{domainEvent.AggregateEvent.OwnerPeerId} msgId={domainEvent.AggregateEvent.MessageId} pts={domainEvent.AggregateEvent.Pts} toPeer={domainEvent.AggregateEvent.ToPeer}");
//    //                    }
//    //                    break;
//    //                case MessageBoxSubType.ForwardMessage:
//    //                    {
//    //                        var targetPeer = domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel
//    //                                ? domainEvent.AggregateEvent.ToPeer
//    //                                : new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId)
//    //                            ;
//    //                        var r = _rpcResultProcessor.ToInboxForwardMessageUpdates(domainEvent.AggregateEvent);
//    //                        await PushUpdatesToPeerAsync(
//    //                                targetPeer,
//    //                                r,
//    //                                excludeUid: domainEvent.AggregateEvent.SenderPeerId,
//    //                                pts: domainEvent.AggregateEvent.Pts,
//    //                                ptsType: ptsType)
//    //                     ;
//    //                    }
//    //                    break;

//    //                case MessageBoxSubType.CreateChat:
//    //                    {
//    //                        if (_createdChatCacheHelper.TryGetValue(domainEvent.AggregateEvent.ToPeer.PeerId,
//    //                                out var chatCreatedEvent))
//    //                        {
//    //                            var r = _rpcResultProcessor.ToCreateChatUpdates(chatCreatedEvent,
//    //                                domainEvent.AggregateEvent);
//    //                            await PushUpdatesToPeerAsync(
//    //                                new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId),
//    //                                r,
//    //                                pts: domainEvent.AggregateEvent.Pts,
//    //                                ptsType: ptsType);
//    //                        }
//    //                    }
//    //                    break;

//    //                case MessageBoxSubType.CreateChannel:
//    //                    {
//    //                        var r = _rpcResultProcessor.ToUpdatePinnedMessageUpdates(domainEvent.AggregateEvent);
//    //                        await PushUpdatesToChannelMemberAsync(
//    //                                domainEvent.AggregateEvent.ToPeer,
//    //                                //new[] { new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId) },
//    //                                r,
//    //                                excludeUid: domainEvent.AggregateEvent.SenderPeerId,
//    //                                pts: domainEvent.AggregateEvent.Pts,
//    //                                ptsType: ptsType)
//    //                     ;
//    //                    }
//    //                    break;
//    //                //case MessageBoxSubType.DeleteChatUser:
//    //                //    {

//    //                //    }
//    //                //    break;

//    //                //case MessageBoxSubType.InviteToChannel:
//    //                //    break;
//    //                case MessageBoxSubType.UpdatePinnedMessage:
//    //                    {
//    //                        var r = _rpcResultProcessor.ToUpdatePinnedMessageUpdates(domainEvent.AggregateEvent);
//    //                        await PushUpdatesToPeerAsync(
//    //                                new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId),
//    //                                r,
//    //                                pts: domainEvent.AggregateEvent.Pts,
//    //                                ptsType: ptsType)
//    //                     ;
//    //                    }
//    //                    break;
//    //                default:
//    //                    throw new ArgumentOutOfRangeException();
//    //            }
//    //        }

//    //        //sw.Stop();
//    //        //TestConsole.WriteLine($"{sw.Elapsed} [6] ReceiveInboxMessageSuccessEvent");
//    //        //    //return Task.CompletedTask;
//    //    }, cancellationToken);
//    //}

//    //// ReadHistory
//    //public async Task HandleAsync(
//    //    IDomainEvent<ReadHistorySaga, ReadHistorySagaId, ReadHistoryCompletedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var affectedMessages = new TAffectedMessages { Pts = domainEvent.AggregateEvent.ReaderPts, PtsCount = 1 };

//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo.ReqMsgId,
//    //            affectedMessages,
//    //            domainEvent.AggregateEvent.SourceCommandId,
//    //            domainEvent.AggregateEvent.ReaderUid)
//    // ;
//    //    var peer = domainEvent.AggregateEvent.ReaderToPeer;
//    //    var updateReadHistoryInbox = new TUpdateReadHistoryInbox
//    //    {
//    //        Peer = peer.ToPeer(),
//    //        MaxId = domainEvent.AggregateEvent.ReaderMessageId,
//    //        Pts = domainEvent.AggregateEvent.ReaderPts,
//    //        PtsCount = 1
//    //    };
//    //    var selfOtherDevicesUpdates = new TUpdates
//    //    {
//    //        Updates = new TVector<IUpdate>(updateReadHistoryInbox),
//    //        Users = new TVector<IUser>(),
//    //        Chats = new TVector<IChat>(),
//    //        Date = DateTime.UtcNow.ToTimestamp()
//    //    };
//    //    await PushUpdatesToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.ReaderUid),
//    //        selfOtherDevicesUpdates,
//    //        domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
//    //        pts: domainEvent.AggregateEvent.ReaderPts,
//    //        ptsType: PtsType.OtherUpdates
//    //    );

//    //    if (!domainEvent.AggregateEvent.IsOut && !domainEvent.AggregateEvent.OutboxAlreadyRead &&
//    //        !_peerHelper.IsBotUser(domainEvent.AggregateEvent.SenderPeerId))
//    //    {
//    //        var readHistoryUpdates =
//    //            _rpcResultProcessor.ToReadHistoryUpdates(domainEvent.AggregateEvent);
//    //        var toPeer = new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId);
//    //        await PushUpdatesToPeerAsync(
//    //            toPeer,
//    //            readHistoryUpdates,
//    //            domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
//    //            pts: domainEvent.AggregateEvent.ReaderPts,
//    //            ptsType: PtsType.OtherUpdates
//    //        );
//    //    }
//    //}

//    //public async Task HandleAsync(IDomainEvent<PtsAggregate, PtsId, IncrementPtsEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    //if (!_notSyncToIdGeneratorPtsChangeReason.Contains(domainEvent.AggregateEvent.Reason))
//    //    {
//    //        var newPts = await _idGenerator.NextIdAsync(IdType.Pts, domainEvent.AggregateEvent.PeerId);
//    //        //Console.WriteLine($"increment pts,peerId:{domainEvent.AggregateEvent.PeerId},reason:{domainEvent.AggregateEvent.Reason},oldPts:{domainEvent.AggregateEvent.Pts},newPts:{newPts}");
//    //        if (newPts != domainEvent.AggregateEvent.Pts + 1)
//    //        {
//    //            _logger.LogWarning($"oldPts+1 is not equal to newPts,oldPts:{domainEvent.AggregateEvent.Pts},newPts:{newPts}");
//    //        }

//    //    }

//    //    //return Task.CompletedTask;
//    //}

//    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserProfileUpdatedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        // todo:这里直接通过事件信息创建用户对象，不从ReadModel读取
//        var user = await _queryProcessor.ProcessAsync(new GetUserByIdQuery(domainEvent.AggregateEvent.UserId),
//            cancellationToken);
//        var r = _rpcResultProcessor.ToUser(user!, user!.UserId);

//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//            r,
//            domainEvent.Metadata.SourceId.Value,
//            domainEvent.AggregateEvent.UserId);
//    }

//    //// create chat
//    //public async Task HandleAsync(IDomainEvent<CreateChatSaga, CreateChatSagaId, CreateChatCompletedForCreatorEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //        () => _rpcResultProcessor.ToCreateChatUpdates(domainEvent.AggregateEvent));
//    //    //var tChat = new TChat
//    //    //{
//    //    //    DefaultBannedRights = new TChatBannedRights(),
//    //    //    CallActive = true,
//    //    //    CallNotEmpty = true,
//    //    //    Creator = true,
//    //    //    Date=domainEvent.AggregateEvent.Date,
//    //    //    Id=domainEvent.AggregateEvent.ChatId,
//    //    //    AdminRights = new TChatAdminRights(),
//    //    //    Deactivated = false,
//    //    //    Kicked = false,
//    //    //    Title = domainEvent.AggregateEvent.Title,
//    //    //    ParticipantsCount = domainEvent.AggregateEvent.MemberUidList.Count,
//    //    //    Left = false,
//    //    //    Photo = new TChatPhotoEmpty(),
//    //    //};
//    //    //throw new NotImplementedException();
//    //    Console.WriteLine("create chat ok.start notify creator...");
//    //    //return Task.CompletedTask;
//    //}

//    public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatCreatedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        _chatEventCacheHelper.Add(domainEvent.AggregateEvent);
//        //_createdChatDict.TryAdd(domainEvent.AggregateEvent.ChatId, domainEvent.AggregateEvent);
//        return Task.CompletedTask;
//    }

//    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        _chatEventCacheHelper.Add(domainEvent.AggregateEvent);
//        return Task.CompletedTask;
//    }

//    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ExportChatInviteEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//            _rpcResultProcessor.ToExportedChatInvite(domainEvent.AggregateEvent)
//        );
//    }

//    //public async Task HandleAsync(IDomainEvent<UserNameAggregate, UserNameId, UserNameCheckCompletedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //        () => domainEvent.AggregateEvent.IsOk ? new TBoolTrue() : new TBoolFalse());
//    //}

//    //public async Task HandleAsync(IDomainEvent<IncrementViewsSaga, IncrementViewsSagaId, IncrementViewsCompletedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //        () => _rpcResultProcessor.ToMessageViews(domainEvent.AggregateEvent));
//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<ReadChannelHistorySaga, ReadChannelHistorySagaId, ReadChannelHistoryCompletedEvent>
//    //        domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    //TestConsole2.WriteLine($"Read channel history ok.reqMsgId:{domainEvent.AggregateEvent.ReqMsgId:x2}");

//    //    //todo:这里的selfUid不是senderUid
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //            new TBoolTrue(),
//    //            domainEvent.Metadata.SourceId.Value,
//    //            domainEvent.AggregateEvent.SenderPeerId)
//    // ;

//    //    if (domainEvent.AggregateEvent.NeedNotifySender)
//    //    {
//    //        var data = new TUpdateReadChannelOutbox
//    //        {
//    //            ChannelId = domainEvent.AggregateEvent.ChannelId,
//    //            MaxId = domainEvent.AggregateEvent.MessageId
//    //        };
//    //        var updates = new TUpdateShort { Update = data, Date = DateTime.UtcNow.ToTimestamp() };
//    //        await PushUpdatesToPeerAsync(
//    //                new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId),
//    //                updates)
//    //     ;
//    //    }

//    //    //await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //    //    () => new TUpdateReadChannelOutbox
//    //    //    {
//    //    //        ChannelId = domainEvent.AggregateEvent.
//    //    //    });
//    //}

//    #endregion

//    //#region Phone Call

//    //public async Task HandleAsync(
//    //    IDomainEvent<PhoneCallSaga, PhoneCallSagaId, RequestCallCompletedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var waiting = _rpcResultProcessor.ToPhoneCallWaiting(domainEvent.AggregateEvent);
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//    //        waiting);
//    //    if (!domainEvent.AggregateEvent.IsParticipantInCalling)
//    //    {
//    //        var requestedUpdates = _rpcResultProcessor.ToPhoneCallRequestedUpdates(domainEvent.AggregateEvent);
//    //        await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.ParticipantId),
//    //            requestedUpdates);
//    //        // todo:only send updates to method caller
//    //        var waitingUpdates = _rpcResultProcessor.ToPhoneCallWaitingUpdates(domainEvent.AggregateEvent);
//    //        await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.AdminId),
//    //            waitingUpdates);
//    //    } else
//    //    {
//    //        var busyUpdates = _rpcResultProcessor.ToPhoneCallBusyDiscardedUpdates(domainEvent.AggregateEvent);
//    //        await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.AdminId), busyUpdates)
//    //     ;
//    //    }
//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<PhoneCallConfigAggregate, PhoneCallConfigId, PhoneCallReceivedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, new TBoolTrue())
//    // ;
//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<PhoneCallSaga, PhoneCallSagaId, AcceptCallCompletedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var callWaiting = _rpcResultProcessor.ToPhoneCallWaiting(domainEvent.AggregateEvent);
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, callWaiting);

//    //    var callAcceptedUpdates = _rpcResultProcessor.ToPhoneCallAcceptedUpdates(domainEvent.AggregateEvent);
//    //    await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.AdminId),
//    //        callAcceptedUpdates,
//    //        onlySendToThisAuthKeyId: domainEvent.AggregateEvent.AdminAuthKeyId);
//    //    // discard participant other devices
//    //    var discardOtherDeviceUpdates = new TUpdateShort
//    //    {
//    //        Date = DateTime.UtcNow.ToTimestamp(),
//    //        Update = new TUpdatePhoneCall
//    //        {
//    //            PhoneCall = new TPhoneCallDiscarded
//    //            {
//    //                Id = domainEvent.AggregateEvent.Id,
//    //                Video = domainEvent.AggregateEvent.Video,
//    //                Duration = 0,
//    //                Reason = new TPhoneCallDiscardReasonMissed()
//    //            }
//    //        }
//    //    };
//    //    await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.ParticipantId),
//    //        discardOtherDeviceUpdates,
//    //        domainEvent.AggregateEvent.ParticipantAuthKeyId);
//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<PhoneCallSaga, PhoneCallSagaId, ConfirmCallCompletedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    //var phoneCallTempData = new PhoneCallTempData(PhoneCallTempDataId.Create(domainEvent.AggregateEvent.Id),
//    //    //    domainEvent.AggregateEvent.Id,
//    //    //    domainEvent.AggregateEvent.AdminId,
//    //    //    domainEvent.AggregateEvent.AdminAuthKeyId,
//    //    //    domainEvent.AggregateEvent.ParticipantId,
//    //    //    domainEvent.AggregateEvent.ParticipantAuthKeyId
//    //    //);
//    //    //_phoneCallTempDataInMemoryRepository.Insert(phoneCallTempData.Id, phoneCallTempData);

//    //    var confirmed =
//    //        _rpcResultProcessor.ToPhoneCallConfirmed(domainEvent.AggregateEvent, domainEvent.AggregateEvent.Ga);
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, confirmed);

//    //    var confirmedUpdates = _rpcResultProcessor.ToPhoneCallConfirmedUpdates(domainEvent.AggregateEvent);
//    //    await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.ParticipantId),
//    //        confirmedUpdates,
//    //        onlySendToThisAuthKeyId: domainEvent.AggregateEvent.ParticipantAuthKeyId);
//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<PhoneCallSaga, PhoneCallSagaId, DiscardCallCompletedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var r = _rpcResultProcessor.ToPhoneCallDiscardUpdates(domainEvent.AggregateEvent);
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r);
//    //    var authKeyId = domainEvent.AggregateEvent.SelfUserId == domainEvent.AggregateEvent.AdminId
//    //        ? domainEvent.AggregateEvent.ParticipantAuthKeyId
//    //        : domainEvent.AggregateEvent.AdminAuthKeyId;
//    //    await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.OtherPartUid),
//    //        r,
//    //        onlySendToThisAuthKeyId: authKeyId);
//    //    //throw new NotImplementedException();
//    //}

//    //#endregion

//    #region Password

//    //public async Task HandleAsync(
//    //    IDomainEvent<UserPasswordAggregate, UserPasswordId, PasswordEmailConfirmedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, new TBoolTrue())
//    // ;
//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<UserPasswordAggregate, UserPasswordId, NewCloudPasswordUpdatedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    //if (domainEvent.AggregateEvent.Email.IsNullOrEmpty())
//    //    if (string.IsNullOrEmpty(domainEvent.AggregateEvent.Email) || string.IsNullOrEmpty(domainEvent.AggregateEvent.EmailConfirmCode))
//    //    {
//    //        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, new TBoolTrue())
//    //     ;
//    //    } else
//    //    {
//    //        // tell client should verify email code.

//    //        var rpcError = new TRpcError
//    //        {
//    //            ErrorCode = 400,
//    //            ErrorMessage = $"EMAIL_UNCONFIRMED_{domainEvent.AggregateEvent.EmailConfirmCode.Length}"
//    //        };

//    //        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, rpcError)
//    //     ;

//    //        //var r=new TRpcResult
//    //        //{
//    //        //    ReqMsgId=domainEvent.AggregateEvent.ReqMsgId,

//    //        //}
//    //        //throw new BadRequestException(
//    //        //    $"EMAIL_UNCONFIRMED_{domainEvent.AggregateEvent.EmailConfirmCode.Length}");
//    //    }
//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<UserPasswordAggregate, UserPasswordId, CloudPasswordRemovedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, new TBoolTrue())
//    // ;
//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<UserPasswordAggregate, UserPasswordId, RecoveryPasswordRequestedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var r = new TPasswordRecovery { EmailPattern = domainEvent.AggregateEvent.Email.MaskEmail() };
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r);
//    //}

//    //public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserCloudPasswordRemovedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    if (domainEvent.AggregateEvent.IsRecoveryPassword)
//    //    {
//    //        var r = _rpcResultProcessor.ToAuthorization(domainEvent.AggregateEvent);
//    //        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r);
//    //    } else
//    //    {
//    //        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, new TBoolTrue())
//    //     ;
//    //    }

//    //    //_sessionAppService.SetSessionPasswordState(domainEvent.AggregateEvent.UserId,PasswordState.None);
//    //    await _eventBus
//    //        .PublishAsync(new SetSessionPasswordStateEvent(domainEvent.AggregateEvent.UserId, PasswordState.None))
//    // ;
//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<UserPasswordAggregate, UserPasswordId, VerifyRecoveryPasswordCodeFailedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var r = new TRpcError { ErrorCode = 400, ErrorMessage = "CODE_INVALID" };
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r);
//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<UserPasswordAggregate, UserPasswordId, VerifyEmailConfirmCodeFailedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var r = new TRpcError { ErrorCode = 400, ErrorMessage = "CODE_INVALID" };
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r);
//    //}

//    //public async Task HandleAsync(
//    //    IDomainEvent<UserPasswordAggregate, UserPasswordId, RecoveryEmailChangedEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    var r = new TRpcError
//    //    {
//    //        ErrorCode = 400,
//    //        ErrorMessage = $"EMAIL_UNCONFIRMED_{domainEvent.AggregateEvent.EmailConfirmCode.Length}"
//    //    };
//    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, r);
//    //}

//    #endregion

//    #region End to End Encryption Chat

//    public async Task HandleAsync(
//        IDomainEvent<EncryptedChatAggregate, EncryptedChatId, EncryptionRequestedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        var encryptedWaiting =
//            _objectMapper.Map<EncryptionRequestedEvent, TEncryptedChatWaiting>(domainEvent.AggregateEvent);
//        encryptedWaiting.Id = domainEvent.AggregateEvent.ChatId;
//        var encryptedRequested =
//            _objectMapper.Map<EncryptionRequestedEvent, TEncryptedChatRequested>(domainEvent.AggregateEvent);
//        encryptedRequested.Id = domainEvent.AggregateEvent.ChatId;
//        var updateToParticipant =
//            new TUpdateEncryption { Chat = encryptedRequested, Date = DateTime.UtcNow.ToTimestamp() };
//        var updatesToParticipant = new TUpdates
//        {
//            Updates = new TVector<IUpdate>(updateToParticipant),
//            Users = new TVector<IUser>(),
//            Chats = new TVector<IChat>(),
//            Seq = 0
//        };

//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, encryptedWaiting)
//     ;
//        // todo:only send to devices support encrypted chat
//        await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.ParticipantId),
//            updatesToParticipant);
//    }

//    public async Task HandleAsync(
//        IDomainEvent<EncryptedChatAggregate, EncryptedChatId, EncryptionAcceptedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        var encryptedChatForAdmin =
//            _objectMapper.Map<EncryptionAcceptedEvent, TEncryptedChat>(domainEvent.AggregateEvent);
//        encryptedChatForAdmin.GAOrB = domainEvent.AggregateEvent.Gb;
//        encryptedChatForAdmin.Id = domainEvent.AggregateEvent.ChatId;

//        var encryptedChatForParticipant =
//            _objectMapper.Map<EncryptionAcceptedEvent, TEncryptedChat>(domainEvent.AggregateEvent);
//        encryptedChatForParticipant.GAOrB = domainEvent.AggregateEvent.Ga;
//        encryptedChatForParticipant.Id = domainEvent.AggregateEvent.ChatId;

//        var updateEncryptedForAdmin =
//            new TUpdateEncryption { Chat = encryptedChatForAdmin, Date = domainEvent.AggregateEvent.Date };
//        var updatesForAdmin = new TUpdates
//        {
//            Updates = new TVector<IUpdate>(updateEncryptedForAdmin),
//            Users = new TVector<IUser>(),
//            Chats = new TVector<IChat>(),
//            Seq = 0
//        };

//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, encryptedChatForParticipant)
//     ;
//        await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.AdminId),
//            updatesForAdmin,
//            onlySendToThisAuthKeyId: domainEvent.AggregateEvent.AdminAuthKeyId);
//    }

//    public async Task HandleAsync(
//        IDomainEvent<SendEncryptedMessageSaga, SendEncryptedMessageSagaId, SendEncryptedInboxSuccessEvent>
//            domainEvent,
//        CancellationToken cancellationToken)
//    {
//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//            new TSentEncryptedMessage { Date = domainEvent.AggregateEvent.Date });
//        var r = _rpcResultProcessor.ToEncryptedMessageUpdates(domainEvent.AggregateEvent);

//        await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.OtherPartUid),
//            r,
//            onlySendToThisAuthKeyId: domainEvent.AggregateEvent.OtherPartAuthKeyId);
//        await SaveEncryptedPushUpdatesAsync(domainEvent.AggregateEvent);
//        //var createEncryptedPushMessage = new CreateEncryptedPushMessageEvent(
//        //    domainEvent.AggregateEvent.OtherPartUid,
//        //    domainEvent.AggregateEvent.Data,
//        //    domainEvent.AggregateEvent.Qts,
//        //    domainEvent.AggregateEvent.OtherPartPermAuthKeyId);
//        //await _eventBus.PublishAsync(createEncryptedPushMessage);
//    }

//    public async Task HandleAsync(
//        IDomainEvent<EncryptedChatAggregate, EncryptedChatId, SetEncryptedTypingCompletedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, new TBoolTrue())
//     ;
//        var updateToOtherParty = new TUpdateEncryptedChatTyping { ChatId = domainEvent.AggregateEvent.ChatId };
//        var updatesToOtherParty =
//            new TUpdateShort { Update = updateToOtherParty, Date = domainEvent.AggregateEvent.Date };
//        await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.ReceiverUid),
//            updatesToOtherParty,
//            onlySendToThisAuthKeyId: domainEvent.AggregateEvent.ReceiverAuthKeyId);
//    }

//    public async Task HandleAsync(
//        IDomainEvent<EncryptedChatAggregate, EncryptedChatId, EncryptedOutboxHistoryHasReadEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, new TBoolTrue());
//        var updateToOtherParty = new TUpdateEncryptedMessagesRead
//        {
//            ChatId = domainEvent.AggregateEvent.ChatId,
//            Date = DateTime.UtcNow.ToTimestamp(),
//            MaxDate = domainEvent.AggregateEvent.MaxDate
//        };
//        var updatesToOtherParty = new TUpdateShort { Update = updateToOtherParty, Date = updateToOtherParty.Date };
//        await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.ReceiverUid),
//            updatesToOtherParty,
//            onlySendToThisAuthKeyId: domainEvent.AggregateEvent.ReceiverAuthKeyId);
//    }

//    public async Task HandleAsync(
//        IDomainEvent<EncryptedChatAggregate, EncryptedChatId, EncryptedChatDiscardedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId, new TBoolTrue());
//        var updateToOtherParty = new TEncryptedChatDiscarded { Id = domainEvent.AggregateEvent.ChatId };
//        var date = domainEvent.AggregateEvent.Date;
//        var updatesToOtherParty = new TUpdateShort
//        {
//            Date = date,
//            Update = new TUpdateEncryption { Chat = updateToOtherParty, Date = date }
//        };
//        //todo:process discarded event
//        await SendMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.ReceiverUid),
//            updatesToOtherParty,
//            onlySendToThisAuthKeyId: domainEvent.AggregateEvent.ReceiverAuthKeyId);
//    }

//    #endregion

//    //public Task HandleAsync(IDomainEvent<UserPasswordAggregate, UserPasswordId, RecoveryPasswordSuccessEvent> domainEvent,
//    //    CancellationToken cancellationToken)
//    //{
//    //    throw new NotImplementedException();
//    //}
//    public Task HandleAsync(IDomainEvent<AppCodeAggregate, AppCodeId, SignUpRequiredEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.ReqMsgId,
//            _rpcResultProcessor.CreateSignUpAuthorization());
//    }
//}


