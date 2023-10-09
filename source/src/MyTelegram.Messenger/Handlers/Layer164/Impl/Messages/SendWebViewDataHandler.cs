// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Used by the user to relay data from an opened <a href="https://corefork.telegram.org/api/bots/webapps">reply keyboard bot web app</a> to the bot that owns it.
/// See <a href="https://corefork.telegram.org/method/messages.sendWebViewData" />
///</summary>
internal sealed class SendWebViewDataHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendWebViewData, MyTelegram.Schema.IUpdates>,
    Messages.ISendWebViewDataHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSendWebViewData obj)
    {
        throw new NotImplementedException();
    }
}
