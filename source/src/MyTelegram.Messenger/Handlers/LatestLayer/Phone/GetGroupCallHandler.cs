namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Get info about a group call
/// Possible errors
/// Code Type Description
/// 403 GROUPCALL_FORBIDDEN The group call has already ended.
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.getGroupCall"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetGroupCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestGetGroupCall, MyTelegram.Schema.Phone.IGroupCall>
{
    protected override Task<MyTelegram.Schema.Phone.IGroupCall> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestGetGroupCall obj)
    {
        throw new NotImplementedException();
    }
}