namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Open a <a href="https://corefork.telegram.org/api/bots/webapps#main-mini-apps">Main Mini App</a>.
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.requestMainWebView"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class RequestMainWebViewHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRequestMainWebView, MyTelegram.Schema.IWebViewResult>
{
    protected override Task<MyTelegram.Schema.IWebViewResult> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestRequestMainWebView obj)
    {
        throw new NotImplementedException();
    }
}