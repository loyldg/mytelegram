namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Delete all revoked chat invites
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ADMIN_ID_INVALID The specified admin ID is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.deleteRevokedExportedChatInvites" />
///</summary>
internal sealed class DeleteRevokedExportedChatInvitesHandler(
    IQueryProcessor queryProcessor,
    IPeerHelper peerHelper,
    IAccessHashHelper accessHashHelper,
    ICommandBus commandBus,
    IChannelAdminRightsChecker channelAdminRightsChecker)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteRevokedExportedChatInvites, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDeleteRevokedExportedChatInvites obj)
    {
        switch (obj.Peer)
        {
            case TInputPeerChannel inputPeerChannel:
                await accessHashHelper.CheckAccessHashAsync(input, inputPeerChannel);
                var peer = peerHelper.GetPeer(obj.Peer);
                var adminId = peerHelper.GetPeer(obj.AdminId, input.UserId);
                await channelAdminRightsChecker.CheckAdminRightAsync(inputPeerChannel.ChannelId, input.UserId,
                    (p) => p.AdminRights.ChangeInfo, RpcErrors.RpcErrors403.ChatAdminRequired);

                var chatInvites = await queryProcessor.ProcessAsync(new GetRevokedChatInvitesQuery(peer.PeerId, adminId.PeerId));
                foreach (var chatInviteReadModel in chatInvites)
                {
                    var command = new DeleteExportedInviteCommand(ChatInviteId.Create(inputPeerChannel.ChannelId, chatInviteReadModel.InviteId), input.ToRequestInfo());
                    await commandBus.PublishAsync(command, default);
                }
                break;
            case TInputPeerChat inputPeerChat:
                break;
        }


        return new TBoolTrue();
    }
}
