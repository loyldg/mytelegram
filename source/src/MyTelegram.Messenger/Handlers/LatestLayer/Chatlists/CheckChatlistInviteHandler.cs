namespace MyTelegram.Messenger.Handlers.LatestLayer.Chatlists;
/// <summary>
/// Obtain information about a <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>.
/// Possible errors
/// Code Type Description
/// 400 INVITE_SLUG_EMPTY The specified invite slug is empty.
/// 400 INVITE_SLUG_EXPIRED The specified chat folder link has expired.
/// <para><c>See <a href="https://corefork.telegram.org/method/chatlists.checkChatlistInvite"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class CheckChatlistInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Chatlists.RequestCheckChatlistInvite, MyTelegram.Schema.Chatlists.IChatlistInvite>
{
    protected override Task<MyTelegram.Schema.Chatlists.IChatlistInvite> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Chatlists.RequestCheckChatlistInvite obj)
    {
        throw new NotImplementedException();
    }
}