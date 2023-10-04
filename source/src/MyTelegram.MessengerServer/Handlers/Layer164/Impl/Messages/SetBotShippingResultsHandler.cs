using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SetBotShippingResultsHandler : RpcResultObjectHandler<RequestSetBotShippingResults, IBool>,
    ISetBotShippingResultsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetBotShippingResults obj)
    {
        throw new NotImplementedException();
    }
}