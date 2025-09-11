namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Dismiss or approve all <a href="https://corefork.telegram.org/api/invites#join-requests">join requests</a> related to a specific chat or channel.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNELS_TOO_MUCH You have joined too many channels/supergroups.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 HIDE_REQUESTER_MISSING The join request was missing or was already handled.
/// 400 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_CHANNELS_TOO_MUCH One of the users you tried to add is already in too many channels/supergroups.
/// See <a href="https://corefork.telegram.org/method/messages.hideAllChatJoinRequests" />
///</summary>
internal sealed class HideAllChatJoinRequestsHandler(
    IQueryProcessor queryProcessor,
    IPeerHelper peerHelper,
    IAccessHashHelper accessHashHelper,
    IChannelAppService channelAppService,
    IChatConverterService chatConverterService,
    IChannelAdminRightsChecker channelAdminRightsChecker,
    ICommandBus commandBus)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestHideAllChatJoinRequests, MyTelegram.Schema.IUpdates>
{
    protected override async Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestHideAllChatJoinRequests obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Peer);
        var channelPeer = peerHelper.GetPeer(obj.Peer);
        var channelId = channelPeer.PeerId;
        if (string.IsNullOrEmpty(obj.Link))
        {
            RpcErrors.RpcErrors400.InviteHashEmpty.ThrowRpcError();
        }
        await channelAdminRightsChecker.CheckAdminRightAsync(channelId, input.UserId,
            p => p.AdminRights.InviteUsers, RpcErrors.RpcErrors403.ChatAdminRequired);

        long? inviteId = null;
        if (!string.IsNullOrEmpty(obj.Link))
        {
            var chatInviteReadModel = await queryProcessor.ProcessAsync(new GetChatInviteByLinkQuery(obj.Link));
            if (chatInviteReadModel == null)
            {
                RpcErrors.RpcErrors400.InviteHashInvalid.ThrowRpcError();
            }

            inviteId = chatInviteReadModel!.InviteId;
        }

        var channelHistoryMinId = 0;
        int topMessageId = 0;
        bool broadcast = false;

        if (obj.Approved)
        {
            var channelReadModel = await channelAppService.GetAsync(channelId);
            if (channelReadModel.HiddenPreHistory)
            {
                channelHistoryMinId = channelReadModel.TopMessageId;
            }
            topMessageId = channelReadModel.TopMessageId;
            broadcast = channelReadModel.Broadcast;
        }

        await HideAllChatJoinRequestsAsync(input.ToRequestInfo(), channelId, inviteId, obj.Approved, topMessageId,
            channelHistoryMinId, broadcast);

        var channel = await chatConverterService.GetChannelAsync(input, channelId, false, false, input.Layer);

        return new TUpdates
        {
            Chats = [channel],
            Date = CurrentDate,
            Updates = new TVector<IUpdate>(new TUpdateChannel
            {
                ChannelId = channelPeer.PeerId,
            }),
            Users = []
        };
    }

    private async Task HideAllChatJoinRequestsAsync(RequestInfo requestInfo, long channelId, long? inviteId,
        bool approved, int topMessageId, int channelHistoryMinId, bool broadcast)
    {
        var pageSize = 1000;
        var hasMoreData = true;

        while (hasMoreData)
        {
            var chatInviteImporters = await queryProcessor.ProcessAsync(new GetChatInviteImportersQuery(channelId,
                ChatInviteRequestState.WaitingForApproval, inviteId, null, null, null, pageSize));
            foreach (var joinChannelRequestReadModel in chatInviteImporters)
            {
                if (joinChannelRequestReadModel.IsJoinRequestProcessed)
                {
                    continue;
                }

                var command = new HideChatJoinRequestCommand(
                    JoinChannelId.Create(channelId, joinChannelRequestReadModel.UserId),
                    requestInfo,
                    joinChannelRequestReadModel.UserId,
                    approved,
                    topMessageId,
                    channelHistoryMinId,
                    broadcast
                );
                await commandBus.PublishAsync(command);
            }

            hasMoreData = chatInviteImporters.Count == pageSize;
        }
    }
}
