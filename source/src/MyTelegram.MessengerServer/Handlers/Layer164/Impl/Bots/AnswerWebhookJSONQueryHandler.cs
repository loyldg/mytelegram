using MyTelegram.Handlers.Bots;
using MyTelegram.Schema.Bots;

namespace MyTelegram.MessengerServer.Handlers.Impl.Bots;

public class AnswerWebHookJsonQueryHandler : RpcResultObjectHandler<RequestAnswerWebhookJSONQuery, IBool>,
    IAnswerWebhookJSONQueryHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestAnswerWebhookJSONQuery obj)
    {
        throw new NotImplementedException();
    }
}