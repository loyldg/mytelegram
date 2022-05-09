using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class CheckHistoryImportHandler : RpcResultObjectHandler<RequestCheckHistoryImport, IHistoryImportParsed>,
    ICheckHistoryImportHandler
{
    protected override Task<IHistoryImportParsed> HandleCoreAsync(IRequestInput input,
        RequestCheckHistoryImport obj)
    {
        throw new NotImplementedException();
    }
}
