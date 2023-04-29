// ReSharper disable All

namespace MyTelegram.Schema;

public interface IUserFull : IObject
{
    BitArray Flags { get; set; }
    bool Blocked { get; set; }
    bool PhoneCallsAvailable { get; set; }
    bool PhoneCallsPrivate { get; set; }
    bool CanPinMessage { get; set; }
    bool HasScheduled { get; set; }
    bool VideoCallsAvailable { get; set; }
    bool VoiceMessagesForbidden { get; set; }
    bool TranslationsDisabled { get; set; }
    long Id { get; set; }
    string? About { get; set; }
    Schema.IPeerSettings Settings { get; set; }
    Schema.IPhoto? PersonalPhoto { get; set; }
    Schema.IPhoto? ProfilePhoto { get; set; }
    Schema.IPhoto? FallbackPhoto { get; set; }
    Schema.IPeerNotifySettings NotifySettings { get; set; }
    Schema.IBotInfo? BotInfo { get; set; }
    int? PinnedMsgId { get; set; }
    int CommonChatsCount { get; set; }
    int? FolderId { get; set; }
    int? TtlPeriod { get; set; }
    string? ThemeEmoticon { get; set; }
    string? PrivateForwardName { get; set; }
    Schema.IChatAdminRights? BotGroupAdminRights { get; set; }
    Schema.IChatAdminRights? BotBroadcastAdminRights { get; set; }
    TVector<Schema.IPremiumGiftOption>? PremiumGifts { get; set; }
    Schema.IWallPaper? Wallpaper { get; set; }
}
