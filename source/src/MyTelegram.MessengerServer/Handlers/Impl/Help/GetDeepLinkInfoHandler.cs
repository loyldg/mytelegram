using MyTelegram.Handlers.Help;
using MyTelegram.Schema.Help;

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
