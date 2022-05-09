using MyTelegram.Handlers.Help;
using MyTelegram.Schema.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class SaveAppLogHandler : RpcResultObjectHandler<RequestSaveAppLog, IBool>,
    ISaveAppLogHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSaveAppLog obj)
    {
        throw new NotImplementedException();
    }
}
