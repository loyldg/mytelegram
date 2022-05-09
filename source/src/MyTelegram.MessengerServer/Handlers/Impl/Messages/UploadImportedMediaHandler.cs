using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class UploadImportedMediaHandler : RpcResultObjectHandler<RequestUploadImportedMedia, IMessageMedia>,
    IUploadImportedMediaHandler
{
    protected override Task<IMessageMedia> HandleCoreAsync(IRequestInput input,
        RequestUploadImportedMedia obj)
    {
        throw new NotImplementedException();
    }
}
