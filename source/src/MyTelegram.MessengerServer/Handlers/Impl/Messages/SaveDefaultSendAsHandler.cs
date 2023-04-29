// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class SaveDefaultSendAsHandler : RpcResultObjectHandler<RequestSaveDefaultSendAs, IBool>,
    Messages.ISaveDefaultSendAsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSaveDefaultSendAs obj)
    {
        throw new NotImplementedException();
    }
}
