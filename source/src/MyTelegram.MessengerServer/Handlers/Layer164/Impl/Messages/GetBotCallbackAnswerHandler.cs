using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetBotCallbackAnswerHandler : RpcResultObjectHandler<RequestGetBotCallbackAnswer, IBotCallbackAnswer>,
    IGetBotCallbackAnswerHandler
{
    protected override Task<IBotCallbackAnswer> HandleCoreAsync(IRequestInput input,
        RequestGetBotCallbackAnswer obj)
    {
        throw new NotImplementedException();
    }
}