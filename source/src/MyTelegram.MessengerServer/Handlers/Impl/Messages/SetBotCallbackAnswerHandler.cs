using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SetBotCallbackAnswerHandler : RpcResultObjectHandler<RequestSetBotCallbackAnswer, IBool>,
    ISetBotCallbackAnswerHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetBotCallbackAnswer obj)
    {
        throw new NotImplementedException();
    }
}
