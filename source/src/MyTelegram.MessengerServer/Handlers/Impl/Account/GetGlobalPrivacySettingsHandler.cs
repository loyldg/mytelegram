using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetGlobalPrivacySettingsHandler :
    RpcResultObjectHandler<RequestGetGlobalPrivacySettings, IGlobalPrivacySettings>,
    IGetGlobalPrivacySettingsHandler, IProcessedHandler
{
    protected override Task<IGlobalPrivacySettings> HandleCoreAsync(IRequestInput input,
        RequestGetGlobalPrivacySettings obj)
    {
        var globalPrivacySettings = new TGlobalPrivacySettings { ArchiveAndMuteNewNoncontactPeers = false };

        return Task.FromResult<IGlobalPrivacySettings>(globalPrivacySettings);
    }
}