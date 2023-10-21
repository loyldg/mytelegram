namespace MyTelegram.Messenger.Services.Interfaces;

public interface IPrivacyAppService
{
    Task<IReadOnlyCollection<IPrivacyReadModel>> GetPrivacyListAsync(IReadOnlyList<long> userIds);
    Task<IReadOnlyCollection<IPrivacyReadModel>> GetPrivacyListAsync(long userId);

    Task<IReadOnlyList<IPrivacyRule>> GetPrivacyRulesAsync(long selfUserId,
        IInputPrivacyKey key);

    Task<SetPrivacyOutput> SetPrivacyAsync(RequestInfo requestInfo,
        long selfUserId,
        IInputPrivacyKey key,
        IReadOnlyList<IInputPrivacyRule> ruleList);

    Task ApplyPrivacyAsync(long selfUserId, long targetUserId, Action executeOnPrivacyNotMatch,
        List<PrivacyType> privacyTypes);

    Task ApplyPrivacyListAsync(long selfUserId, IReadOnlyList<long> targetUserIdList, Action<long> executeOnPrivacyNotMatch,
        List<PrivacyType> privacyTypes);

    Task SetGlobalPrivacySettingsAsync(long selfUserId, bool? archiveAndMuteNewNoncontactPeers);
    Task<GlobalPrivacySettingsCacheItem?> GetGlobalPrivacySettingsAsync(long selfUserId);
}