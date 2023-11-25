// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Pin a message
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_ONESIDE_NOT_AVAIL Bots can't pin messages in PM just for themselves.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 PIN_RESTRICTED You can't pin messages.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// See <a href="https://corefork.telegram.org/method/messages.updatePinnedMessage" />
///</summary>
internal sealed class UpdatePinnedMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUpdatePinnedMessage, MyTelegram.Schema.IUpdates>,
    Messages.IUpdatePinnedMessageHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IRandomHelper _randomHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public UpdatePinnedMessageHandler(ICommandBus commandBus,
        IPeerHelper peerHelper,
        IRandomHelper randomHelper,
        IAccessHashHelper accessHashHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _randomHelper = randomHelper;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestUpdatePinnedMessage obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.Peer);
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        var ownerPeerId = peer.PeerType == PeerType.Channel ? peer.PeerId : input.UserId;
        var command = new StartUpdatePinnedMessageCommand(MessageId.Create(ownerPeerId, obj.Id),
            input.ToRequestInfo(),
            !obj.Unpin,
            obj.Silent,
            obj.PmOneside,
            CurrentDate,
            _randomHelper.NextLong(),
            new TMessageActionPinMessage().ToBytes().ToHexString());
        await _commandBus.PublishAsync(command, CancellationToken.None);
        return null!;
    }
}
