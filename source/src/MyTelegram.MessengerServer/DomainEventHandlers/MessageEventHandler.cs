namespace MyTelegram.MessengerServer.DomainEventHandlers;

public class MessageEventHandler :
    ISubscribeSynchronousTo<MessageSaga, MessageSagaId, SendOutboxMessageCompletedEvent>,
    ISubscribeSynchronousTo<MessageSaga, MessageSagaId, ReceiveInboxMessageCompletedEvent>,

    ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedEvent>,
    ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedEvent>

{
    private readonly IObjectMessageSender _objectMessageSender;
    private readonly ICommandBus _commandBus;
    private readonly IIdGenerator _idGenerator;
    private readonly IAckCacheService _ackCacheService;
    private readonly IResponseCacheAppService _responseCacheAppService;
    private readonly ITlUpdatesConverter _updatesConverter;
    private readonly ITlMessageConverter _messageConverter;
    private readonly ICreatedChatCacheHelper _createdChatCacheHelper;
    private readonly ILogger<MessageEventHandler> _logger;
    private readonly IQueryProcessor _queryProcessor;

    public MessageEventHandler(
        IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService,
        ITlUpdatesConverter updatesConverter,
        ITlMessageConverter messageConverter,
        ICreatedChatCacheHelper createdChatCacheHelper,
        ILogger<MessageEventHandler> logger,
        IQueryProcessor queryProcessor)
    {
        _objectMessageSender = objectMessageSender;
        _commandBus = commandBus;
        _idGenerator = idGenerator;
        _ackCacheService = ackCacheService;
        _responseCacheAppService = responseCacheAppService;
        _updatesConverter = updatesConverter;
        _messageConverter = messageConverter;
        _createdChatCacheHelper = createdChatCacheHelper;
        _logger = logger;
        _queryProcessor = queryProcessor;
    }

    public Task HandleAsync(IDomainEvent<MessageSaga, MessageSagaId, SendOutboxMessageCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return domainEvent.AggregateEvent.MessageItem.MessageSubType switch
        {
            MessageSubType.CreateChat => HandleCreateChatAsync(domainEvent.AggregateEvent, domainEvent.Metadata.SourceId.Value),
            MessageSubType.CreateChannel => HandleCreateChannelAsync(domainEvent.AggregateEvent, domainEvent.Metadata.SourceId.Value),
            MessageSubType.InviteToChannel => HandleInviteToChannelAsync(domainEvent.AggregateEvent, domainEvent.Metadata.SourceId.Value),
            MessageSubType.UpdatePinnedMessage => HandleUpdatePinnedMessageAsync(domainEvent.AggregateEvent, domainEvent.Metadata.SourceId.Value),
            _ => HandleSendMessageAsync(domainEvent.AggregateEvent),
        };
    }

    public Task HandleAsync(IDomainEvent<MessageSaga, MessageSagaId, ReceiveInboxMessageCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return domainEvent.AggregateEvent.MessageItem.MessageSubType switch
        {
            MessageSubType.CreateChat => HandleCreateChatAsync(domainEvent.AggregateEvent),
            MessageSubType.UpdatePinnedMessage => HandleUpdatePinnedMessageAsync(domainEvent.AggregateEvent),
            MessageSubType.ForwardMessage => HandleForwardMessageAsync(domainEvent.AggregateEvent),
            _ => HandleReceiveMessageAsync(domainEvent.AggregateEvent)
        };
    }

    private async Task ReplyRpcResultToSenderAsync(long reqMsgId,
        Peer toPeer,
        IUpdates updates,
        int groupItemCount,
        long selfUserId,
        int pts)
    {
        if (groupItemCount > 1)
        {
            await SendMultiMediaResultAsync(reqMsgId,
                toPeer,
                updates,
                groupItemCount,
                selfUserId,
                pts).ConfigureAwait(false);
        }
        else
        {
            await SendRpcMessageToClientAsync(reqMsgId,
                updates,
                null,
                selfUserId,
                pts,
                toPeer.PeerType).ConfigureAwait(false);
        }
    }

    private async Task SendMultiMediaResultAsync(long reqMsgId, Peer toPeer, IUpdates updates, int groupItemCount, long selfUserId, int pts)
    {
        var cachedCount = _responseCacheAppService.AddToCache(reqMsgId, updates);
        if (cachedCount == groupItemCount)
        {
            if (_responseCacheAppService.TryRemoveResponseList(reqMsgId, out var responseList))
            {
                var updatesAllInOne = new TUpdates
                {
                    Updates = new TVector<IUpdate>(),
                    Users = new TVector<IUser>(),
                    Chats = new TVector<IChat>(),
                    Date = DateTime.UtcNow.ToTimestamp()
                };
                foreach (var allUpdate in responseList)
                {
                    if (allUpdate is TUpdates updatesItem)
                    {
                        foreach (var update in updatesItem.Updates)
                        {
                            updatesAllInOne.Updates.Add(update);
                        }
                    }
                }

                await SendRpcMessageToClientAsync(reqMsgId,
                    updatesAllInOne,
                    null,
                    selfUserId,
                    pts,
                    toPeer.PeerType).ConfigureAwait(false);
            }
        }
    }

    private async Task PushUpdatesToPeerAsync(Peer toPeer,
        IUpdates updates,
        long excludeAuthKeyId = 0,
        long excludeUid = 0,
        long onlySendToThisAuthKeyId = 0,
        int pts = 0,
        PtsType ptsType = PtsType.Unknown,
        IMessage? newMessage = null)
    {
        long globalSeqNo = 0;
        if (pts > 0)
        {
            var dataBytes = updates.ToBytes();
            globalSeqNo = await SavePushUpdatesAsync(
                toPeer,
                dataBytes,
                pts,
                ptsType,
                excludeAuthKeyId,
                excludeUid,
                onlySendToThisAuthKeyId,
                newMessage).ConfigureAwait(false);
        }

        await _objectMessageSender.PushMessageToPeerAsync(toPeer,
            updates,
            excludeAuthKeyId,
            excludeUid,
            onlySendToThisAuthKeyId,
            pts,
            ptsType,
            globalSeqNo).ConfigureAwait(false);
    }

    private async Task<long> SavePushUpdatesAsync(Peer toPeer,
        byte[] data,
        int pts,
        PtsType ptsType,
        long excludeAuthKeyId,
        long excludeUid,
        long onlySendToThisAuthKeyId,
        IMessage? newMessage = null)
    {
        if (pts == 0)
        {
            return 0;
        }

        var globalSeqNo = await _idGenerator.NextLongIdAsync(IdType.GlobalSeqNo).ConfigureAwait(false);
        var dataBytes = newMessage == null ? data : newMessage.ToBytes();
        var command = new CreatePushUpdatesCommand(PushUpdatesId.Create(
                toPeer.PeerId,
                excludeAuthKeyId,
                excludeUid,
                onlySendToThisAuthKeyId,
                pts),
            toPeer,
            excludeAuthKeyId,
            excludeUid,
            onlySendToThisAuthKeyId,
            dataBytes,
            pts,
            ptsType,
            globalSeqNo);
        await _commandBus.PublishAsync(command, default).ConfigureAwait(false);
        return globalSeqNo;
    }

    private async Task SendRpcMessageToClientAsync(
        long reqMsgId,
        IObject rpcData,
        string? sourceId = null,
        long selfUserId = 0,
        int pts = 0,
        PeerType toPeerType = PeerType.User
    )
    {
        if (!string.IsNullOrEmpty(sourceId))
        {
            await SaveRpcResultAsync(reqMsgId, sourceId, selfUserId, rpcData).ConfigureAwait(false);
        }

        if (pts > 0 && selfUserId != 0 && toPeerType != PeerType.Channel)
        {
            await _ackCacheService.AddRpcPtsToCacheAsync(reqMsgId, pts, 0, new Peer(PeerType.User, selfUserId))
                .ConfigureAwait(false);
        }

        await _objectMessageSender.SendRpcMessageToClientAsync(reqMsgId, rpcData).ConfigureAwait(false);
    }

    private Task SaveRpcResultAsync(long reqMsgId,
        string sourceId,
        long peerId,
        IObject rpcResult)
    {
        var command = new CreateRpcResultCommand(RpcResultId.Create(sourceId),
            reqMsgId,
            peerId,
            sourceId,
            rpcResult.ToBytes());
        return _commandBus.PublishAsync(command, default);
    }

    private Task AddRpcGlobalSeqNoForAuthKeyIdAsync(long reqMsgId,
        long selfUserId,
        long globalSeqNo)
    {
        return _ackCacheService.AddRpcPtsToCacheAsync(reqMsgId, 0, globalSeqNo, new Peer(PeerType.User, selfUserId));
    }

    private async Task PushUpdatesToChannelSingleMemberAsync(Peer channelMemberPeer,
        IUpdates updates,
        long excludeAuthKeyId = 0,
        long excludeUid = 0,
        long onlySendToThisAuthKeyId = 0,
        int pts = 0,
        PtsType ptsType = PtsType.Unknown)
    {
        var globalSeqNo = await SavePushUpdatesAsync(
            channelMemberPeer,
            updates.ToBytes(),
            pts,
            ptsType,
            excludeAuthKeyId,
            excludeUid,
            onlySendToThisAuthKeyId).ConfigureAwait(false);
        await _objectMessageSender.PushMessageToPeerAsync(channelMemberPeer,
            updates,
            excludeAuthKeyId,
            excludeUid,
            onlySendToThisAuthKeyId,
            pts,
            ptsType,
            globalSeqNo).ConfigureAwait(false);
    }
    private async Task PushUpdatesToChannelMemberAsync(
        Peer channelPeer,
        IUpdates updates,
        long excludeAuthKeyId = 0,
        long excludeUid = 0,
        long onlySendToThisAuthKeyId = 0,
        int pts = 0,
        PtsType ptsType = PtsType.Unknown,
        bool skipSaveUpdates = false)
    {
        var globalSeqNo = 0L;
        if (!skipSaveUpdates)
        {
            globalSeqNo = await SavePushUpdatesAsync(
                channelPeer,
                updates.ToBytes(),
                pts,
                ptsType,
                excludeAuthKeyId,
                excludeUid,
                onlySendToThisAuthKeyId).ConfigureAwait(false);
        }

        await _objectMessageSender.PushMessageToPeerAsync(channelPeer,
            updates,
            excludeAuthKeyId,
            excludeUid,
            onlySendToThisAuthKeyId,
            pts,
            ptsType,
            globalSeqNo).ConfigureAwait(false);
    }

    public async Task HandleAsync(IDomainEvent<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var updates =
            _updatesConverter.ToEditUpdates(domainEvent.AggregateEvent, domainEvent.AggregateEvent.SenderPeerId);
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.Request.ReqMsgId,
            updates,
            domainEvent.Metadata.SourceId.Value,
            domainEvent.AggregateEvent.SenderPeerId,
            domainEvent.AggregateEvent.Pts,
            domainEvent.AggregateEvent.ToPeer.PeerType
        ).ConfigureAwait(false);
        await PushUpdatesToPeerAsync(
            new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId),
            updates,
            domainEvent.AggregateEvent.Request.AuthKeyId,
            pts: domainEvent.AggregateEvent.Pts,
            ptsType: PtsType.NewMessages).ConfigureAwait(false);

        // Channel message shares the same message,edit out message should notify channel member
        if (domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel)
        {
            var channelEditUpdates = _updatesConverter.ToEditUpdates(domainEvent.AggregateEvent, 0);
            await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.ToPeer,
                channelEditUpdates,
                pts: domainEvent.AggregateEvent.Pts).ConfigureAwait(false);
        }
    }

    public Task HandleAsync(IDomainEvent<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        //var toPeer = domainEvent.AggregateEvent.ToPeer with { PeerId = domainEvent.AggregateEvent.OwnerPeerId };
        var toPeer = new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId);
        return PushUpdatesToPeerAsync(toPeer,
            _updatesConverter.ToEditUpdates(domainEvent.AggregateEvent),
            pts: domainEvent.AggregateEvent.Pts,
            ptsType: PtsType.NewMessages
        );
    }

    private Task HandleReceiveMessageAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        var updates = _updatesConverter.ToUpdates(aggregateEvent);
        return PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer,
            updates,
            pts: aggregateEvent.Pts
        );
    }

    private async Task HandleSendMessageToChannelAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        var selfUpdates = _updatesConverter.ToSelfUpdates(aggregateEvent);
        var channelUpdates = _updatesConverter.ToChannelMessageUpdates(aggregateEvent);
        var ptsType = PtsType.OtherUpdates;
        if (item.MessageSubType == MessageSubType.Normal || item.MessageSubType == MessageSubType.ForwardMessage)
        {
            ptsType = PtsType.NewMessages;
        }

        var globalSeqNo = await SavePushUpdatesAsync(item.ToPeer,
            channelUpdates.ToBytes(),
            aggregateEvent.Pts,
            ptsType,
            aggregateEvent.Request.AuthKeyId,
            aggregateEvent.Request.UserId,
            0).ConfigureAwait(false);
        await AddRpcGlobalSeqNoForAuthKeyIdAsync(aggregateEvent.Request.ReqMsgId, item.SenderPeer.PeerId, globalSeqNo).ConfigureAwait(false);

        if (aggregateEvent.Request.ReqMsgId == 0 || item.MessageSubType == MessageSubType.ForwardMessage)
        {
            await PushUpdatesToPeerAsync(item.SenderPeer,
                selfUpdates,
                pts: aggregateEvent.Pts,
                ptsType: ptsType
            ).ConfigureAwait(false);
        }
        else
        {
            await ReplyRpcResultToSenderAsync(aggregateEvent.Request.ReqMsgId,
                aggregateEvent.MessageItem.SenderPeer,
                selfUpdates,
                aggregateEvent.GroupItemCount,
                aggregateEvent.Request.UserId,
                aggregateEvent.Pts
            ).ConfigureAwait(false);

            var newMessage = _messageConverter.ToMessage(item);
            await PushUpdatesToPeerAsync(item.SenderPeer,
                _updatesConverter.ToSelfOtherDeviceUpdates(aggregateEvent),
                excludeAuthKeyId: aggregateEvent.Request.AuthKeyId,
                pts: aggregateEvent.Pts,
                newMessage: newMessage
            ).ConfigureAwait(false);
        }
        await PushUpdatesToPeerAsync(item.ToPeer, channelUpdates, aggregateEvent.Request.AuthKeyId).ConfigureAwait(false);
    }

    private async Task HandleSendMessageAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        if (item.ToPeer.PeerType == PeerType.Channel)
        {
            await HandleSendMessageToChannelAsync(aggregateEvent);
            return;
        }

        var selfUpdates = _updatesConverter.ToSelfUpdates(aggregateEvent);
        var ptsType = PtsType.OtherUpdates;
        IMessage? newMessage = null;
        if (item.MessageSubType == MessageSubType.Normal ||
            item.MessageSubType == MessageSubType.ForwardMessage)
        {
            ptsType = PtsType.NewMessages;
            newMessage = _messageConverter.ToMessage(item);
        }

        // when reqMsgId==0?
        // forward message reqMsgId==0
        if (aggregateEvent.Request.ReqMsgId == 0)
        {
            await PushUpdatesToPeerAsync(item.SenderPeer,
                selfUpdates,
                pts: aggregateEvent.Pts,
                ptsType: ptsType
            ).ConfigureAwait(false);
        }
        else
        {
            await ReplyRpcResultToSenderAsync(aggregateEvent.Request.ReqMsgId,
                item.SenderPeer,
                selfUpdates,
                aggregateEvent.GroupItemCount,
                item.SenderPeer.PeerId,
                aggregateEvent.Pts
            ).ConfigureAwait(false);
        }

        await PushUpdatesToPeerAsync(item.SenderPeer,
            _updatesConverter.ToSelfOtherDeviceUpdates(aggregateEvent),
            aggregateEvent.Request.AuthKeyId,
            pts: aggregateEvent.Pts,
            newMessage: newMessage
        ).ConfigureAwait(false);
    }

    private async Task HandleCreateChatAsync(SendOutboxMessageCompletedEvent aggregateEvent, string sourceId)
    {
        if (_createdChatCacheHelper.TryGetValue(aggregateEvent.MessageItem.ToPeer.PeerId, out var eventData))
        {
            var updates = _updatesConverter.ToCreateChatUpdates(eventData, aggregateEvent);
            await SendRpcMessageToClientAsync(aggregateEvent.Request.ReqMsgId,
                updates,
                sourceId,
                pts: aggregateEvent.Pts).ConfigureAwait(false);
            await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.SenderPeer,
                updates,
                aggregateEvent.Request.AuthKeyId,
                pts: aggregateEvent.Pts
            ).ConfigureAwait(false);
        }
    }


    private Task HandleCreateChatAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        if (_createdChatCacheHelper.TryGetValue(aggregateEvent.MessageItem.ToPeer.PeerId, out var eventData))
        {
            var updates = _updatesConverter.ToCreateChatUpdates(eventData, aggregateEvent);

            return PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer,
                updates,
                pts: aggregateEvent.Pts
            );
        }

        return Task.CompletedTask;
    }


    private async Task HandleCreateChannelAsync(SendOutboxMessageCompletedEvent aggregateEvent, string sourceId)
    {
        if (_createdChatCacheHelper.TryRemove(aggregateEvent.MessageItem.ToPeer.PeerId, out ChannelCreatedEvent? eventData))
        {
            var updates = _updatesConverter.ToCreateChannelUpdates(eventData, aggregateEvent);
            await SendRpcMessageToClientAsync(aggregateEvent.Request.ReqMsgId,
                updates,
                sourceId,
                aggregateEvent.MessageItem.SenderPeer.PeerId
            ).ConfigureAwait(false);
            await PushUpdatesToChannelSingleMemberAsync(aggregateEvent.MessageItem.SenderPeer,
                updates,
                aggregateEvent.Request.AuthKeyId,
                pts: aggregateEvent.Pts
            ).ConfigureAwait(false);
        }
        else
        {
            _logger.LogWarning("Can not find create channel cache info,channelId={ChannelId}",
                aggregateEvent.MessageItem.ToPeer.PeerId);
        }
    }

    private async Task HandleInviteToChannelAsync(SendOutboxMessageCompletedEvent aggregateEvent, string sourceId)
    {
        if (_createdChatCacheHelper.TryRemove(aggregateEvent.MessageItem.ToPeer.PeerId,
                out StartInviteToChannelEvent? startInviteToChannelEvent))
        {
            var item = aggregateEvent.MessageItem;
            // TODO: Create channel info from domain events
            var channelReadModel = await _queryProcessor
                .ProcessAsync(new GetChannelByIdQuery(item.ToPeer.PeerId), default).ConfigureAwait(false);

            var updates = _updatesConverter.ToInviteToChannelUpdates(aggregateEvent,
                startInviteToChannelEvent,
                channelReadModel,
                true);
            await SendRpcMessageToClientAsync(aggregateEvent.Request.ReqMsgId,
                updates,
                sourceId,
                item.SenderPeer.PeerId).ConfigureAwait(false);
            // notify self other devices
            await PushUpdatesToChannelSingleMemberAsync(item.SenderPeer, updates, aggregateEvent.Request.AuthKeyId)
                .ConfigureAwait(false);
            var updatesForChannelMember = _updatesConverter.ToInviteToChannelUpdates(aggregateEvent,
                startInviteToChannelEvent,
                channelReadModel,
                false);
            // notify channel members
            await PushUpdatesToChannelMemberAsync(item.ToPeer,
                updatesForChannelMember,
                excludeUid: item.SenderPeer.PeerId,
                pts: aggregateEvent.Pts).ConfigureAwait(false);
        }
    }
    private Task HandleForwardMessageAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        var updates = _updatesConverter.ToInboxForwardMessageUpdates(aggregateEvent);
        return PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer, updates, pts: aggregateEvent.Pts);
    }
    private Task HandleUpdatePinnedMessageAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        var updates = _updatesConverter.ToUpdatePinnedMessageUpdates(aggregateEvent);
        return PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer, updates, pts: aggregateEvent.Pts);
    }

    private async Task HandleUpdatePinnedMessageAsync(SendOutboxMessageCompletedEvent aggregateEvent, string sourceId)
    {
        var updates = _updatesConverter.ToUpdatePinnedMessageUpdates(aggregateEvent);
        await SendRpcMessageToClientAsync(aggregateEvent.Request.ReqMsgId,
            updates,
            sourceId,
            aggregateEvent.MessageItem.SenderPeer.PeerId,
            aggregateEvent.Pts,
            aggregateEvent.MessageItem.ToPeer.PeerType
        ).ConfigureAwait(false);
        await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.SenderPeer, updates,
            excludeAuthKeyId: aggregateEvent.Request.AuthKeyId,
            pts: aggregateEvent.Pts);

        if (aggregateEvent.MessageItem.ToPeer.PeerType == PeerType.Channel)
        {
            var channelUpdates = _updatesConverter.ToUpdatePinnedMessageServiceUpdates(aggregateEvent);
            await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.ToPeer,
                channelUpdates,
                aggregateEvent.Request.AuthKeyId, pts: aggregateEvent.Pts).ConfigureAwait(false);
        }
    }
}