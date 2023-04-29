// ReSharper disable All

namespace MyTelegram.Schema;

public interface IConfig : IObject
{
    BitArray Flags { get; set; }
    bool DefaultP2pContacts { get; set; }
    bool PreloadFeaturedStickers { get; set; }
    bool RevokePmInbox { get; set; }
    bool BlockedMode { get; set; }
    bool ForceTryIpv6 { get; set; }
    int Date { get; set; }
    int Expires { get; set; }
    bool TestMode { get; set; }
    int ThisDc { get; set; }
    TVector<Schema.IDcOption> DcOptions { get; set; }
    string DcTxtDomainName { get; set; }
    int ChatSizeMax { get; set; }
    int MegagroupSizeMax { get; set; }
    int ForwardedCountMax { get; set; }
    int OnlineUpdatePeriodMs { get; set; }
    int OfflineBlurTimeoutMs { get; set; }
    int OfflineIdleTimeoutMs { get; set; }
    int OnlineCloudTimeoutMs { get; set; }
    int NotifyCloudDelayMs { get; set; }
    int NotifyDefaultDelayMs { get; set; }
    int PushChatPeriodMs { get; set; }
    int PushChatLimit { get; set; }
    int EditTimeLimit { get; set; }
    int RevokeTimeLimit { get; set; }
    int RevokePmTimeLimit { get; set; }
    int RatingEDecay { get; set; }
    int StickersRecentLimit { get; set; }
    int ChannelsReadMediaPeriod { get; set; }
    int? TmpSessions { get; set; }
    int CallReceiveTimeoutMs { get; set; }
    int CallRingTimeoutMs { get; set; }
    int CallConnectTimeoutMs { get; set; }
    int CallPacketTimeoutMs { get; set; }
    string MeUrlPrefix { get; set; }
    string? AutoupdateUrlPrefix { get; set; }
    string? GifSearchUsername { get; set; }
    string? VenueSearchUsername { get; set; }
    string? ImgSearchUsername { get; set; }
    string? StaticMapsProvider { get; set; }
    int CaptionLengthMax { get; set; }
    int MessageLengthMax { get; set; }
    int WebfileDcId { get; set; }
    string? SuggestedLangCode { get; set; }
    int? LangPackVersion { get; set; }
    int? BaseLangPackVersion { get; set; }
    Schema.IReaction? ReactionsDefault { get; set; }
    string? AutologinToken { get; set; }
}
