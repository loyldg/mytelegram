namespace MyTelegram.Core;

public record GlobalPrivacySettingsCacheItem(bool ArchiveAndMuteNewNoncontactPeers, bool KeepArchivedUnmuted, bool KeepArchivedFolders, bool HideReadMarks, bool NewNoncontactPeersRequirePremium)
{
    public static string GetCacheKey(long userId) => MyCacheKey.With("global_privacy_settings", $"{userId}");
}