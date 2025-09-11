using IExportedChatInvite = MyTelegram.Schema.Messages.IExportedChatInvite;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Edit an exported chat invite
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_INVITE_PERMANENT You can't set an expiration date on permanent invite links.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 403 EDIT_BOT_INVITE_FORBIDDEN Normal users can't edit invites that were created by bots.
/// 400 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USAGE_LIMIT_INVALID The specified usage limit is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.editExportedChatInvite" />
///</summary>
internal sealed class EditExportedChatInviteHandler(
    IQueryProcessor queryProcessor,
    IChannelAppService channelAppService,
    IAccessHashHelper accessHashHelper,
    ICommandBus commandBus,
    IChatInviteLinkHelper chatInviteLinkHelper)
    : RpcResultObjectHandler<Schema.Messages.RequestEditExportedChatInvite, IExportedChatInvite>
{
    protected override async Task<IExportedChatInvite> HandleCoreAsync(IRequestInput input,
        RequestEditExportedChatInvite obj)
    {
        switch (obj.Peer)
        {
            case TInputPeerChannel inputPeerChannel:
            {
                var link = obj.Link.Substring(obj.Link.LastIndexOf("/") + 2);
                await accessHashHelper.CheckAccessHashAsync(input, inputPeerChannel);
                var chatInviteReadModel = await queryProcessor.ProcessAsync(new GetChatInviteQuery(
                    inputPeerChannel.ChannelId,
                    link));
                if (chatInviteReadModel == null)
                {
                    RpcErrors.RpcErrors400.PeerIdInvalid.ThrowRpcError();
                }

                var channelReadModel = await channelAppService.GetAsync(inputPeerChannel.ChannelId);
                if (channelReadModel == null)
                {
                    RpcErrors.RpcErrors400.ChannelIdInvalid.ThrowRpcError();
                }

                var admin = channelReadModel!.AdminList.FirstOrDefault(p => p.UserId == input.UserId);
                if (admin == null || !admin.AdminRights.ChangeInfo)
                {
                    RpcErrors.RpcErrors400.ChatAdminRequired.ThrowRpcError();
                }

                //var inviteId = chatInviteReadModel!.InviteId;
                var hash = link;
                string? newHash = null;
                if (obj.Revoked)
                {
                    newHash = chatInviteLinkHelper.GenerateInviteLink();
                }

                var command = new EditChatInviteCommand(
                    ChatInviteId.Create(inputPeerChannel.ChannelId, chatInviteReadModel!.InviteId),
                    input.ToRequestInfo(),
                    inputPeerChannel.ChannelId,
                    chatInviteReadModel.InviteId,
                    hash,
                    newHash,
                    input.UserId,
                    obj.Title,
                    obj.RequestNeeded ?? false,
                    null,
                    obj.ExpireDate,
                    obj.UsageLimit,
                    chatInviteReadModel.Permanent,
                    obj.Revoked
                );

                await commandBus.PublishAsync(command, default);

                return null!;
            }

            case TInputPeerChat inputPeerChat:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        throw new NotImplementedException();
    }
}