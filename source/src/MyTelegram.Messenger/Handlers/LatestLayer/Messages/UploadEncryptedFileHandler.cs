namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Upload encrypted file and associate it to a secret chat (without actually sending it to the chat).
/// Possible errors
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.uploadEncryptedFile"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UploadEncryptedFileHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUploadEncryptedFile, MyTelegram.Schema.IEncryptedFile>
{
    protected override Task<MyTelegram.Schema.IEncryptedFile> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestUploadEncryptedFile obj)
    {
        throw new NotImplementedException();
    }
}