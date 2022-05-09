using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class InitHistoryImportHandler : RpcResultObjectHandler<RequestInitHistoryImport, IHistoryImport>,
    IInitHistoryImportHandler
{
    protected override Task<IHistoryImport> HandleCoreAsync(IRequestInput input,
        RequestInitHistoryImport obj)
    {
        throw new NotImplementedException();
    }
}
