namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Export an invite link for a chat
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 EXPIRE_DATE_INVALID The specified expiration date is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USAGE_LIMIT_INVALID The specified usage limit is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.exportChatInvite" />
///</summary>
internal sealed class ExportChatInviteHandler(
    ICommandBus commandBus,
    IRandomHelper randomHelper,
    IAccessHashHelper accessHashHelper,
    IIdGenerator idGenerator,
    IChannelAppService channelAppService,
    IChatInviteLinkHelper chatInviteLinkHelper,
    IChannelAdminRightsChecker channelAdminRightsChecker)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestExportChatInvite, MyTelegram.Schema.IExportedChatInvite>
{
    private readonly IRandomHelper _randomHelper = randomHelper;

    protected override async Task<MyTelegram.Schema.IExportedChatInvite> HandleCoreAsync(IRequestInput input,
        RequestExportChatInvite obj)
    {
        if (obj.Peer is TInputPeerChannel inputPeerChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(input, inputPeerChannel.ChannelId, inputPeerChannel.AccessHash, AccessHashType.Channel);

            var chatInviteId = await idGenerator.NextLongIdAsync(IdType.InviteId, inputPeerChannel.ChannelId);
            var inviteHash = chatInviteLinkHelper.GenerateInviteLink();
            var channelReadModel = await channelAppService.GetAsync(inputPeerChannel.ChannelId);
            if (channelReadModel == null)
            {
                RpcErrors.RpcErrors400.ChannelIdInvalid.ThrowRpcError();
            }

            await channelAdminRightsChecker.CheckAdminRightAsync(inputPeerChannel.ChannelId, input.UserId,
                (p) => p.AdminRights.ChangeInfo, RpcErrors.RpcErrors403.ChatAdminRequired);

            var command = new CreateChatInviteCommand(ChatInviteId.Create(inputPeerChannel.ChannelId, chatInviteId),
                input.ToRequestInfo(),
                inputPeerChannel.ChannelId,
                chatInviteId,
                inviteHash,
                input.UserId,
                obj.Title,
                obj.RequestNeeded,
                null,
                obj.ExpireDate,
                obj.UsageLimit,
                false,
                CurrentDate,
                channelReadModel!.Broadcast
            );

            await commandBus.PublishAsync(command);
            return null!;
        }

        throw new NotImplementedException();
    }
}
