using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SetInlineGameScoreHandler : RpcResultObjectHandler<RequestSetInlineGameScore, IBool>,
    ISetInlineGameScoreHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetInlineGameScore obj)
    {
        throw new NotImplementedException();
    }
}