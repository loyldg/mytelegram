using MyTelegram.Handlers.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class GetDeepLinkInfoHandler : RpcResultObjectHandler<RequestGetDeepLinkInfo, IDeepLinkInfo>,
    IGetDeepLinkInfoHandler
{
    protected override Task<IDeepLinkInfo> HandleCoreAsync(IRequestInput input,
        RequestGetDeepLinkInfo obj)
    {
        throw new NotImplementedException();
    }
}
