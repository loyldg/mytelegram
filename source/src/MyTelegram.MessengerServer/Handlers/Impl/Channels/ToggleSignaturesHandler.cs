using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class ToggleSignaturesHandler : RpcResultObjectHandler<RequestToggleSignatures, IUpdates>,
    IToggleSignaturesHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestToggleSignatures obj)
    {
        throw new NotImplementedException();
    }
}
