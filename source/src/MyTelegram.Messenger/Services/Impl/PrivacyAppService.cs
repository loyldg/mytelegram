namespace MyTelegram.Messenger.Services.Impl;

public class PrivacyAppService : BaseAppService, IPrivacyAppService
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ICacheManager<GlobalPrivacySettingsCacheItem> _cacheManager;

    public PrivacyAppService(ICacheManager<GlobalPrivacySettingsCacheItem> cacheManager, IQueryProcessor queryProcessor)
    {
        _cacheManager = cacheManager;
        _queryProcessor = queryProcessor;
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

    public Task SetGlobalPrivacySettingsAsync(long selfUserId, GlobalPrivacySettings globalPrivacySettings)
    {
        throw new NotImplementedException();
    }

    public async Task<GlobalPrivacySettingsCacheItem?> GetGlobalPrivacySettingsAsync(long userId)
    {
        var cacheKey = GlobalPrivacySettingsCacheItem.GetCacheKey(userId);
        var item = await _cacheManager.GetAsync(cacheKey);
        var globalPrivacySettings = await _queryProcessor.ProcessAsync(new GetGlobalPrivacySettingsQuery(userId));
        if (globalPrivacySettings != null)
        {
            item = new(globalPrivacySettings.ArchiveAndMuteNewNoncontactPeers,
                globalPrivacySettings.KeepArchivedUnmuted, globalPrivacySettings.KeepArchivedFolders,
                globalPrivacySettings.HideReadMarks, globalPrivacySettings.NewNoncontactPeersRequirePremium);
            await _cacheManager.SetAsync(cacheKey, item);
        }
        return item;
    }

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
            case TInputUserEmpty:
                break;
            case TInputUserFromMessage inputUserFromMessage:
                return inputUserFromMessage.UserId;
            case TInputUserSelf:
                break;
        }

        return 0;
    }
}