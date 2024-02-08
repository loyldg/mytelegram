// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

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
/// See <a href="https://corefork.telegram.org/method/messages.editExportedChatInvite" />
///</summary>
internal sealed class EditExportedChatInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestEditExportedChatInvite, MyTelegram.Schema.Messages.IExportedChatInvite>,
    Messages.IEditExportedChatInviteHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly ICommandBus _commandBus;
    private readonly IChatInviteLinkHelper _chatInviteLinkHelper;

    public EditExportedChatInviteHandler(IQueryProcessor queryProcessor, IAccessHashHelper accessHashHelper, IPeerHelper peerHelper, ICommandBus commandBus, IIdGenerator idGenerator, IChatInviteLinkHelper chatInviteLinkHelper)
    {
        _queryProcessor = queryProcessor;
        _accessHashHelper = accessHashHelper;
        _commandBus = commandBus;
        _chatInviteLinkHelper = chatInviteLinkHelper;
    }

    protected override async Task<MyTelegram.Schema.Messages.IExportedChatInvite> HandleCoreAsync(IRequestInput input,
        RequestEditExportedChatInvite obj)
    {
        //var peer = _peerHelper.GetPeer(obj.Peer);
        //if (peer.PeerType == PeerType.Channel)
        //{
        //    await _accessHashHelper.CheckAccessHashAsync(peer.PeerId)
        //}
        //var chatInviteReadModel=await _queryProcessor .ProcessAsync(new GetChatInviteByLinkQuery(obj.Peer.))
        switch (obj.Peer)
        {
            case TInputPeerChannel inputPeerChannel:
                {
                    var link = obj.Link.Substring(obj.Link.LastIndexOf("/") + 2);
                    await _accessHashHelper.CheckAccessHashAsync(inputPeerChannel);
                    var chatInviteReadModel = await _queryProcessor.ProcessAsync(new GetChatInviteQuery(inputPeerChannel.ChannelId,
                        link));
                    if (chatInviteReadModel == null)
                    {
                        RpcErrors.RpcErrors400.PeerIdInvalid.ThrowRpcError();
                    }

                    var channelReadModel =
                        await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(inputPeerChannel.ChannelId));
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
                        newHash = _chatInviteLinkHelper.GenerateInviteLink();
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

                    await _commandBus.PublishAsync(command, default);
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
