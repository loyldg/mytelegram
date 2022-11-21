// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

public class ToggleUsernameHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleUsername, IBool>,
    Channels.IToggleUsernameHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleUsername obj)
    {
        throw new NotImplementedException();
    }
}
