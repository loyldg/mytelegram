// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Adds a user to a chat and sends a service message on it.May also return 0-N updates of type <a href="https://corefork.telegram.org/constructor/updateGroupInvitePrivacyForbidden">updateGroupInvitePrivacyForbidden</a>: it indicates we couldn't add a user to a chat because of their privacy settings; if required, an <a href="https://corefork.telegram.org/api/invites">invite link</a> can be shared with the user, instead.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_GROUPS_BLOCKED This bot can't be added to groups.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USERS_TOO_MUCH The maximum number of users has been exceeded (to create a chat, for example).
/// 400 USER_ALREADY_PARTICIPANT The user is already in the group.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// 400 USER_IS_BLOCKED You were blocked by this user.
/// 403 USER_NOT_MUTUAL_CONTACT The provided user is not a mutual contact.
/// 403 USER_PRIVACY_RESTRICTED The user's privacy settings do not allow you to do this.
/// 400 YOU_BLOCKED_USER You blocked this user.
/// See <a href="https://corefork.telegram.org/method/messages.addChatUser" />
///</summary>
internal sealed class AddChatUserHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestAddChatUser, MyTelegram.Schema.IUpdates>,
    Messages.IAddChatUserHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IRandomHelper _randomHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IPrivacyAppService _privacyAppService;
    public AddChatUserHandler(ICommandBus commandBus,
        IPeerHelper peerHelper,
        IRandomHelper randomHelper,
        IAccessHashHelper accessHashHelper,
        IPrivacyAppService privacyAppService)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _randomHelper = randomHelper;
        _accessHashHelper = accessHashHelper;
        _privacyAppService = privacyAppService;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestAddChatUser obj)
    {
        if (obj.UserId is TInputUser inputUser)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputUser.UserId, inputUser.AccessHash);
            await _privacyAppService.ApplyPrivacyAsync(input.UserId,
                inputUser.UserId,
                () => RpcErrors.RpcErrors403.UserPrivacyRestricted.ThrowRpcError(),
                new List<PrivacyType>
                {
                    PrivacyType.ChatInvite
                });

            var peer = _peerHelper.GetPeer(obj.UserId);
            var command = new AddChatUserCommand(ChatId.Create(obj.ChatId),
                input.ToRequestInfo(),
                peer.PeerId,
                CurrentDate,
                new TMessageActionChatAddUser { Users = new TVector<long>(peer.PeerId) }.ToBytes().ToHexString(),
                _randomHelper.NextLong());
            await _commandBus.PublishAsync(command, CancellationToken.None);
            return null!;
        }

        throw new NotImplementedException();
    }
}
