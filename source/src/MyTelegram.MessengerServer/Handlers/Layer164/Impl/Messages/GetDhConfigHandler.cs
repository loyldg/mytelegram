using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetDhConfigHandler : RpcResultObjectHandler<RequestGetDhConfig, IDhConfig>,
    IGetDhConfigHandler
{
    protected override Task<IDhConfig> HandleCoreAsync(IRequestInput input,
        RequestGetDhConfig obj)
    {
        throw new NotImplementedException();
    }
}