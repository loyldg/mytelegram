using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class UploadEncryptedFileHandler : RpcResultObjectHandler<RequestUploadEncryptedFile, IEncryptedFile>,
    IUploadEncryptedFileHandler
{
    protected override Task<IEncryptedFile> HandleCoreAsync(IRequestInput input,
        RequestUploadEncryptedFile obj)
    {
        throw new NotImplementedException();
    }
}