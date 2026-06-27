using MyTelegram.Messenger.Converters.ConverterServices;
using IExportedChatInvite = MyTelegram.Schema.IExportedChatInvite;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get info about the chat invites of a specific chat
/// Possible errors
/// Code Type Description
/// 400 ADMIN_ID_INVALID The specified admin ID is invalid.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getExportedChatInvites"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetExportedChatInvitesHandler(IPeerHelper peerHelper, IQueryProcessor queryProcessor, IChannelAppService channelAppService, IUserConverterService userConverterService, IChatInviteExportedConverterService chatInviteExportedConverterService) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetExportedChatInvites, MyTelegram.Schema.Messages.IExportedChatInvites>
{
    protected override async Task<MyTelegram.Schema.Messages.IExportedChatInvites> HandleCoreAsync(IRequestInput input, RequestGetExportedChatInvites obj)
    {
        var peer = peerHelper.GetPeer(obj.Peer, input.UserId);
        var channelReadModel = await channelAppService.GetAsync(peer.PeerId, false);
        if (channelReadModel == null!)
        {
            RpcErrors.RpcErrors400.PeerIdInvalid.ThrowRpcError();
        }

        if (channelReadModel!.AdminList.All(p => p.UserId != input.UserId))
        {
            RpcErrors.RpcErrors400.ChatAdminRequired.ThrowRpcError();
        }

        var admin = peerHelper.GetPeer(obj.AdminId, input.UserId);
        var invites = await queryProcessor.ProcessAsync(new GetChatInvitesQuery(obj.Revoked, peer.PeerId, admin.PeerId, obj.OffsetDate, obj.OffsetLink ?? string.Empty, obj.Limit));
        var userIds = invites.Select(p => p.AdminId).ToList();
        var users = await userConverterService.GetUserListAsync(input, userIds, false, false, input.Layer);
        //var tInvites = invites.Select(p => objectMapper.Map<IChatInviteReadModel, TChatInviteExported>(p)).ToList();
        //tInvites.ForEach(p => p.Link = chatInviteLinkHelper.GetFullLink(options.Value.JoinChatDomain, p.Link));
        var tInvites = invites.Select(p => chatInviteExportedConverterService.ToExportedChatInvite(p, input.Layer));
        var r = new TExportedChatInvites
        {
            Count = invites.Count,
            Invites = [.. tInvites],
            Users = [.. users],
        };
        return r;
    }
}