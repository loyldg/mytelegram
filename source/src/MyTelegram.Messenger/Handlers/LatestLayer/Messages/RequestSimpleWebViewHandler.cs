namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Open a <a href="https://corefork.telegram.org/api/bots/webapps">bot mini app</a>.
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// 400 URL_INVALID Invalid URL provided.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.requestSimpleWebView"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class RequestSimpleWebViewHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRequestSimpleWebView, MyTelegram.Schema.IWebViewResult>
{
    protected override Task<MyTelegram.Schema.IWebViewResult> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestRequestSimpleWebView obj)
    {
        throw new NotImplementedException();
    }
}