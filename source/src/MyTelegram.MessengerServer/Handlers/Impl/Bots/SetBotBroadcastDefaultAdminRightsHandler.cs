// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

public class SetBotBroadcastDefaultAdminRightsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestSetBotBroadcastDefaultAdminRights, IBool>,
    Bots.ISetBotBroadcastDefaultAdminRightsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestSetBotBroadcastDefaultAdminRights obj)
    {
        throw new NotImplementedException();
    }
}
