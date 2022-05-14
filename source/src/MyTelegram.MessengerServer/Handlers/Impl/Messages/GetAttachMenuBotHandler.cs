// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class GetAttachMenuBotHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAttachMenuBot, MyTelegram.Schema.IAttachMenuBotsBot>,
    Messages.IGetAttachMenuBotHandler
{
    protected override Task<MyTelegram.Schema.IAttachMenuBotsBot> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetAttachMenuBot obj)
    {
        throw new NotImplementedException();
    }
}
