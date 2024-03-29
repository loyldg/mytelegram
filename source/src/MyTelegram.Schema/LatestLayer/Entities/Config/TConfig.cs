﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Current configuration
/// See <a href="https://corefork.telegram.org/constructor/config" />
///</summary>
[TlObject(0xcc1a241e)]
public sealed class TConfig : IConfig
{
    public uint ConstructorId => 0xcc1a241e;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Whether the client should use P2P by default for phone calls with contacts
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool DefaultP2pContacts { get; set; }

    ///<summary>
    /// Whether the client should preload featured stickers
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool PreloadFeaturedStickers { get; set; }

    ///<summary>
    /// Whether incoming private messages can be deleted for both participants
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool RevokePmInbox { get; set; }

    ///<summary>
    /// Indicates that telegram is <em>probably</em> censored by governments/ISPs in the current region
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool BlockedMode { get; set; }

    ///<summary>
    /// Whether to forcefully connect using IPv6 <a href="https://corefork.telegram.org/type/DcOption">dcOptions</a>, even if the client knows that IPv4 is available.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool ForceTryIpv6 { get; set; }

    ///<summary>
    /// Current date at the server
    ///</summary>
    public int Date { get; set; }

    ///<summary>
    /// Expiration date of this config: when it expires it'll have to be refetched using <a href="https://corefork.telegram.org/method/help.getConfig">help.getConfig</a>
    ///</summary>
    public int Expires { get; set; }

    ///<summary>
    /// Whether we're connected to the test DCs
    /// See <a href="https://corefork.telegram.org/type/Bool" />
    ///</summary>
    public bool TestMode { get; set; }

    ///<summary>
    /// ID of the DC that returned the reply
    ///</summary>
    public int ThisDc { get; set; }

    ///<summary>
    /// DC IP list
    ///</summary>
    public TVector<MyTelegram.Schema.IDcOption> DcOptions { get; set; }

    ///<summary>
    /// Domain name for fetching encrypted DC list from DNS TXT record
    ///</summary>
    public string DcTxtDomainName { get; set; }

    ///<summary>
    /// Maximum member count for normal <a href="https://corefork.telegram.org/api/channel">groups</a>
    ///</summary>
    public int ChatSizeMax { get; set; }

    ///<summary>
    /// Maximum member count for <a href="https://corefork.telegram.org/api/channel">supergroups</a>
    ///</summary>
    public int MegagroupSizeMax { get; set; }

    ///<summary>
    /// Maximum number of messages that can be forwarded at once using <a href="https://corefork.telegram.org/method/messages.forwardMessages">messages.forwardMessages</a>.
    ///</summary>
    public int ForwardedCountMax { get; set; }

    ///<summary>
    /// The client should <a href="https://corefork.telegram.org/method/account.updateStatus">update its online status</a> every N milliseconds
    ///</summary>
    public int OnlineUpdatePeriodMs { get; set; }

    ///<summary>
    /// Delay before offline status needs to be sent to the server
    ///</summary>
    public int OfflineBlurTimeoutMs { get; set; }

    ///<summary>
    /// Time without any user activity after which it should be treated offline
    ///</summary>
    public int OfflineIdleTimeoutMs { get; set; }

    ///<summary>
    /// If we are offline, but were online from some other client in last <code>online_cloud_timeout_ms</code> milliseconds after we had gone offline, then delay offline notification for <code>notify_cloud_delay_ms</code> milliseconds.
    ///</summary>
    public int OnlineCloudTimeoutMs { get; set; }

    ///<summary>
    /// If we are offline, but online from some other client then delay sending the offline notification for <code>notify_cloud_delay_ms</code> milliseconds.
    ///</summary>
    public int NotifyCloudDelayMs { get; set; }

    ///<summary>
    /// If some other client is online, then delay notification for <code>notification_default_delay_ms</code> milliseconds
    ///</summary>
    public int NotifyDefaultDelayMs { get; set; }

    ///<summary>
    /// Not for client use
    ///</summary>
    public int PushChatPeriodMs { get; set; }

    ///<summary>
    /// Not for client use
    ///</summary>
    public int PushChatLimit { get; set; }

    ///<summary>
    /// Only messages with age smaller than the one specified can be edited
    ///</summary>
    public int EditTimeLimit { get; set; }

    ///<summary>
    /// Only channel/supergroup messages with age smaller than the specified can be deleted
    ///</summary>
    public int RevokeTimeLimit { get; set; }

    ///<summary>
    /// Only private messages with age smaller than the specified can be deleted
    ///</summary>
    public int RevokePmTimeLimit { get; set; }

    ///<summary>
    /// Exponential decay rate for computing <a href="https://corefork.telegram.org/api/top-rating">top peer rating</a>
    ///</summary>
    public int RatingEDecay { get; set; }

    ///<summary>
    /// Maximum number of recent stickers
    ///</summary>
    public int StickersRecentLimit { get; set; }

