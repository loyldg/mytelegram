using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class UpdateDialogFiltersOrderHandler : RpcResultObjectHandler<RequestUpdateDialogFiltersOrder, IBool>,
    IUpdateDialogFiltersOrderHandler, IProcessedHandler
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUpdateDialogFiltersOrder obj)
    {
        return new TBoolTrue();
        //throw new NotImplementedException();
    }
}
