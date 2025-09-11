namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Open a <a href="https://corefork.telegram.org/api/bots/webapps">bot mini app</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// 400 URL_INVALID Invalid URL provided.
/// See <a href="https://corefork.telegram.org/method/messages.requestSimpleWebView" />
///</summary>
internal sealed class RequestSimpleWebViewHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRequestSimpleWebView, MyTelegram.Schema.IWebViewResult>
{
    protected override Task<MyTelegram.Schema.IWebViewResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestRequestSimpleWebView obj)
    {
        throw new NotImplementedException();
    }
}
