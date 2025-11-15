namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Cancels a request for creation and/or delete info on secret chat.
/// Possible errors
/// Code Type Description
/// 400 CHAT_ID_EMPTY The provided chat ID is empty.
/// 400 ENCRYPTION_ALREADY_ACCEPTED Secret chat already accepted.
/// 400 ENCRYPTION_ALREADY_DECLINED The secret chat was already declined.
/// 400 ENCRYPTION_ID_INVALID The provided secret chat ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.discardEncryption"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DiscardEncryptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDiscardEncryption, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestDiscardEncryption obj)
    {
        throw new NotImplementedException();
    }
}