using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SetBotPrecheckoutResultsHandler : RpcResultObjectHandler<RequestSetBotPrecheckoutResults, IBool>,
    ISetBotPrecheckoutResultsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetBotPrecheckoutResults obj)
    {
        throw new NotImplementedException();
    }
}