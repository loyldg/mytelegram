namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Upload encrypted file and associate it to a secret chat
/// See <a href="https://corefork.telegram.org/method/messages.uploadEncryptedFile" />
///</summary>
internal sealed class UploadEncryptedFileHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUploadEncryptedFile, MyTelegram.Schema.IEncryptedFile>
{
    protected override Task<MyTelegram.Schema.IEncryptedFile> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestUploadEncryptedFile obj)
    {
        throw new NotImplementedException();
    }
}
