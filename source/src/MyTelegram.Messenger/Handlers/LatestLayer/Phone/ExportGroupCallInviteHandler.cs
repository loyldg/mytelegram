namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Get an <a href="https://corefork.telegram.org/api/links#video-chat-livestream-links">invite link</a> for a group call or livestream
/// Possible errors
/// Code Type Description
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// 403 PUBLIC_CHANNEL_MISSING You can only export group call invite links for public chats or channels.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.exportGroupCallInvite"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ExportGroupCallInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestExportGroupCallInvite, MyTelegram.Schema.Phone.IExportedGroupCallInvite>
{
    protected override Task<MyTelegram.Schema.Phone.IExportedGroupCallInvite> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestExportGroupCallInvite obj)
    {
        throw new NotImplementedException();
    }
}