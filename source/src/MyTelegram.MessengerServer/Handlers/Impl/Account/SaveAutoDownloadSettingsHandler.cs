using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class SaveAutoDownloadSettingsHandler : RpcResultObjectHandler<RequestSaveAutoDownloadSettings, IBool>,
    ISaveAutoDownloadSettingsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSaveAutoDownloadSettings obj)
    {
        throw new NotImplementedException();
    }
}
