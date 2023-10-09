// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Should be called after the user hides the report spam/add as contact bar of a new chat, effectively prevents the user from executing the actions specified in the <a href="https://corefork.telegram.org/constructor/peerSettings">peer's settings</a>.
/// See <a href="https://corefork.telegram.org/method/messages.hidePeerSettingsBar" />
///</summary>
internal sealed class HidePeerSettingsBarHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestHidePeerSettingsBar, IBool>,
    Messages.IHidePeerSettingsBarHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestHidePeerSettingsBar obj)
    {
        throw new NotImplementedException();
    }
}
