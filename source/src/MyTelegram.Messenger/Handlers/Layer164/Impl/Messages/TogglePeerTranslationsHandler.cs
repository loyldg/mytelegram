// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Show or hide the <a href="https://corefork.telegram.org/api/translation">real-time chat translation popup</a> for a certain chat
/// See <a href="https://corefork.telegram.org/method/messages.togglePeerTranslations" />
///</summary>
internal sealed class TogglePeerTranslationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestTogglePeerTranslations, IBool>,
    Messages.ITogglePeerTranslationsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestTogglePeerTranslations obj)
    {
        throw new NotImplementedException();
    }
}
