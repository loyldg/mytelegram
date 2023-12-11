// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Get info about RTMP streams in a group call or livestream.<br>
/// This method should be invoked to the same group/channel-related DC used for <a href="https://corefork.telegram.org/api/files#downloading-files">downloading livestream chunks</a>.<br>
/// As usual, the media DC is preferred, if available.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// 400 GROUPCALL_JOIN_MISSING You haven't joined this group call.
/// See <a href="https://corefork.telegram.org/method/phone.getGroupCallStreamChannels" />
///</summary>
internal sealed class GetGroupCallStreamChannelsHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestGetGroupCallStreamChannels, MyTelegram.Schema.Phone.IGroupCallStreamChannels>,
    Phone.IGetGroupCallStreamChannelsHandler
{
    protected override Task<MyTelegram.Schema.Phone.IGroupCallStreamChannels> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestGetGroupCallStreamChannels obj)
    {
        throw new NotImplementedException();
    }
}
