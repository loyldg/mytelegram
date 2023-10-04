// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object contains info on API configuring parameters.
/// See <a href="https://corefork.telegram.org/constructor/Config" />
///</summary>
public interface IConfig : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the client should use P2P by default for phone calls with contacts
    ///</summary>
    bool DefaultP2pContacts { get; set; }

    ///<summary>
    /// Whether the client should preload featured stickers
    ///</summary>
    bool PreloadFeaturedStickers { get; set; }

    ///<summary>
    /// Whether incoming private messages can be deleted for both participants
    ///</summary>
    bool RevokePmInbox { get; set; }

    ///<summary>
    /// Indicates that telegram is <em>probably</em> censored by governments/ISPs in the current region
    ///</summary>
    bool BlockedMode { get; set; }

    ///<summary>
    /// Whether to forcefully connect using IPv6 <a href="https://corefork.telegram.org/type/DcOption">dcOptions</a>, even if the client knows that IPv4 is available.
    ///</summary>
    bool ForceTryIpv6 { get; set; }

    ///<summary>
    /// Current date at the server
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// Expiration date of this config: when it expires it'll have to be refetched using <a href="https://corefork.telegram.org/method/help.getConfig">help.getConfig</a>
    ///</summary>
    int Expires { get; set; }

    ///<summary>
    /// Whether we're connected to the test DCs
    /// See <a href="https://corefork.telegram.org/type/Bool" />
    ///</summary>
    bool TestMode { get; set; }

    ///<summary>
    /// ID of the DC that returned the reply
    ///</summary>
    int ThisDc { get; set; }

    ///<summary>
    /// DC IP list
    /// See <a href="https://corefork.telegram.org/type/DcOption" />
    ///</summary>
    TVector<MyTelegram.Schema.IDcOption> DcOptions { get; set; }

    ///<summary>
    /// Domain name for fetching encrypted DC list from DNS TXT record
    ///</summary>
    string DcTxtDomainName { get; set; }

    ///<summary>
    /// Maximum member count for normal <a href="https://corefork.telegram.org/api/channel">groups</a>
    ///</summary>
    int ChatSizeMax { get; set; }

    ///<summary>
    /// Maximum member count for <a href="https://corefork.telegram.org/api/channel">supergroups</a>
    ///</summary>
    int MegagroupSizeMax { get; set; }

    ///<summary>
    /// Maximum number of messages that can be forwarded at once using <a href="https://corefork.telegram.org/method/messages.forwardMessages">messages.forwardMessages</a>.
    ///</summary>
    int ForwardedCountMax { get; set; }

    ///<summary>
    /// The client should <a href="https://corefork.telegram.org/method/account.updateStatus">update its online status</a> every N milliseconds
    ///</summary>
    int OnlineUpdatePeriodMs { get; set; }

    ///<summary>
    /// Delay before offline status needs to be sent to the server
    ///</summary>
    int OfflineBlurTimeoutMs { get; set; }

    ///<summary>
    /// Time without any user activity after which it should be treated offline
    ///</summary>
    int OfflineIdleTimeoutMs { get; set; }

    ///<summary>
    /// If we are offline, but were online from some other client in last <code>online_cloud_timeout_ms</code> milliseconds after we had gone offline, then delay offline notification for <code>notify_cloud_delay_ms</code> milliseconds.
    ///</summary>
    int OnlineCloudTimeoutMs { get; set; }

    ///<summary>
    /// If we are offline, but online from some other client then delay sending the offline notification for <code>notify_cloud_delay_ms</code> milliseconds.
    ///</summary>
    int NotifyCloudDelayMs { get; set; }

    ///<summary>
    /// If some other client is online, then delay notification for <code>notification_default_delay_ms</code> milliseconds
    ///</summary>
    int NotifyDefaultDelayMs { get; set; }

    ///<summary>
    /// Not for client use
    ///</summary>
    int PushChatPeriodMs { get; set; }

    ///<summary>
    /// Not for client use
    ///</summary>
    int PushChatLimit { get; set; }

    ///<summary>
    /// Only messages with age smaller than the one specified can be edited
    ///</summary>
    int EditTimeLimit { get; set; }

    ///<summary>
    /// Only channel/supergroup messages with age smaller than the specified can be deleted
    ///</summary>
    int RevokeTimeLimit { get; set; }

    ///<summary>
    /// Only private messages with age smaller than the specified can be deleted
    ///</summary>
    int RevokePmTimeLimit { get; set; }

    ///<summary>
    /// Exponential decay rate for computing <a href="https://corefork.telegram.org/api/top-rating">top peer rating</a>
    ///</summary>
    int RatingEDecay { get; set; }

    ///<summary>
    /// Maximum number of recent stickers
    ///</summary>
    int StickersRecentLimit { get; set; }

    ///<summary>
    /// Indicates that round videos (video notes) and voice messages sent in channels and older than the specified period must be marked as read
    ///</summary>
    int ChannelsReadMediaPeriod { get; set; }

    ///<summary>
    /// Temporary <a href="https://corefork.telegram.org/passport">passport</a> sessions
    ///</summary>
    int? TmpSessions { get; set; }

    ///<summary>
    /// Maximum allowed outgoing ring time in VoIP calls: if the user we're calling doesn't reply within the specified time (in milliseconds), we should hang up the call
    ///</summary>
    int CallReceiveTimeoutMs { get; set; }

    ///<summary>
    /// Maximum allowed incoming ring time in VoIP calls: if the current user doesn't reply within the specified time (in milliseconds), the call will be automatically refused
    ///</summary>
    int CallRingTimeoutMs { get; set; }

    ///<summary>
    /// VoIP connection timeout: if the instance of libtgvoip on the other side of the call doesn't connect to our instance of libtgvoip within the specified time (in milliseconds), the call must be aborted
    ///</summary>
    int CallConnectTimeoutMs { get; set; }

    ///<summary>
    /// If during a VoIP call a packet isn't received for the specified period of time, the call must be aborted
    ///</summary>
    int CallPacketTimeoutMs { get; set; }

    ///<summary>
    /// The domain to use to parse <a href="https://corefork.telegram.org/api/links">deep links »</a>.
    ///</summary>
    string MeUrlPrefix { get; set; }

    ///<summary>
    /// URL to use to auto-update the current app
    ///</summary>
    string? AutoupdateUrlPrefix { get; set; }

    ///<summary>
    /// Username of the bot to use to search for GIFs
    ///</summary>
    string? GifSearchUsername { get; set; }

    ///<summary>
    /// Username of the bot to use to search for venues
    ///</summary>
    string? VenueSearchUsername { get; set; }

    ///<summary>
    /// Username of the bot to use for image search
    ///</summary>
    string? ImgSearchUsername { get; set; }

    ///<summary>
    /// ID of the map provider to use for venues
    ///</summary>
    string? StaticMapsProvider { get; set; }

    ///<summary>
    /// Maximum length of caption (length in utf8 codepoints)
    ///</summary>
    int CaptionLengthMax { get; set; }

    ///<summary>
    /// Maximum length of messages (length in utf8 codepoints)
    ///</summary>
    int MessageLengthMax { get; set; }

    ///<summary>
    /// DC ID to use to download <a href="https://corefork.telegram.org/api/files#downloading-webfiles">webfiles</a>
    ///</summary>
    int WebfileDcId { get; set; }

    ///<summary>
    /// Suggested language code
    ///</summary>
    string? SuggestedLangCode { get; set; }

    ///<summary>
    /// Language pack version
    ///</summary>
    int? LangPackVersion { get; set; }

    ///<summary>
    /// Basic language pack version
    ///</summary>
    int? BaseLangPackVersion { get; set; }

    ///<summary>
    /// Default <a href="https://corefork.telegram.org/api/reactions">message reaction</a>
    /// See <a href="https://corefork.telegram.org/type/Reaction" />
    ///</summary>
    MyTelegram.Schema.IReaction? ReactionsDefault { get; set; }

    ///<summary>
    /// Autologin token, <a href="https://corefork.telegram.org/api/url-authorization#link-url-authorization">click here for more info on URL authorization »</a>.
    ///</summary>
    string? AutologinToken { get; set; }
}
