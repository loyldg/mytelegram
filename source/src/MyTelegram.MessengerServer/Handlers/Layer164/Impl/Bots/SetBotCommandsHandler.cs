using MyTelegram.Handlers.Bots;
using MyTelegram.Schema.Bots;

namespace MyTelegram.MessengerServer.Handlers.Impl.Bots;

public class SetBotCommandsHandler : RpcResultObjectHandler<RequestSetBotCommands, IBool>,
    ISetBotCommandsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetBotCommands obj)
    {
        throw new NotImplementedException();
    }
}