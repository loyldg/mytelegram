// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

public class GetBotMenuButtonHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestGetBotMenuButton, MyTelegram.Schema.IBotMenuButton>,
    Bots.IGetBotMenuButtonHandler
{
    protected override Task<MyTelegram.Schema.IBotMenuButton> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestGetBotMenuButton obj)
    {
        throw new NotImplementedException();
    }
}
