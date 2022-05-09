using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class UpdateDialogFiltersOrderHandler : RpcResultObjectHandler<RequestUpdateDialogFiltersOrder, IBool>,
    IUpdateDialogFiltersOrderHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUpdateDialogFiltersOrder obj)
    {
        throw new NotImplementedException();
    }
}
