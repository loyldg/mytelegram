namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Terminate webview interaction started with <a href="https://corefork.telegram.org/method/messages.requestWebView">messages.requestWebView</a>, sending the specified message to the chat on behalf of the user.
/// Possible errors
/// Code Type Description
/// 400 QUERY_ID_INVALID The query ID is invalid.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.sendWebViewResultMessage"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✖] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class SendWebViewResultMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendWebViewResultMessage, MyTelegram.Schema.IWebViewMessageSent>
{
    protected override Task<MyTelegram.Schema.IWebViewMessageSent> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSendWebViewResultMessage obj)
    {
        throw new NotImplementedException();
    }
}