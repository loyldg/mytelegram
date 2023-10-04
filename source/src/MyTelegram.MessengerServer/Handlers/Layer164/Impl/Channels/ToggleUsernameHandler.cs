// ReSharper disable All

using MyTelegram.Schema.Channels;
using RequestToggleUsername = MyTelegram.Schema.Channels.RequestToggleUsername;

namespace MyTelegram.Handlers.Channels;

public class ToggleUsernameHandler : RpcResultObjectHandler<RequestToggleUsername, IBool>,
    Channels.IToggleUsernameHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestToggleUsername obj)
    {
        throw new NotImplementedException();
    }
}