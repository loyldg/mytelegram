// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

internal sealed class TogglePeerTranslationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestTogglePeerTranslations, IBool>,
    Messages.ITogglePeerTranslationsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestTogglePeerTranslations obj)
    {
        throw new NotImplementedException();
    }
}
