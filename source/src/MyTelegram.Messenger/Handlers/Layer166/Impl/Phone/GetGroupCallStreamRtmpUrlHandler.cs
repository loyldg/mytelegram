// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Get RTMP URL and stream key for RTMP livestreams. Can be used even before creating the actual RTMP livestream with <a href="https://corefork.telegram.org/method/phone.createGroupCall">phone.createGroupCall</a> (the <code>rtmp_stream</code> flag must be set).
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// See <a href="https://corefork.telegram.org/method/phone.getGroupCallStreamRtmpUrl" />
///</summary>
internal sealed class GetGroupCallStreamRtmpUrlHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestGetGroupCallStreamRtmpUrl, MyTelegram.Schema.Phone.IGroupCallStreamRtmpUrl>,
    Phone.IGetGroupCallStreamRtmpUrlHandler
{
    protected override Task<MyTelegram.Schema.Phone.IGroupCallStreamRtmpUrl> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestGetGroupCallStreamRtmpUrl obj)
    {
        throw new NotImplementedException();
    }
}
