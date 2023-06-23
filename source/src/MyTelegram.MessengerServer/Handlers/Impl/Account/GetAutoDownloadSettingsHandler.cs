using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;
using IAutoDownloadSettings = MyTelegram.Schema.Account.IAutoDownloadSettings;
using TAutoDownloadSettings = MyTelegram.Schema.TAutoDownloadSettings;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetAutoDownloadSettingsHandler :
    RpcResultObjectHandler<RequestGetAutoDownloadSettings, IAutoDownloadSettings>,
    IGetAutoDownloadSettingsHandler, IProcessedHandler
{
    protected override Task<IAutoDownloadSettings> HandleCoreAsync(IRequestInput input,
        RequestGetAutoDownloadSettings obj)
    {
        var settings = new TAutoDownloadSettings
        {
            AudioPreloadNext = true,
            Disabled = false,
            FileSizeMax = 1024 * 1024 * 10,
            PhonecallsLessData = true,
            PhotoSizeMax = 1024 * 1024 * 10,
            VideoPreloadLarge = true,
            VideoSizeMax = 1024 * 1024 * 10,
            VideoUploadMaxbitrate = 1024 * 1024 * 4
        };
        IAutoDownloadSettings r =
            new Schema.Account.TAutoDownloadSettings { High = settings, Low = settings, Medium = settings };

        return Task.FromResult(r);
    }
}