// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

internal sealed class ToggleAntiSpamHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleAntiSpam, MyTelegram.Schema.IUpdates>,
    Channels.IToggleAntiSpamHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleAntiSpam obj)
    {
        throw new NotImplementedException();
    }
}
