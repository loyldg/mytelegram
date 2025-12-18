namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Sends a message with a file attachment to a secret chat
/// Possible errors
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 DATA_TOO_LONG Data too long.
/// 400 ENCRYPTION_DECLINED The secret chat was declined.
/// 400 FILE_EMTPY An empty file was provided.
/// 400 MD5_CHECKSUM_INVALID The MD5 checksums do not match.
/// 400 MSG_WAIT_FAILED A waiting call returned an error.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.sendEncryptedFile"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SendEncryptedFileHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendEncryptedFile, MyTelegram.Schema.Messages.ISentEncryptedMessage>
{
    protected override Task<MyTelegram.Schema.Messages.ISentEncryptedMessage> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSendEncryptedFile obj)
    {
        throw new NotImplementedException();
    }
}