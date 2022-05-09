using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class UpdateDialogFilterHandler : RpcResultObjectHandler<RequestUpdateDialogFilter, IBool>,
    IUpdateDialogFilterHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUpdateDialogFilter obj)
    {
        throw new NotImplementedException();
    }
}
