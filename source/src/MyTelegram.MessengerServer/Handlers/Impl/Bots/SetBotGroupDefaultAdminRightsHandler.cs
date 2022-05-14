// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

public class SetBotGroupDefaultAdminRightsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestSetBotGroupDefaultAdminRights, IBool>,
    Bots.ISetBotGroupDefaultAdminRightsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestSetBotGroupDefaultAdminRights obj)
    {
        throw new NotImplementedException();
    }
}
