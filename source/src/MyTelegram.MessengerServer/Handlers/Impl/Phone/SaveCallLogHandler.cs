// ReSharper disable All

using MyTelegram.Schema.Phone;

namespace MyTelegram.Handlers.Phone;

public class SaveCallLogHandler : RpcResultObjectHandler<RequestSaveCallLog, IBool>,
    Phone.ISaveCallLogHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSaveCallLog obj)
    {
        throw new NotImplementedException();
    }
}
