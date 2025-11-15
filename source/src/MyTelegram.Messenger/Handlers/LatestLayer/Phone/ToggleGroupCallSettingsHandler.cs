namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Change group call settings
/// Possible errors
/// Code Type Description
/// 400 GROUPCALL_INVALID The specified group call is invalid.
/// 400 GROUPCALL_NOT_MODIFIED Group call settings weren't modified.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.toggleGroupCallSettings"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleGroupCallSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestToggleGroupCallSettings, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestToggleGroupCallSettings obj)
    {
        throw new NotImplementedException();
    }
}