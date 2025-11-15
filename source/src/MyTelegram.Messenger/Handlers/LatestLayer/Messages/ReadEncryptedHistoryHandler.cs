namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Marks message history within a secret chat as read.
/// Possible errors
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 MAX_DATE_INVALID The specified maximum date is invalid.
/// 400 MSG_WAIT_FAILED A waiting call returned an error.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.readEncryptedHistory"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReadEncryptedHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadEncryptedHistory, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestReadEncryptedHistory obj)
    {
        throw new NotImplementedException();
    }
}