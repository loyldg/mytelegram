using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SetGameScoreHandler : RpcResultObjectHandler<RequestSetGameScore, IUpdates>,
    ISetGameScoreHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSetGameScore obj)
    {
        throw new NotImplementedException();
    }
}