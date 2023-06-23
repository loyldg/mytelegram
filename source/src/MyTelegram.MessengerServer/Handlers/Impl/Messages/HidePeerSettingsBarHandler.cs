using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class HidePeerSettingsBarHandler : RpcResultObjectHandler<RequestHidePeerSettingsBar, IBool>,
    IHidePeerSettingsBarHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestHidePeerSettingsBar obj)
    {
        throw new NotImplementedException();
    }
}