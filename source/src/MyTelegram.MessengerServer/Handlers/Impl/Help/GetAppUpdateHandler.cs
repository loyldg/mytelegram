using MyTelegram.Handlers.Help;

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
