// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Returns installed attachment menu <a href="https://corefork.telegram.org/api/bots/attach">bot web apps »</a>
/// See <a href="https://corefork.telegram.org/method/messages.getAttachMenuBots" />
///</summary>
internal sealed class GetAttachMenuBotsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAttachMenuBots, MyTelegram.Schema.IAttachMenuBots>,
    Messages.IGetAttachMenuBotsHandler
{
    protected override Task<MyTelegram.Schema.IAttachMenuBots> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetAttachMenuBots obj)
    {
        throw new NotImplementedException();
    }
}
