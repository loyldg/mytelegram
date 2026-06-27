namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Delete a chat invite
/// Possible errors
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 INVITE_REVOKED_MISSING The specified invite link was already revoked or is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.deleteExportedChatInvite"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DeleteExportedChatInviteHandler(IQueryProcessor queryProcessor, ICommandBus commandBus, IChannelAdminRightsChecker channelAdminRightsChecker, IChatInviteLinkHelper chatInviteLinkHelper) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteExportedChatInvite, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input, RequestDeleteExportedChatInvite obj)
    {
        switch (obj.Peer)
        {
            case TInputPeerChannel inputPeerChannel:
                {
                    var link = chatInviteLinkHelper.GetHashFromLink(obj.Link);
                    var chatInviteReadModel = await queryProcessor.ProcessAsync(new GetChatInviteQuery(inputPeerChannel.ChannelId, link));
                    if (chatInviteReadModel == null)
                    {
                        RpcErrors.RpcErrors400.PeerIdInvalid.ThrowRpcError();
                    }

                    await channelAdminRightsChecker.CheckAdminRightAsync(inputPeerChannel.ChannelId, input.UserId, (p) => p.ChangeInfo, RpcErrors.RpcErrors403.ChatAdminRequired);
                    var command = new DeleteExportedInviteCommand(ChatInviteId.Create(inputPeerChannel.ChannelId, chatInviteReadModel!.InviteId), input.ToRequestInfo());
                    await commandBus.PublishAsync(command);
                }

                break;
            case TInputPeerChat:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return new TBoolTrue();
    }
}