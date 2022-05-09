using MyTelegram.Handlers.Help;
using MyTelegram.Schema.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class GetAppUpdateHandler : RpcResultObjectHandler<RequestGetAppUpdate, IAppUpdate>,
    IGetAppUpdateHandler
{
    protected override Task<IAppUpdate> HandleCoreAsync(IRequestInput input,
        RequestGetAppUpdate obj)
    {
        throw new NotImplementedException();
    }
}
