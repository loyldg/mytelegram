// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get global privacy settings
/// See <a href="https://corefork.telegram.org/method/account.getGlobalPrivacySettings" />
///</summary>
internal sealed class GetGlobalPrivacySettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetGlobalPrivacySettings, MyTelegram.Schema.IGlobalPrivacySettings>,
    Account.IGetGlobalPrivacySettingsHandler, IProcessedHandler
{
    private readonly IPrivacyAppService _privacyAppService;

    public GetGlobalPrivacySettingsHandler(IPrivacyAppService privacyAppService)
    {
        _privacyAppService = privacyAppService;
    }

    protected override async Task<MyTelegram.Schema.IGlobalPrivacySettings> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetGlobalPrivacySettings obj)
    {
        var settings = await _privacyAppService.GetGlobalPrivacySettingsAsync(input.UserId);
        var globalPrivacySettings = new TGlobalPrivacySettings { ArchiveAndMuteNewNoncontactPeers = settings?.ArchiveAndMuteNewNoncontactPeers ?? false };

        return globalPrivacySettings;
    }
}
