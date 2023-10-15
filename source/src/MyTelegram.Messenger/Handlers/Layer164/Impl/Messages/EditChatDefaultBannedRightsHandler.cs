// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Edit the default banned rights of a <a href="https://corefork.telegram.org/api/channel">channel/supergroup/group</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BANNED_RIGHTS_INVALID You provided some invalid flags in the banned rights.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 UNTIL_DATE_INVALID Invalid until date provided.
/// See <a href="https://corefork.telegram.org/method/messages.editChatDefaultBannedRights" />
///</summary>
internal sealed class EditChatDefaultBannedRightsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestEditChatDefaultBannedRights, MyTelegram.Schema.IUpdates>,
    Messages.IEditChatDefaultBannedRightsHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public EditChatDefaultBannedRightsHandler(ICommandBus commandBus,
        IPeerHelper peerHelper,
        IAccessHashHelper accessHashHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditChatDefaultBannedRights obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.Peer);
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);

        switch (peer.PeerType)
        {
            case PeerType.Chat:
                {
                    var command = new EditChatDefaultBannedRightsCommand(ChatId.Create(peer.PeerId),
                        input.ToRequestInfo(),
                        GetChatBannedRights(obj.BannedRights)
                        ,
                        input.UserId);
                    await _commandBus.PublishAsync(command, CancellationToken.None);
                }
                break;
            case PeerType.Channel:
                {
                    var command = new EditChannelDefaultBannedRightsCommand(ChannelId.Create(peer.PeerId),
                        input.ToRequestInfo(),
                       GetChatBannedRights(obj.BannedRights),
                        input.UserId);
                    await _commandBus.PublishAsync(command, CancellationToken.None);
                }
                break;
        }

        return null!;
    }

    private ChatBannedRights GetChatBannedRights(IChatBannedRights chatBannedRights)
    {
        return ChatBannedRights.FromValue(chatBannedRights.Flags.ToInt(), chatBannedRights.UntilDate);
    }
}
