namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Get RTMP URL and stream key for RTMP livestreams. Can be used even before creating the actual RTMP livestream with <a href="https://corefork.telegram.org/method/phone.createGroupCall">phone.createGroupCall</a> (the <code>rtmp_stream</code> flag must be set).
/// Possible errors
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.getGroupCallStreamRtmpUrl"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetGroupCallStreamRtmpUrlHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestGetGroupCallStreamRtmpUrl, MyTelegram.Schema.Phone.IGroupCallStreamRtmpUrl>
{
    protected override Task<MyTelegram.Schema.Phone.IGroupCallStreamRtmpUrl> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestGetGroupCallStreamRtmpUrl obj)
    {
        throw new NotImplementedException();
    }
}