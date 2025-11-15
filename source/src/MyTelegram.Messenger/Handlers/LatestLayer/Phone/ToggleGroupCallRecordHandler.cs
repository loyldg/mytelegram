namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Start or stop recording a group call: the recorded audio and video streams will be automatically sent to <code>Saved messages</code> (the chat with ourselves).
/// Possible errors
/// Code Type Description
/// 403 GROUPCALL_FORBIDDEN The group call has already ended.
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// 400 GROUPCALL_NOT_MODIFIED Group call settings weren't modified.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.toggleGroupCallRecord"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleGroupCallRecordHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestToggleGroupCallRecord, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestToggleGroupCallRecord obj)
    {
        throw new NotImplementedException();
    }
}