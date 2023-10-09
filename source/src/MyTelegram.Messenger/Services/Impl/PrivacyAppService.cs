namespace MyTelegram.Messenger.Services.Impl;

public class PrivacyAppService : BaseAppService, IPrivacyAppService //, ISingletonDependency
{
    private readonly ICacheManager<GlobalPrivacySettingsCacheItem> _cacheManager;

    public PrivacyAppService(ICacheManager<GlobalPrivacySettingsCacheItem> cacheManager)
    {
        _cacheManager = cacheManager;
    }

    public Task<IReadOnlyCollection<IPrivacyReadModel>> GetPrivacyListAsync(IReadOnlyList<long> userIds)
    {
        return Task.FromResult<IReadOnlyCollection<IPrivacyReadModel>>(Array.Empty<IPrivacyReadModel>());
    }

    public Task<IReadOnlyCollection<IPrivacyReadModel>> GetPrivacyListAsync(long userId)
    {
        return GetPrivacyListAsync(new[] { userId });
    }

    public Task ApplyPrivacyAsync(long selfUserId, long targetUserId, Action executeOnPrivacyNotMatch, List<PrivacyType> privacyTypes)
    {
        return Task.CompletedTask;
    }

    public Task ApplyPrivacyListAsync(long selfUserId, IReadOnlyList<long> targetUserIdList, Action<long> executeOnPrivacyNotMatch,
        List<PrivacyType> privacyTypes)
    {
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<IPrivacyRule>> GetPrivacyRulesAsync(long selfUserId,
        IInputPrivacyKey key)
    {
        return Task.FromResult<IReadOnlyList<IPrivacyRule>>(Array.Empty<IPrivacyRule>());

    }

    public Task SetGlobalPrivacySettingsAsync(long selfUserId, bool? archiveAndMuteNewNoncontactPeers)
    {
        return _cacheManager.SetAsync(GetGlobalPrivacySettingsCacheKey(selfUserId),
              new GlobalPrivacySettingsCacheItem(archiveAndMuteNewNoncontactPeers));
    }

    public Task<GlobalPrivacySettingsCacheItem?> GetGlobalPrivacySettingsAsync(long selfUserId)
    {
        return _cacheManager.GetAsync(GetGlobalPrivacySettingsCacheKey(selfUserId));
    }



    private string GetGlobalPrivacySettingsCacheKey(long selfUserId) => MyCacheKey.With("global_privacy", $"{selfUserId}");

    public async Task<SetPrivacyOutput> SetPrivacyAsync(RequestInfo requestInfo,
        long selfUserId,
        IInputPrivacyKey key,
        IReadOnlyList<IInputPrivacyRule> ruleList)
    {
        return new SetPrivacyOutput(new List<IPrivacyRule>());
    }

    // ReSharper disable once UnusedParameter.Local
    private static List<IPrivacyRule> GetDefaultPrivacyRules(PrivacyType privacyType)
    {
        return new List<IPrivacyRule> { new TPrivacyValueAllowAll() };
    }

    private List<long> GetUserIds(TVector<IInputUser> users)
    {
        return users.Select(GetUserId).Where(p => p != 0).ToList();
    }

    private long GetUserId(IInputUser user)
    {
        switch (user)
        {
            case TInputUser inputUser:
                return inputUser.UserId;
                break;
            case TInputUserEmpty inputUserEmpty:
                break;
            case TInputUserFromMessage inputUserFromMessage:
                return inputUserFromMessage.UserId;
                break;
            case TInputUserSelf inputUserSelf:
                break;
        }

        return 0;
    }
}