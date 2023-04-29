// ReSharper disable All

using MyTelegram.Schema.Bots;

namespace MyTelegram.Handlers.Bots;

public class GetBotMenuButtonHandler : RpcResultObjectHandler<RequestGetBotMenuButton, Schema.IBotMenuButton>,
    Bots.IGetBotMenuButtonHandler
{
    protected override Task<Schema.IBotMenuButton> HandleCoreAsync(IRequestInput input,
        RequestGetBotMenuButton obj)
    {
        throw new NotImplementedException();
    }
}
