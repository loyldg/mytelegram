// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

public class SetBotMenuButtonHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestSetBotMenuButton, IBool>,
    Bots.ISetBotMenuButtonHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestSetBotMenuButton obj)
    {
        throw new NotImplementedException();
    }
}
