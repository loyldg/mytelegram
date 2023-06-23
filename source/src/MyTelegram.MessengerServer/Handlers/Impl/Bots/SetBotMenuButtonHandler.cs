// ReSharper disable All

using MyTelegram.Schema.Bots;

namespace MyTelegram.Handlers.Bots;

public class SetBotMenuButtonHandler : RpcResultObjectHandler<RequestSetBotMenuButton, IBool>,
    Bots.ISetBotMenuButtonHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetBotMenuButton obj)
    {
        throw new NotImplementedException();
    }
}