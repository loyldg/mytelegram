namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Associate a group to a channel as <a href="https://corefork.telegram.org/api/discussion">discussion group</a> for that channel
/// Possible errors
/// Code Type Description
/// 400 BROADCAST_ID_INVALID Broadcast ID invalid.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 LINK_NOT_MODIFIED Discussion link not modified.
/// 400 MEGAGROUP_ID_INVALID Invalid supergroup ID.
/// 400 MEGAGROUP_PREHISTORY_HIDDEN Group with hidden history for new members can't be set as discussion groups.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.setDiscussionGroup"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SetDiscussionGroupHandler(ICommandBus commandBus, IChannelAppService channelAppService, IChannelAdminRightsChecker channelAdminRightsChecker) : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestSetDiscussionGroup, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input, RequestSetDiscussionGroup obj)
    {
        var broadcastChannel = obj.Broadcast.ToChannelPeer();
        var broadcastChannelReadModel = await channelAppService.GetAsync(broadcastChannel.PeerId);
        if (!broadcastChannelReadModel.Broadcast)
        {
            RpcErrors.RpcErrors400.BroadcastIdInvalid.ThrowRpcError();
        }

        await channelAdminRightsChecker.CheckAdminRightAsync(broadcastChannel.PeerId, input.UserId, p => p.ChangeInfo);

        long? groupId = null;

        switch (obj.Group)
        {
            case TInputChannel inputChannel:
                groupId = inputChannel.ChannelId;
                break;
            case TInputChannelEmpty _:
                break;
            default:
                RpcErrors.RpcErrors400.MegagroupIdInvalid.ThrowRpcError();
                break;
        }

        if (groupId.HasValue)
        {
            var groupReadModel = await channelAppService.GetAsync(groupId.Value);
            if (groupReadModel.Broadcast || groupReadModel.Forum)
            {
                RpcErrors.RpcErrors400.MegagroupIdInvalid.ThrowRpcError();
            }

            // The group is already linked to another channel
            if (groupReadModel.LinkedChatId.HasValue && groupReadModel.LinkedChatId != broadcastChannel.PeerId)
            {
                RpcErrors.RpcErrors400.MegagroupIdInvalid.ThrowRpcError();
            }

            if (groupReadModel.HiddenPreHistory)
            {
                RpcErrors.RpcErrors400.MegagroupPrehistoryHidden.ThrowRpcError();
            }

            await channelAdminRightsChecker.CheckAdminRightAsync(groupId.Value, input.UserId, p => p.ChangeInfo);
        }

        if (broadcastChannelReadModel.LinkedChatId == groupId)
        {
            RpcErrors.RpcErrors400.LinkNotModified.ThrowRpcError();
        }

        var command = new StartSetChannelDiscussionGroupCommand(TempId.New, input.ToRequestInfo(), broadcastChannel.PeerId, groupId);
        await commandBus.PublishAsync(command);
        return null!;
    }
}