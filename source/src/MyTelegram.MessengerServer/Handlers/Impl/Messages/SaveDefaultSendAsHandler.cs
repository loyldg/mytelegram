// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class SaveDefaultSendAsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSaveDefaultSendAs, IBool>,
    Messages.ISaveDefaultSendAsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSaveDefaultSendAs obj)
    {
        throw new NotImplementedException();
    }
}
