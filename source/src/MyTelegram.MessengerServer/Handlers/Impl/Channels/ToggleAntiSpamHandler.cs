// ReSharper disable All

using MyTelegram.Schema.Channels;

namespace MyTelegram.Handlers.Channels;

internal sealed class ToggleAntiSpamHandler : RpcResultObjectHandler<RequestToggleAntiSpam, Schema.IUpdates>,
    Channels.IToggleAntiSpamHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestToggleAntiSpam obj)
    {
        throw new NotImplementedException();
    }
}