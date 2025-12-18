namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Make a user admin in a <a href="https://corefork.telegram.org/api/channel#basic-groups">basic group</a>.
/// Possible errors
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// 400 USER_NOT_PARTICIPANT You're not a member of this supergroup/channel.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.editChatAdmin"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class EditChatAdminHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestEditChatAdmin, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input, RequestEditChatAdmin obj)
    {
        return new TBoolTrue();
    }
}