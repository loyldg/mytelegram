using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class ResetSavedHandler : RpcResultObjectHandler<RequestResetSaved, IBool>,
    IResetSavedHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestResetSaved obj)
    {
        throw new NotImplementedException();
    }
}