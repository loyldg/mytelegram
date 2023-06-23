// ReSharper disable All

using MyTelegram.Schema.Bots;

namespace MyTelegram.Handlers.Bots;

public class SetBotGroupDefaultAdminRightsHandler : RpcResultObjectHandler<RequestSetBotGroupDefaultAdminRights, IBool>,
    Bots.ISetBotGroupDefaultAdminRightsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetBotGroupDefaultAdminRights obj)
    {
        throw new NotImplementedException();
    }
}