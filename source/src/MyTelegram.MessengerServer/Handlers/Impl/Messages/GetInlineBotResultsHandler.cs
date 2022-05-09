using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetInlineBotResultsHandler : RpcResultObjectHandler<RequestGetInlineBotResults, IBotResults>,
    IGetInlineBotResultsHandler
{
    protected override Task<IBotResults> HandleCoreAsync(IRequestInput input,
        RequestGetInlineBotResults obj)
    {
        throw new NotImplementedException();
    }
}
