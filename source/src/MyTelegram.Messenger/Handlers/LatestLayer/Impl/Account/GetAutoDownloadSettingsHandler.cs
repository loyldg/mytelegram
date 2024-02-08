// ReSharper disable All

using MyTelegram.Services.Services;

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get media autodownload settings
/// See <a href="https://corefork.telegram.org/method/account.getAutoDownloadSettings" />
///</summary>
internal sealed class GetAutoDownloadSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetAutoDownloadSettings, MyTelegram.Schema.Account.IAutoDownloadSettings>,
    Account.IGetAutoDownloadSettingsHandler
{
    protected override Task<MyTelegram.Schema.Account.IAutoDownloadSettings> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetAutoDownloadSettings obj)
    {
        var settings = new MyTelegram.Schema.TAutoDownloadSettings
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
        MyTelegram.Schema.Account.IAutoDownloadSettings r =
            new Schema.Account.TAutoDownloadSettings { High = settings, Low = settings, Medium = settings };

        return Task.FromResult(r);
    }
}