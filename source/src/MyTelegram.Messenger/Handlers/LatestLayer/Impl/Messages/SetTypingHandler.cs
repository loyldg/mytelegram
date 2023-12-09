// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Sends a current user typing event (see <a href="https://corefork.telegram.org/type/SendMessageAction">SendMessageAction</a> for all event types) to a conversation partner or group.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 403 GROUPCALL_FORBIDDEN The group call has already ended.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// 403 USER_IS_BLOCKED You were blocked by this user.
/// 400 USER_IS_BOT Bots can't send messages to other bots.
/// See <a href="https://corefork.telegram.org/method/messages.setTyping" />
///</summary>
internal sealed class SetTypingHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetTyping, IBool>,
    Messages.ISetTypingHandler
{
    private readonly IBlockCacheAppService _blockCacheAppService;
    private readonly IObjectMessageSender _messageSender;
    private readonly IPeerHelper _peerHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public SetTypingHandler(IPeerHelper peerHelper,
        IObjectMessageSender messageSender,
        IBlockCacheAppService blockCacheAppService,
        IAccessHashHelper accessHashHelper)
    {
        _peerHelper = peerHelper;
        _messageSender = messageSender;
        _blockCacheAppService = blockCacheAppService;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetTyping obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.Peer);
        var userId = input.UserId;
        var peer = _peerHelper.GetPeer(obj.Peer, userId);
        IUpdate? update = null;
        switch (peer.PeerType)
        {
            case PeerType.Unknown:
                break;
            case PeerType.Self:
                break;
            case PeerType.User:
                update = new TUpdateUserTyping { Action = obj.Action, UserId = userId };

                if (await _blockCacheAppService.IsBlockedAsync(peer.PeerId, userId).ConfigureAwait(false))
                {
                    return new TBoolTrue();
                }

                break;
            case PeerType.Chat:
                update = new TUpdateChatUserTyping
                {
                    Action = obj.Action,
                    ChatId = peer.PeerId,
                    FromId = new TPeerUser { UserId = userId }
                    //UserId = session.UserId
                };
                break;
            case PeerType.Channel:
                update = new TUpdateChannelUserTyping
                {
                    Action = obj.Action,
                    ChannelId = peer.PeerId,
                    TopMsgId = obj.TopMsgId,
                    //UserId = session.UserId,
                    FromId = new TPeerUser { UserId = userId }
                };

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (update == null)
        {
            return new TBoolTrue();
        }

        var updateShort = new TUpdateShort { Date = CurrentDate, Update = update };
        await _messageSender.PushMessageToPeerAsync(peer, updateShort, excludeAuthKeyId: input.AuthKeyId);
        return new TBoolTrue();
    }
}
