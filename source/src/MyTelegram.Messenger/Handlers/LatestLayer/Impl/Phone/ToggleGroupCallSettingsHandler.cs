// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Change group call settings
/// <para>Possible errors</para>
/// Code Type Description
/// 400 GROUPCALL_NOT_MODIFIED Group call settings weren't modified.
/// See <a href="https://corefork.telegram.org/method/phone.toggleGroupCallSettings" />
///</summary>
internal sealed class ToggleGroupCallSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestToggleGroupCallSettings, MyTelegram.Schema.IUpdates>,
    Phone.IToggleGroupCallSettingsHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestToggleGroupCallSettings obj)
    {
        throw new NotImplementedException();
    }
}
