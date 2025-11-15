namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Send typing event by the current user to a secret chat.
/// Possible errors
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.setEncryptedTyping"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SetEncryptedTypingHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetEncryptedTyping, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSetEncryptedTyping obj)
    {
        throw new NotImplementedException();
    }
}