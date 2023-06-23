using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class GetAdminLogHandler : RpcResultObjectHandler<RequestGetAdminLog, IAdminLogResults>,
    IGetAdminLogHandler
{
    protected override Task<IAdminLogResults> HandleCoreAsync(IRequestInput input,
        RequestGetAdminLog obj)
    {
        throw new NotImplementedException();
    }
}