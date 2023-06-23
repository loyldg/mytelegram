// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetAttachMenuBotHandler : RpcResultObjectHandler<RequestGetAttachMenuBot, Schema.IAttachMenuBotsBot>,
    Messages.IGetAttachMenuBotHandler
{
    protected override Task<Schema.IAttachMenuBotsBot> HandleCoreAsync(IRequestInput input,
        RequestGetAttachMenuBot obj)
    {
        throw new NotImplementedException();
    }
}