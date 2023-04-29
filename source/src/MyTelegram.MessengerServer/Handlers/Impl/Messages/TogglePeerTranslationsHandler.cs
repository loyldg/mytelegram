// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

internal sealed class TogglePeerTranslationsHandler : RpcResultObjectHandler<RequestTogglePeerTranslations, IBool>,
    Messages.ITogglePeerTranslationsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestTogglePeerTranslations obj)
    {
        throw new NotImplementedException();
    }
}
