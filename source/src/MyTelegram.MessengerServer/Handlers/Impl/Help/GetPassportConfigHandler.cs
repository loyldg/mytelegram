using MyTelegram.Handlers.Help;
using MyTelegram.Schema.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class GetPassportConfigHandler : RpcResultObjectHandler<RequestGetPassportConfig, IPassportConfig>,
    IGetPassportConfigHandler
{
    protected override Task<IPassportConfig> HandleCoreAsync(IRequestInput input,
        RequestGetPassportConfig obj)
    {
        throw new NotImplementedException();
    }
}
