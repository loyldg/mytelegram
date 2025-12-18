namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Changes chat name and sends a service message on it.
/// Possible errors
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 CHAT_TITLE_EMPTY No chat title provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.editChatTitle"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class EditChatTitleHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestEditChatTitle, MyTelegram.Schema.IUpdates>
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input, RequestEditChatTitle obj)
    {
        throw new NotImplementedException();
    }
}