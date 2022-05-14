// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class GetAttachMenuBotsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAttachMenuBots, MyTelegram.Schema.IAttachMenuBots>,
    Messages.IGetAttachMenuBotsHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.IAttachMenuBots> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetAttachMenuBots obj)
    {
        return Task.FromResult<IAttachMenuBots>(new TAttachMenuBots
        {
            Bots = new TVector<IAttachMenuBot>(),
            Users = new TVector<IUser>(),
            Hash = 255,//obj.Hash
        });
    }
}
