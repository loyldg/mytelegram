using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class EditInlineBotMessageHandler : RpcResultObjectHandler<RequestEditInlineBotMessage, IBool>,
    IEditInlineBotMessageHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestEditInlineBotMessage obj)
    {
        throw new NotImplementedException();
    }
}
