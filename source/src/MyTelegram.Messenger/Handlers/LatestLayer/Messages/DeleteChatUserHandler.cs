namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Deletes a user from a chat and sends a service message on it.
/// Possible errors
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// 400 USER_NOT_PARTICIPANT You're not a member of this supergroup/channel.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.deleteChatUser"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class DeleteChatUserHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteChatUser, MyTelegram.Schema.IUpdates>
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input, RequestDeleteChatUser obj)
    {
        throw new NotImplementedException();
    }
}