    ///<summary>
    /// Indicates that round videos (video notes) and voice messages sent in channels and older than the specified period must be marked as read
    ///</summary>
    public int ChannelsReadMediaPeriod { get; set; }

    ///<summary>
    /// Temporary <a href="https://corefork.telegram.org/passport">passport</a> sessions
    ///</summary>
    public int? TmpSessions { get; set; }

    ///<summary>
    /// Maximum allowed outgoing ring time in VoIP calls: if the user we're calling doesn't reply within the specified time (in milliseconds), we should hang up the call
    ///</summary>
    public int CallReceiveTimeoutMs { get; set; }

    ///<summary>
    /// Maximum allowed incoming ring time in VoIP calls: if the current user doesn't reply within the specified time (in milliseconds), the call will be automatically refused
    ///</summary>
    public int CallRingTimeoutMs { get; set; }

    ///<summary>
    /// VoIP connection timeout: if the instance of libtgvoip on the other side of the call doesn't connect to our instance of libtgvoip within the specified time (in milliseconds), the call must be aborted
    ///</summary>
    public int CallConnectTimeoutMs { get; set; }

    ///<summary>
    /// If during a VoIP call a packet isn't received for the specified period of time, the call must be aborted
    ///</summary>
    public int CallPacketTimeoutMs { get; set; }

    ///<summary>
    /// The domain to use to parse <a href="https://corefork.telegram.org/api/links">deep links »</a>.
    ///</summary>
    public string MeUrlPrefix { get; set; }

    ///<summary>
    /// URL to use to auto-update the current app
    ///</summary>
    public string? AutoupdateUrlPrefix { get; set; }

    ///<summary>
    /// Username of the bot to use to search for GIFs
    ///</summary>
    public string? GifSearchUsername { get; set; }

    ///<summary>
    /// Username of the bot to use to search for venues
    ///</summary>
    public string? VenueSearchUsername { get; set; }

    ///<summary>
    /// Username of the bot to use for image search
    ///</summary>
    public string? ImgSearchUsername { get; set; }

    ///<summary>
    /// ID of the map provider to use for venues
    ///</summary>
    public string? StaticMapsProvider { get; set; }

    ///<summary>
    /// Maximum length of caption (length in utf8 codepoints)
    ///</summary>
    public int CaptionLengthMax { get; set; }

    ///<summary>
    /// Maximum length of messages (length in utf8 codepoints)
    ///</summary>
    public int MessageLengthMax { get; set; }

    ///<summary>
    /// DC ID to use to download <a href="https://corefork.telegram.org/api/files#downloading-webfiles">webfiles</a>
    ///</summary>
    public int WebfileDcId { get; set; }

    ///<summary>
    /// Suggested language code
    ///</summary>
    public string? SuggestedLangCode { get; set; }

    ///<summary>
    /// Language pack version
    ///</summary>
    public int? LangPackVersion { get; set; }

    ///<summary>
    /// Basic language pack version
    ///</summary>
    public int? BaseLangPackVersion { get; set; }

    ///<summary>
    /// Default <a href="https://corefork.telegram.org/api/reactions">message reaction</a>
    /// See <a href="https://corefork.telegram.org/type/Reaction" />
    ///</summary>
    public MyTelegram.Schema.IReaction? ReactionsDefault { get; set; }

    ///<summary>
    /// Autologin token, <a href="https://corefork.telegram.org/api/url-authorization#link-url-authorization">click here for more info on URL authorization »</a>.
    ///</summary>
    public string? AutologinToken { get; set; }

