// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Get an <a href="https://corefork.telegram.org/api/links#video-chat-livestream-links">invite link</a> for a group call or livestream
/// <para>Possible errors</para>
/// Code Type Description
/// 403 PUBLIC_CHANNEL_MISSING You can only export group call invite links for public chats or channels.
/// See <a href="https://corefork.telegram.org/method/phone.exportGroupCallInvite" />
///</summary>
internal sealed class ExportGroupCallInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestExportGroupCallInvite, MyTelegram.Schema.Phone.IExportedGroupCallInvite>,
    Phone.IExportGroupCallInviteHandler
{
    protected override Task<MyTelegram.Schema.Phone.IExportedGroupCallInvite> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestExportGroupCallInvite obj)
    {
        throw new NotImplementedException();
    }
}
