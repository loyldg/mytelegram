namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Indicate to the server (from the user side) that the user is still using a web app.If the method returns a <code>QUERY_ID_INVALID</code> error, the webview must be closed.
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.prolongWebView"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ProlongWebViewHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestProlongWebView, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestProlongWebView obj)
    {
        throw new NotImplementedException();
    }
}