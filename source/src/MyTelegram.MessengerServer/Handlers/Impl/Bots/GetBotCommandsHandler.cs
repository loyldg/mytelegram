using MyTelegram.Handlers.Bots;
using MyTelegram.Schema.Bots;

namespace MyTelegram.MessengerServer.Handlers.Impl.Bots;

public class GetBotCommandsHandler : RpcResultObjectHandler<RequestGetBotCommands, TVector<IBotCommand>>,
    IGetBotCommandsHandler
{
    protected override Task<TVector<IBotCommand>> HandleCoreAsync(IRequestInput input,
        RequestGetBotCommands obj)
    {
        throw new NotImplementedException();
    }
}
