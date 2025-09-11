namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Dismiss or approve a chat <a href="https://corefork.telegram.org/api/invites#join-requests">join request</a> related to a specific chat or channel.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNELS_TOO_MUCH You have joined too many channels/supergroups.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 HIDE_REQUESTER_MISSING The join request was missing or was already handled.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_ALREADY_PARTICIPANT The user is already in the group.
/// 403 USER_CHANNELS_TOO_MUCH One of the users you tried to add is already in too many channels/supergroups.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.hideChatJoinRequest" />
///</summary>
internal sealed class HideChatJoinRequestHandler(
    IQueryProcessor queryProcessor,
    IPeerHelper peerHelper,
    IAccessHashHelper accessHashHelper,
    IChannelAppService channelAppService,
    IChannelAdminRightsChecker channelAdminRightsChecker,
    ICommandBus commandBus)
    : RpcResultObjectHandler<RequestHideChatJoinRequest, IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestHideChatJoinRequest obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Peer);
        await accessHashHelper.CheckAccessHashAsync(input, obj.UserId);

        var channelPeer = peerHelper.GetPeer(obj.Peer);
        var channelId = channelPeer.PeerId;
        var userPeer = peerHelper.GetPeer(obj.UserId);

        await channelAdminRightsChecker.CheckAdminRightAsync(channelId, input.UserId,
            p => p.AdminRights.InviteUsers, RpcErrors.RpcErrors403.ChatAdminRequired);

        var joinRequestReadModel =
            await queryProcessor.ProcessAsync(new GetJoinRequestQuery(channelId, userPeer.PeerId));
        if (joinRequestReadModel == null)
        {
            RpcErrors.RpcErrors400.HideRequesterMissing.ThrowRpcError();
        }

        if (joinRequestReadModel!.IsJoinRequestProcessed)
        {
            RpcErrors.RpcErrors400.HideRequesterMissing.ThrowRpcError();
        }

        var channelHistoryMinId = 0;
        var topMessageId = 0;
        var broadcast = false;

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

        var command = new HideChatJoinRequestCommand2(
            JoinChannelId.Create(channelId, userPeer.PeerId),
            input.ToRequestInfo(),
            userPeer.PeerId,
            obj.Approved,
            topMessageId,
            channelHistoryMinId,
            broadcast
        );
        await commandBus.PublishAsync(command);

        return null!;
    }
}
