namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Open a <a href="https://corefork.telegram.org/api/bots/webapps#main-mini-apps">Main Mini App</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// See <a href="https://corefork.telegram.org/method/messages.requestMainWebView" />
///</summary>
internal sealed class RequestMainWebViewHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRequestMainWebView, MyTelegram.Schema.IWebViewResult>
{
    protected override Task<MyTelegram.Schema.IWebViewResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestRequestMainWebView obj)
    {
        throw new NotImplementedException();
    }
}
