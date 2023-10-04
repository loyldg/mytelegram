using MyTelegram.Handlers.Bots;
using MyTelegram.Schema.Bots;

namespace MyTelegram.MessengerServer.Handlers.Impl.Bots;

public class ResetBotCommandsHandler : RpcResultObjectHandler<RequestResetBotCommands, IBool>,
    IResetBotCommandsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestResetBotCommands obj)
    {
        throw new NotImplementedException();
    }
}