    public void ComputeFlag()
    {
        if (DefaultP2pContacts) { Flags[3] = true; }
        if (PreloadFeaturedStickers) { Flags[4] = true; }
        if (RevokePmInbox) { Flags[6] = true; }
        if (BlockedMode) { Flags[8] = true; }
        if (ForceTryIpv6) { Flags[14] = true; }
        if (/*TmpSessions != 0 && */TmpSessions.HasValue) { Flags[0] = true; }
        if (AutoupdateUrlPrefix != null) { Flags[7] = true; }
        if (GifSearchUsername != null) { Flags[9] = true; }
        if (VenueSearchUsername != null) { Flags[10] = true; }
        if (ImgSearchUsername != null) { Flags[11] = true; }
        if (StaticMapsProvider != null) { Flags[12] = true; }
        if (SuggestedLangCode != null) { Flags[2] = true; }
        if (/*LangPackVersion != 0 && */LangPackVersion.HasValue) { Flags[2] = true; }
        if (/*BaseLangPackVersion != 0 && */BaseLangPackVersion.HasValue) { Flags[2] = true; }
        if (ReactionsDefault != null) { Flags[15] = true; }
        if (AutologinToken != null) { Flags[16] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Date);
        writer.Write(Expires);
        writer.Write(TestMode);
        writer.Write(ThisDc);
        writer.Write(DcOptions);
        writer.Write(DcTxtDomainName);
        writer.Write(ChatSizeMax);
        writer.Write(MegagroupSizeMax);
        writer.Write(ForwardedCountMax);
        writer.Write(OnlineUpdatePeriodMs);
        writer.Write(OfflineBlurTimeoutMs);
        writer.Write(OfflineIdleTimeoutMs);
        writer.Write(OnlineCloudTimeoutMs);
        writer.Write(NotifyCloudDelayMs);
        writer.Write(NotifyDefaultDelayMs);
        writer.Write(PushChatPeriodMs);
        writer.Write(PushChatLimit);
        writer.Write(EditTimeLimit);
        writer.Write(RevokeTimeLimit);
        writer.Write(RevokePmTimeLimit);
        writer.Write(RatingEDecay);
        writer.Write(StickersRecentLimit);
        writer.Write(ChannelsReadMediaPeriod);
        if (Flags[0]) { writer.Write(TmpSessions.Value); }
        writer.Write(CallReceiveTimeoutMs);
        writer.Write(CallRingTimeoutMs);
        writer.Write(CallConnectTimeoutMs);
        writer.Write(CallPacketTimeoutMs);
        writer.Write(MeUrlPrefix);
        if (Flags[7]) { writer.Write(AutoupdateUrlPrefix); }
        if (Flags[9]) { writer.Write(GifSearchUsername); }
        if (Flags[10]) { writer.Write(VenueSearchUsername); }
        if (Flags[11]) { writer.Write(ImgSearchUsername); }
        if (Flags[12]) { writer.Write(StaticMapsProvider); }
        writer.Write(CaptionLengthMax);
        writer.Write(MessageLengthMax);
        writer.Write(WebfileDcId);
        if (Flags[2]) { writer.Write(SuggestedLangCode); }
        if (Flags[2]) { writer.Write(LangPackVersion.Value); }
        if (Flags[2]) { writer.Write(BaseLangPackVersion.Value); }
        if (Flags[15]) { writer.Write(ReactionsDefault); }
        if (Flags[16]) { writer.Write(AutologinToken); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[3]) { DefaultP2pContacts = true; }
        if (Flags[4]) { PreloadFeaturedStickers = true; }
        if (Flags[6]) { RevokePmInbox = true; }
        if (Flags[8]) { BlockedMode = true; }
        if (Flags[14]) { ForceTryIpv6 = true; }
        Date = reader.ReadInt32();
        Expires = reader.ReadInt32();
        TestMode = reader.Read();
        ThisDc = reader.ReadInt32();
        DcOptions = reader.Read<TVector<MyTelegram.Schema.IDcOption>>();
        DcTxtDomainName = reader.ReadString();
        ChatSizeMax = reader.ReadInt32();
        MegagroupSizeMax = reader.ReadInt32();
        ForwardedCountMax = reader.ReadInt32();
        OnlineUpdatePeriodMs = reader.ReadInt32();
        OfflineBlurTimeoutMs = reader.ReadInt32();
        OfflineIdleTimeoutMs = reader.ReadInt32();
        OnlineCloudTimeoutMs = reader.ReadInt32();
        NotifyCloudDelayMs = reader.ReadInt32();
        NotifyDefaultDelayMs = reader.ReadInt32();
        PushChatPeriodMs = reader.ReadInt32();
        PushChatLimit = reader.ReadInt32();
        EditTimeLimit = reader.ReadInt32();
        RevokeTimeLimit = reader.ReadInt32();
        RevokePmTimeLimit = reader.ReadInt32();
        RatingEDecay = reader.ReadInt32();
        StickersRecentLimit = reader.ReadInt32();
        ChannelsReadMediaPeriod = reader.ReadInt32();
        if (Flags[0]) { TmpSessions = reader.ReadInt32(); }
        CallReceiveTimeoutMs = reader.ReadInt32();
        CallRingTimeoutMs = reader.ReadInt32();
        CallConnectTimeoutMs = reader.ReadInt32();
        CallPacketTimeoutMs = reader.ReadInt32();
        MeUrlPrefix = reader.ReadString();
        if (Flags[7]) { AutoupdateUrlPrefix = reader.ReadString(); }
        if (Flags[9]) { GifSearchUsername = reader.ReadString(); }
        if (Flags[10]) { VenueSearchUsername = reader.ReadString(); }
        if (Flags[11]) { ImgSearchUsername = reader.ReadString(); }
        if (Flags[12]) { StaticMapsProvider = reader.ReadString(); }
        CaptionLengthMax = reader.ReadInt32();
        MessageLengthMax = reader.ReadInt32();
        WebfileDcId = reader.ReadInt32();
        if (Flags[2]) { SuggestedLangCode = reader.ReadString(); }
        if (Flags[2]) { LangPackVersion = reader.ReadInt32(); }
        if (Flags[2]) { BaseLangPackVersion = reader.ReadInt32(); }
        if (Flags[15]) { ReactionsDefault = reader.Read<MyTelegram.Schema.IReaction>(); }
        if (Flags[16]) { AutologinToken = reader.ReadString(); }
    }
}