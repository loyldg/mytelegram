namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

/// <summary>
/// Set whether all users should
/// <a href="https://corefork.telegram.org/api/invites#join-requests">request admin approval to join the group »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical
/// to the current information.
/// 400 CHAT_PUBLIC_REQUIRED You can only enable join requests in public groups.
/// See <a href="https://corefork.telegram.org/method/channels.toggleJoinRequest" />
/// </summary>
internal sealed class ToggleJoinRequestHandler(
    IPeerHelper peerHelper,
    IAccessHashHelper accessHashHelper,
    ICommandBus commandBus,
    IChannelAdminRightsChecker channelAdminRightsChecker) : RpcResultObjectHandler<RequestToggleJoinRequest, IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestToggleJoinRequest obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Channel);
        var peer = peerHelper.GetChannel(obj.Channel);
        await channelAdminRightsChecker.CheckAdminRightAsync(peer.PeerId, input.UserId,
            p => p.AdminRights.ChangeInfo, RpcErrors.RpcErrors403.ChatAdminRequired);

        var command = new ToggleJoinRequestCommand(ChannelId.Create(peer.PeerId), input.ToRequestInfo(), obj.Enabled);
        await commandBus.PublishAsync(command);

        return null!;
    }
}