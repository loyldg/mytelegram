// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetAttachMenuBotsHandler : RpcResultObjectHandler<RequestGetAttachMenuBots, Schema.IAttachMenuBots>,
    Messages.IGetAttachMenuBotsHandler, IProcessedHandler
{
    protected override Task<Schema.IAttachMenuBots> HandleCoreAsync(IRequestInput input,
        RequestGetAttachMenuBots obj)
    {
        return Task.FromResult<IAttachMenuBots>(new TAttachMenuBots
        {
            Bots = new TVector<IAttachMenuBot>(),
            Users = new TVector<IUser>(),
            Hash = 255 //obj.Hash
        });
    }
}
