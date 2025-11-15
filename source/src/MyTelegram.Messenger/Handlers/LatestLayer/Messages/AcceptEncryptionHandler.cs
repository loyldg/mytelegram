namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Confirms creation of a secret chat
/// Possible errors
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 ENCRYPTION_ALREADY_ACCEPTED Secret chat already accepted.
/// 400 ENCRYPTION_ALREADY_DECLINED The secret chat was already declined.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.acceptEncryption"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class AcceptEncryptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestAcceptEncryption, MyTelegram.Schema.IEncryptedChat>
{
    protected override Task<MyTelegram.Schema.IEncryptedChat> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestAcceptEncryption obj)
    {
        throw new NotImplementedException();
    }
}