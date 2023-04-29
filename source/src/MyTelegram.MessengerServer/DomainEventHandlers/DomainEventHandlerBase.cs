namespace MyTelegram.MessengerServer.DomainEventHandlers;

public abstract class DomainEventHandlerBase
{
    private readonly IAckCacheService _ackCacheService;
    private readonly ICommandBus _commandBus;
    private readonly IIdGenerator _idGenerator;
    private readonly IObjectMessageSender _objectMessageSender;
    private readonly IResponseCacheAppService _responseCacheAppService;

    protected DomainEventHandlerBase(IObjectMessageSender objectMessageSender,
        ICommandBus commandBus,
        IIdGenerator idGenerator,
        IAckCacheService ackCacheService,
        IResponseCacheAppService responseCacheAppService)
    {
        _objectMessageSender = objectMessageSender;
        _commandBus = commandBus;
        _idGenerator = idGenerator;
        _ackCacheService = ackCacheService;
        _responseCacheAppService = responseCacheAppService;
    }

    protected Task AddRpcGlobalSeqNoForAuthKeyIdAsync(long reqMsgId,
        long selfUserId,
        long globalSeqNo)
    {
        return _ackCacheService.AddRpcPtsToCacheAsync(reqMsgId, 0, globalSeqNo, new Peer(PeerType.User, selfUserId));
    }

    protected async Task PushUpdatesToChannelMemberAsync(
        Peer channelPeer,
        //IEnumerable<Peer> messageReceivePeerList,
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
                pts, //频道只使用GlobalSeqNo,pts直接传0
                ptsType,
                excludeAuthKeyId,
                excludeUid,
                onlySendToThisAuthKeyId);
        }

        await _objectMessageSender.PushMessageToPeerAsync(channelPeer,
            updates,
            excludeAuthKeyId,
            excludeUid,
            onlySendToThisAuthKeyId,
            pts,
            ptsType,
            globalSeqNo);
    }

    protected async Task PushUpdatesToChannelSingleMemberAsync(Peer channelMemberPeer,
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
            onlySendToThisAuthKeyId);
        await _objectMessageSender.PushMessageToPeerAsync(channelMemberPeer,
            updates,
            excludeAuthKeyId,
            excludeUid,
            onlySendToThisAuthKeyId,
            pts,
            ptsType,
            globalSeqNo);
    }

    protected async Task PushUpdatesToPeerAsync(Peer toPeer,
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
                newMessage);
        }

        await _objectMessageSender.PushMessageToPeerAsync(toPeer,
            updates,
            excludeAuthKeyId,
            excludeUid,
            onlySendToThisAuthKeyId,
            pts,
            ptsType,
            globalSeqNo);
        //return globalSeqNo;
    }

    protected async Task ReplyRpcResultToSenderAsync(long reqMsgId,
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
                pts);
        }
        else
        {
            await SendRpcMessageToClientAsync(reqMsgId,
                updates,
                null,
                selfUserId,
                pts,
                toPeer.PeerType);
        }
    }

    protected async Task<long> SavePushUpdatesAsync(Peer toPeer,
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

        var globalSeqNo = await _idGenerator.NextLongIdAsync(IdType.GlobalSeqNo);
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
        await _commandBus.PublishAsync(command, default);
        return globalSeqNo;
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

    protected async Task SendMessageToPeerAsync(Peer toPeer,
        IObject data,
        long excludeAuthKeyId = 0,
        long excludeUid = 0,
        long onlySendToThisAuthKeyId = 0,
        int pts = 0,
        PtsType ptsType = PtsType.Unknown)
    {
        var globalSeqNo = await SavePushUpdatesAsync(
            toPeer,
            data.ToBytes(),
            pts,
            ptsType,
            excludeAuthKeyId,
            excludeUid,
            onlySendToThisAuthKeyId);
        await _objectMessageSender.PushMessageToPeerAsync(toPeer,
            data,
            excludeAuthKeyId,
            excludeUid,
            onlySendToThisAuthKeyId,
            pts,
            ptsType,
            globalSeqNo);
    }

    protected async Task SendMultiMediaResultAsync(long reqMsgId,
        Peer toPeer,
        IUpdates updates,
        int groupItemCount,
        long selfUserId,
        int pts)
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
                    toPeer.PeerType);
            }
        }
    }

    protected async Task SendRpcMessageToClientAsync(
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
            await SaveRpcResultAsync(reqMsgId, sourceId, selfUserId, rpcData);
        }

        if (pts > 0 && selfUserId != 0 && toPeerType != PeerType.Channel)
        {
            await _ackCacheService.AddRpcPtsToCacheAsync(reqMsgId, pts, 0, new Peer(PeerType.User, selfUserId))
                ;
        }

        await _objectMessageSender.SendRpcMessageToClientAsync(reqMsgId, rpcData);
    }

    protected Task UpdateSelfGlobalSeqNoAfterSendChannelMessageAsync(long userId,
        long globalSeqNo)
    {
        var updateGlobalSeqNoCommand = new UpdateGlobalSeqNoCommand(PtsId.Create(userId), userId, 0, globalSeqNo);
        return _commandBus.PublishAsync(updateGlobalSeqNoCommand, default);
    }
}
