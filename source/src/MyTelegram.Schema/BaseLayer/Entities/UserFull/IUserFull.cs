// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object contains extended user info.
/// See <a href="https://corefork.telegram.org/constructor/UserFull" />
///</summary>
[JsonDerivedType(typeof(TUserFull), nameof(TUserFull))]
public interface IUserFull : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether you have blocked this user
    ///</summary>
    bool Blocked { get; set; }

    ///<summary>
    /// Whether this user can make VoIP calls
    ///</summary>
    bool PhoneCallsAvailable { get; set; }

    ///<summary>
    /// Whether this user's privacy settings allow you to call them
    ///</summary>
    bool PhoneCallsPrivate { get; set; }

    ///<summary>
    /// Whether you can pin messages in the chat with this user, you can do this only for a chat with yourself
    ///</summary>
    bool CanPinMessage { get; set; }

    ///<summary>
    /// Whether <a href="https://corefork.telegram.org/api/scheduled-messages">scheduled messages</a> are available
    ///</summary>
    bool HasScheduled { get; set; }

    ///<summary>
    /// Whether the user can receive video calls
    ///</summary>
    bool VideoCallsAvailable { get; set; }

    ///<summary>
    /// Whether this user doesn't allow sending voice messages in a private chat with them
    ///</summary>
    bool VoiceMessagesForbidden { get; set; }

    ///<summary>
    /// Whether the <a href="https://corefork.telegram.org/api/translation">real-time chat translation popup</a> should be hidden.
    ///</summary>
    bool TranslationsDisabled { get; set; }

    ///<summary>
    /// Whether this user has some <a href="https://corefork.telegram.org/api/stories#pinned-or-archived-stories">pinned stories</a>.
    ///</summary>
    bool StoriesPinnedAvailable { get; set; }

    ///<summary>
    /// Whether we've <a href="https://corefork.telegram.org/api/block">blocked this user, preventing them from seeing our stories »</a>.
    ///</summary>
    bool BlockedMyStoriesFrom { get; set; }

    ///<summary>
    /// Whether the other user has chosen a custom wallpaper for us using <a href="https://corefork.telegram.org/method/messages.setChatWallPaper">messages.setChatWallPaper</a> and the <code>for_both</code> flag, see <a href="https://corefork.telegram.org/api/wallpapers#installing-wallpapers-in-a-specific-chat">here »</a> for more info.
    ///</summary>
    bool WallpaperOverridden { get; set; }
    bool ContactRequirePremium { get; set; }
    bool ReadDatesPrivate { get; set; }

    ///<summary>
    /// User ID
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// Bio of the user
    ///</summary>
    string? About { get; set; }

    ///<summary>
    /// Peer settings
    /// See <a href="https://corefork.telegram.org/type/PeerSettings" />
    ///</summary>
    MyTelegram.Schema.IPeerSettings Settings { get; set; }

    ///<summary>
    /// Personal profile photo, to be shown instead of <code>profile_photo</code>.
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    MyTelegram.Schema.IPhoto? PersonalPhoto { get; set; }

    ///<summary>
    /// Profile photo
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    MyTelegram.Schema.IPhoto? ProfilePhoto { get; set; }

    ///<summary>
    /// Fallback profile photo, displayed if no photo is present in <code>profile_photo</code> or <code>personal_photo</code>, due to privacy settings.
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    MyTelegram.Schema.IPhoto? FallbackPhoto { get; set; }

    ///<summary>
    /// Notification settings
    /// See <a href="https://corefork.telegram.org/type/PeerNotifySettings" />
    ///</summary>
    MyTelegram.Schema.IPeerNotifySettings NotifySettings { get; set; }

    ///<summary>
    /// For bots, info about the bot (bot commands, etc)
    /// See <a href="https://corefork.telegram.org/type/BotInfo" />
    ///</summary>
    MyTelegram.Schema.IBotInfo? BotInfo { get; set; }

    ///<summary>
    /// Message ID of the last <a href="https://corefork.telegram.org/api/pin">pinned message</a>
    ///</summary>
    int? PinnedMsgId { get; set; }

    ///<summary>
    /// Chats in common with this user
    ///</summary>
    int CommonChatsCount { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/folders#peer-folders">Peer folder ID, for more info click here</a>
    ///</summary>
    int? FolderId { get; set; }

    ///<summary>
    /// Time To Live of all messages in this chat; once a message is this many seconds old, it must be deleted.
    ///</summary>
    int? TtlPeriod { get; set; }

    ///<summary>
    /// Emoji associated with chat theme
    ///</summary>
    string? ThemeEmoticon { get; set; }

    ///<summary>
    /// Anonymized text to be shown instead of the user's name on forwarded messages
    ///</summary>
    string? PrivateForwardName { get; set; }

    ///<summary>
    /// A <a href="https://corefork.telegram.org/api/rights#suggested-bot-rights">suggested set of administrator rights</a> for the bot, to be shown when adding the bot as admin to a group, see <a href="https://corefork.telegram.org/api/rights#suggested-bot-rights">here for more info on how to handle them »</a>.
    /// See <a href="https://corefork.telegram.org/type/ChatAdminRights" />
    ///</summary>
    MyTelegram.Schema.IChatAdminRights? BotGroupAdminRights { get; set; }

    ///<summary>
    /// A <a href="https://corefork.telegram.org/api/rights#suggested-bot-rights">suggested set of administrator rights</a> for the bot, to be shown when adding the bot as admin to a channel, see <a href="https://corefork.telegram.org/api/rights#suggested-bot-rights">here for more info on how to handle them »</a>.
    /// See <a href="https://corefork.telegram.org/type/ChatAdminRights" />
    ///</summary>
    MyTelegram.Schema.IChatAdminRights? BotBroadcastAdminRights { get; set; }

    ///<summary>
    /// Telegram Premium subscriptions gift options
    /// See <a href="https://corefork.telegram.org/type/PremiumGiftOption" />
    ///</summary>
    TVector<MyTelegram.Schema.IPremiumGiftOption>? PremiumGifts { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/wallpapers">Wallpaper</a> to use in the private chat with the user.
    /// See <a href="https://corefork.telegram.org/type/WallPaper" />
    ///</summary>
    MyTelegram.Schema.IWallPaper? Wallpaper { get; set; }

    ///<summary>
    /// Active <a href="https://corefork.telegram.org/api/stories">stories »</a>
    /// See <a href="https://corefork.telegram.org/type/PeerStories" />
    ///</summary>
    MyTelegram.Schema.IPeerStories? Stories { get; set; }
}
