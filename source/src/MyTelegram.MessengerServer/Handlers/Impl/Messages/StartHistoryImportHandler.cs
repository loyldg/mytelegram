using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class StartHistoryImportHandler : RpcResultObjectHandler<RequestStartHistoryImport, IBool>,
    IStartHistoryImportHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestStartHistoryImport obj)
    {
        throw new NotImplementedException();
    }
}