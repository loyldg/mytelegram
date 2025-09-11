namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Terminate webview interaction started with <a href="https://corefork.telegram.org/method/messages.requestWebView">messages.requestWebView</a>, sending the specified message to the chat on behalf of the user.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 QUERY_ID_INVALID The query ID is invalid.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// See <a href="https://corefork.telegram.org/method/messages.sendWebViewResultMessage" />
///</summary>
internal sealed class SendWebViewResultMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendWebViewResultMessage, MyTelegram.Schema.IWebViewMessageSent>
{
    protected override Task<MyTelegram.Schema.IWebViewMessageSent> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSendWebViewResultMessage obj)
    {
        throw new NotImplementedException();
    }
}
