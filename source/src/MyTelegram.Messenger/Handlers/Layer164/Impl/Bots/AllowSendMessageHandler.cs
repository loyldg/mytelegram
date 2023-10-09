// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

///<summary>
/// See <a href="https://corefork.telegram.org/method/bots.allowSendMessage" />
///</summary>
internal sealed class AllowSendMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestAllowSendMessage, MyTelegram.Schema.IUpdates>,
    Bots.IAllowSendMessageHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestAllowSendMessage obj)
    {
        throw new NotImplementedException();
    }
}
