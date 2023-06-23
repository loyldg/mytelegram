using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SetInlineBotResultsHandler : RpcResultObjectHandler<RequestSetInlineBotResults, IBool>,
    ISetInlineBotResultsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetInlineBotResults obj)
    {
        throw new NotImplementedException();
    }
}