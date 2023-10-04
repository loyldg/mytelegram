using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class UploadThemeHandler : RpcResultObjectHandler<RequestUploadTheme, IDocument>,
    IUploadThemeHandler
{
    protected override Task<IDocument> HandleCoreAsync(IRequestInput input,
        RequestUploadTheme obj)
    {
        throw new NotImplementedException();
    }
}