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
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestDeleteChatUser obj)
    {
        throw new NotImplementedException();
    }
}
