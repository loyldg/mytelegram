// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Deletes a user from a chat and sends a service message on it.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// 400 USER_NOT_PARTICIPANT You're not a member of this supergroup/channel.
/// See <a href="https://corefork.telegram.org/method/messages.deleteChatUser" />
///</summary>
internal sealed class DeleteChatUserHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteChatUser, MyTelegram.Schema.IUpdates>,
    Messages.IDeleteChatUserHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IRandomHelper _randomHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public DeleteChatUserHandler(ICommandBus commandBus,
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
        RequestDeleteChatUser obj)
    {
        if (obj.UserId is TInputUser inputUser)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputUser.UserId, inputUser.AccessHash);
        }

        var peer = _peerHelper.GetPeer(obj.UserId, input.UserId);
        var command = new DeleteChatUserCommand(ChatId.Create(obj.ChatId),
            input.ToRequestInfo(),
            peer.PeerId,
            new TMessageActionChatDeleteUser { UserId = peer.PeerId }.ToBytes().ToHexString(),
            _randomHelper.NextLong()
        );
        await _commandBus.PublishAsync(command, CancellationToken.None);

        return null!;
    }
}
