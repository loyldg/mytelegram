using MyTelegram.Domain.Aggregates.RpcResult;
using MyTelegram.Domain.Aggregates.Updates;
using MyTelegram.Domain.Commands.Pts;
using MyTelegram.Domain.Commands.RpcResult;
using MyTelegram.Domain.Commands.Updates;

namespace MyTelegram.Messenger.DomainEventHandlers;

public abstract class DomainEventHandlerBase(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService
)
{
    protected Task AddRpcGlobalSeqNoForAuthKeyIdAsync(
        long reqMsgId,
        long selfUserId,
        long globalSeqNo
    )
    {
        return ackCacheService.AddRpcPtsToCacheAsync(
            reqMsgId,
            0,
            globalSeqNo,
            new Peer(PeerType.User, selfUserId)
        );
    }

    protected async Task PushUpdatesToChannelMemberAsync(
        Peer senderPeer,
        Peer channelPeer,
        IUpdates updates,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        UpdatesType updatesType = UpdatesType.Updates,
        int pts = 0,
        bool skipSaveUpdates = false
    )
    {
        var globalSeqNo = 0L;
        if (!skipSaveUpdates)
        {
            globalSeqNo = await SavePushUpdatesAsync(
                channelPeer.PeerId,
                updates,
                pts,
                excludeAuthKeyId,
                excludeUserId,
                onlySendToUserId,
                onlySendToThisAuthKeyId,
                [senderPeer.PeerId],
                updatesType: updatesType
            );
        }

        await objectMessageSender.PushMessageToPeerAsync(
            channelPeer,
            updates,
            excludeAuthKeyId,
            excludeUserId,
            onlySendToUserId,
            onlySendToThisAuthKeyId,
            pts,
            globalSeqNo: globalSeqNo
        );
    }

    protected async Task PushUpdatesToChannelSingleMemberAsync(
        long channelId,
        Peer channelMemberPeer,
        IUpdates updates,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        int pts = 0,
        UpdatesType updatesType = UpdatesType.Updates,
        long? senderUserId = null
    )
    {
        var globalSeqNo = await SavePushUpdatesAsync(
            channelId,
            updates,
            pts,
            excludeAuthKeyId,
            excludeUserId,
            onlySendToUserId,
            onlySendToThisAuthKeyId,
            updatesType: updatesType
        );
        await objectMessageSender.PushMessageToPeerAsync(
            channelMemberPeer,
            updates,
            excludeAuthKeyId,
            excludeUserId,
            onlySendToUserId,
            onlySendToThisAuthKeyId,
            pts,
            globalSeqNo: globalSeqNo
        );
    }

    protected async Task PushUpdatesToPeerAsync(
        Peer toPeer,
        IUpdates updates,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        int pts = 0,
        UpdatesType updatesType = UpdatesType.Updates,
        long? senderUserId = null,
        bool skipSaveUpdates = false,
        PushData? pushData = null,
        List<long>? excludeUserIds = null
    )
    {
        long globalSeqNo = 0;
        List<long>? users = null;
        List<long>? chats = null;
        if (toPeer.PeerType == PeerType.User)
        {
            users = [toPeer.PeerId];
            if (senderUserId.HasValue)
            {
                users.Add(senderUserId.Value);
            }
        }
        else if (toPeer.PeerType == PeerType.Channel || toPeer.PeerType == PeerType.Chat)
        {
            chats = [toPeer.PeerId];
        }

        if (!skipSaveUpdates)
        {
            globalSeqNo = await SavePushUpdatesAsync(
                toPeer.PeerId,
                updates,
                pts,
                excludeAuthKeyId,
                excludeUserId,
                onlySendToUserId,
                onlySendToThisAuthKeyId,
                users,
                chats,
                updatesType
            );
        }

        await objectMessageSender.PushMessageToPeerAsync(
            toPeer,
            updates,
            excludeAuthKeyId,
            excludeUserId,
            onlySendToUserId,
            onlySendToThisAuthKeyId,
            pts,
            globalSeqNo: globalSeqNo,
            pushData: pushData,
            excludeUserIds: excludeUserIds
        );
    }

    protected Task ReplyRpcResultToSenderAsync(
        RequestInfo requestInfo,
        Peer toPeer,
        IUpdates updates,
        long selfUserId,
        int pts
    )
    {
        return SendRpcMessageToClientAsync(requestInfo, updates, selfUserId, pts, toPeer.PeerType);
    }

    protected async Task<long> SavePushUpdatesAsync(
        long ownerPeerId,
        IUpdates updates,
        int pts,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        List<long>? users = null,
        List<long>? chats = null,
        UpdatesType updatesType = UpdatesType.Updates,
        int? messageId = null
    )
    {
        var userIds = users;
        var chatIds = chats;
        var date = DateTime.UtcNow.ToTimestamp();
        IUpdate[]? allUpdates = null;
        switch (updates)
        {
            case TUpdates updates1:
                allUpdates = updates1.Updates.ToArray();
                date = updates1.Date;
                if (updates1.Users.Count > 0)
                {
                    userIds ??= [];
                    userIds.AddRange(updates1.Users.Select(p => p.Id));
                }

                if (updates1.Chats.Count > 0)
                {
                    chatIds ??= [];
                    chatIds.AddRange(updates1.Chats.Select(p => p.Id));
                }

                foreach (var update in updates1.Updates)
                {
                    if (update is TUpdateMessageID updateMessageId)
                    {
                        messageId = updateMessageId.Id;
                        break;
                    }

                    if (update is TUpdateNewMessage updateNewMessage)
                    {
                        messageId = updateNewMessage.Message.Id;
                        break;
                    }

                    if (update is TUpdateNewChannelMessage updateNewChannelMessage)
                    {
                        messageId = updateNewChannelMessage.Message.Id;
                        break;
                    }
                }

                break;
            case TUpdateShort updateShort:
                date = updateShort.Date;
                allUpdates = [updateShort.Update];
                break;
            case TUpdateShortChatMessage updateShortChatMessage:
                date = updateShortChatMessage.Date;
                messageId = updateShortChatMessage.Id;
                updatesType = UpdatesType.NewMessages;

                break;
            case TUpdateShortMessage updateShortMessage:
                date = updateShortMessage.Date;
                messageId = updateShortMessage.Id;
                updatesType = UpdatesType.NewMessages;
                break;
            case TUpdateShortSentMessage updateShortSentMessage:
                date = updateShortSentMessage.Date;
                messageId = updateShortSentMessage.Id;
                updatesType = UpdatesType.NewMessages;
                break;
            case TUpdatesTooLong:
                return 0;
        }

        var globalSeqNo = await idGenerator.NextLongIdAsync(IdType.GlobalSeqNo);

        _ = Task.Run(() =>
        {
            var command = new CreateUpdatesCommand(
                UpdatesId.New,
                ownerPeerId,
                excludeAuthKeyId,
                excludeUserId,
                onlySendToUserId,
                onlySendToThisAuthKeyId,
                updatesType,
                pts,
                messageId,
                date,
                globalSeqNo,
                allUpdates,
                userIds,
                chatIds
            );
            commandBus.PublishAsync(command);
        });
        return globalSeqNo;
    }

    protected async Task PushMessageToPeerAsync<TData>(
        Peer toPeer,
        TData data,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        int pts = 0,
        UpdatesType updatesType = UpdatesType.Updates,
        long channelId = 0,
        PushData? pushData = null,
        List<long>? excludeUserIds = null
    )
        where TData : IObject
    {
        var globalSeqNo = 0L;
        if (data is IUpdates updates)
        {
            globalSeqNo = await SavePushUpdatesAsync(
                toPeer.PeerId,
                updates,
                pts,
                excludeAuthKeyId,
                excludeUserId,
                onlySendToThisAuthKeyId: onlySendToThisAuthKeyId,
                updatesType: updatesType
            );
        }
        await objectMessageSender.PushMessageToPeerAsync(
            toPeer,
            data,
            excludeAuthKeyId,
            excludeUserId,
            onlySendToUserId,
            onlySendToThisAuthKeyId,
            pts,
            globalSeqNo: globalSeqNo,
            pushData: pushData,
            excludeUserIds: excludeUserIds
        );
    }

    protected Task PushMessageToAuthKeyIdAsync<TData>(
        Peer toPeer,
        TData data,
        long authKeyId,
        int? qts = null,
        UpdatesType updatesType = UpdatesType.Updates
    )
        where TData : IObject
    {
        return objectMessageSender.PushMessageToPeerAsync(
            toPeer,
            data,
            onlySendToThisAuthKeyId: authKeyId,
            qts: qts
        );
    }

    protected async Task ReplyRpcMessageAndNotifySelfOtherDevicesAsync(
        RequestInfo requestInfo,
        IUpdates updates,
        int pts = 0,
        UpdatesType updatesType = UpdatesType.Updates,
        PeerType toPeerType = PeerType.User
    )
    {
        await SendRpcMessageToClientAsync(
            requestInfo,
            updates,
            requestInfo.UserId,
            pts,
            toPeerType
        );
        await PushUpdatesToPeerAsync(
            requestInfo.UserId.ToUserPeer(),
            updates,
            requestInfo.PermAuthKeyId,
            pts: pts,
            updatesType: updatesType
        );
    }

    protected async Task SendRpcMessageToClientAsync<TData>(
        RequestInfo requestInfo,
        TData data,
        long selfUserId = 0,
        int pts = 0,
        PeerType toPeerType = PeerType.User
    )
        where TData : IObject
    {
        if (requestInfo.ReqMsgId == 0)
        {
            return;
        }

        if (pts > 0 && selfUserId != 0 && toPeerType != PeerType.Channel)
        {
            await ackCacheService.AddRpcPtsToCacheAsync(
                requestInfo.ReqMsgId,
                pts,
                0,
                new Peer(PeerType.User, selfUserId)
            );
        }

        await objectMessageSender.SendRpcMessageToClientAsync(requestInfo, data);
        await SaveRpcResultAsync(requestInfo, data);
    }

    protected Task UpdateSelfGlobalSeqNoAfterSendChannelMessageAsync(long userId, long globalSeqNo)
    {
        Task.Run(() =>
        {
            var updateGlobalSeqNoCommand = new UpdateGlobalSeqNoCommand(
                PtsId.Create(userId),
                userId,
                0,
                globalSeqNo
            );
            commandBus.PublishAsync(updateGlobalSeqNoCommand);
        });

        return Task.CompletedTask;
    }

    private Task SaveRpcResultAsync<TData>(
        RequestInfo requestInfo,
        TData data
    )
        where TData : IObject
    {
        if (requestInfo.IsSubRequest)
        {
            return Task.CompletedTask;
        }

        var command = new CreateRpcResultCommand(
            RpcResultId.Create(requestInfo.UserId, requestInfo.ReqMsgId),
            requestInfo,
            data.ToBytes());

        return commandBus.PublishAsync(command);
    }
}
