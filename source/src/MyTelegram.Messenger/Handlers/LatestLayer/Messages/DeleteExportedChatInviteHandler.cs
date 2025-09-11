namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Delete a chat invite
/// <para>Possible errors</para>
/// Code Type Description
/// 400 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 INVITE_REVOKED_MISSING The specified invite link was already revoked or is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.deleteExportedChatInvite" />
///</summary>
internal sealed class DeleteExportedChatInviteHandler(
    IQueryProcessor queryProcessor,
    IAccessHashHelper accessHashHelper,
    ICommandBus commandBus,
    IChannelAdminRightsChecker channelAdminRightsChecker,
    IChatInviteLinkHelper chatInviteLinkHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteExportedChatInvite, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDeleteExportedChatInvite obj)
    {
        switch (obj.Peer)
        {
            case TInputPeerChannel inputPeerChannel:
                {
                    var link = chatInviteLinkHelper.GetHashFromLink(obj.Link);
                    await accessHashHelper.CheckAccessHashAsync(input, inputPeerChannel);
                    var chatInviteReadModel = await queryProcessor.ProcessAsync(new GetChatInviteQuery(inputPeerChannel.ChannelId, link));
                    if (chatInviteReadModel == null)
                    {
                        RpcErrors.RpcErrors400.PeerIdInvalid.ThrowRpcError();
                    }

                    await channelAdminRightsChecker.CheckAdminRightAsync(inputPeerChannel.ChannelId, input.UserId,
                        (p) => p.AdminRights.ChangeInfo, RpcErrors.RpcErrors403.ChatAdminRequired);

                    var command = new DeleteExportedInviteCommand(
                        ChatInviteId.Create(inputPeerChannel.ChannelId, chatInviteReadModel!.InviteId),
                        input.ToRequestInfo());
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
