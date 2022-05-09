using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetPollResultsHandler : RpcResultObjectHandler<RequestGetPollResults, IUpdates>,
    IGetPollResultsHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestGetPollResults obj)
    {
        throw new NotImplementedException();
    }
}
