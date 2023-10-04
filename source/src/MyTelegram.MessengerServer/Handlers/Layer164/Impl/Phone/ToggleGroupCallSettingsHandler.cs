using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class ToggleGroupCallSettingsHandler : RpcResultObjectHandler<RequestToggleGroupCallSettings, IUpdates>,
    IToggleGroupCallSettingsHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestToggleGroupCallSettings obj)
    {
        throw new NotImplementedException();
    }
}