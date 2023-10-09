// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Start or stop recording a group call: the recorded audio and video streams will be automatically sent to <code>Saved messages</code> (the chat with ourselves).
/// <para>Possible errors</para>
/// Code Type Description
/// 403 GROUPCALL_FORBIDDEN The group call has already ended.
/// 400 GROUPCALL_NOT_MODIFIED Group call settings weren't modified.
/// See <a href="https://corefork.telegram.org/method/phone.toggleGroupCallRecord" />
///</summary>
internal sealed class ToggleGroupCallRecordHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestToggleGroupCallRecord, MyTelegram.Schema.IUpdates>,
    Phone.IToggleGroupCallRecordHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestToggleGroupCallRecord obj)
    {
        throw new NotImplementedException();
    }
}
