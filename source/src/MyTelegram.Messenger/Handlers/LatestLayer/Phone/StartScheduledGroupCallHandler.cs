namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Start a scheduled group call.
/// Possible errors
/// Code Type Description
/// 403 GROUPCALL_ALREADY_STARTED The groupcall has already started, you can join directly using <a href="https://corefork.telegram.org/method/phone.joinGroupCall">phone.joinGroupCall</a>.
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.startScheduledGroupCall"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class StartScheduledGroupCallHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestStartScheduledGroupCall, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestStartScheduledGroupCall obj)
    {
        throw new NotImplementedException();
    }
}