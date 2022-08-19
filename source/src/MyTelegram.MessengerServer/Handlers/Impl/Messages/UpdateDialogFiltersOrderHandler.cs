using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class UpdateDialogFiltersOrderHandler : RpcResultObjectHandler<RequestUpdateDialogFiltersOrder, IBool>,
    IUpdateDialogFiltersOrderHandler, IProcessedHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUpdateDialogFiltersOrder obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
        //throw new NotImplementedException();
    }
}
