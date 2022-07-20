// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

public class SaveCallLogHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestSaveCallLog, IBool>,
    Phone.ISaveCallLogHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestSaveCallLog obj)
    {
        throw new NotImplementedException();
    }
}
