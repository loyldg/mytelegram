// ReSharper disable All

using MyTelegram.Schema.Bots;

namespace MyTelegram.Handlers.Bots;

public class SetBotBroadcastDefaultAdminRightsHandler :
    RpcResultObjectHandler<RequestSetBotBroadcastDefaultAdminRights, IBool>,
    Bots.ISetBotBroadcastDefaultAdminRightsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetBotBroadcastDefaultAdminRights obj)
    {
        throw new NotImplementedException();
    }
}