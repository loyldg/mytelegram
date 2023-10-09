// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Creates a new chat.May also return 0-N updates of type <a href="https://corefork.telegram.org/constructor/updateGroupInvitePrivacyForbidden">updateGroupInvitePrivacyForbidden</a>: it indicates we couldn't add a user to a chat because of their privacy settings; if required, an <a href="https://corefork.telegram.org/api/invites">invite link</a> can be shared with the user, instead.
/// <para>Possible errors</para>
/// Code Type Description
/// 500 CHAT_ID_GENERATE_FAILED Failure while generating the chat ID.
/// 400 CHAT_INVALID Invalid chat.
/// 400 CHAT_TITLE_EMPTY No chat title provided.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 USERS_TOO_FEW Not enough users (to create a chat, for example).
/// 406 USER_RESTRICTED You're spamreported, you can't create channels or chats.
/// See <a href="https://corefork.telegram.org/method/messages.createChat" />
///</summary>
internal sealed class CreateChatHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestCreateChat, MyTelegram.Schema.IUpdates>,
    Messages.ICreateChatHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestCreateChat obj)
    {
        throw new NotImplementedException();
    }
}
