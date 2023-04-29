// ReSharper disable All

namespace MyTelegram.Schema;

public interface ILayeredChannelFull : IChatFull
{
    bool CanViewParticipants { get; set; }

    bool CanSetUsername { get; set; }

    //bool CanSetStickers { get; set; }
    //bool HiddenPrehistory { get; set; }
    //bool CanSetLocation { get; set; }
    //bool HasScheduled { get; set; }
    //bool CanViewStats { get; set; }
    //bool Blocked { get; set; }
    bool CanDeleteChannel { get; set; }

    int? ParticipantsCount { get; set; }
    //int? AdminsCount { get; set; }
    //int? KickedCount { get; set; }
    //int? BannedCount { get; set; }
    //int? OnlineCount { get; set; }
    //int ReadInboxMaxId { get; set; }
    //int ReadOutboxMaxId { get; set; }
    //int UnreadCount { get; set; }

    /// <summary>
    ///     See <a href="https://core.telegram.org/type/Photo" />
    /// </summary>
    Schema.IPhoto ChatPhoto { get; set; }

    /////<summary>
    /////See <a href="https://core.telegram.org/type/PeerNotifySettings" />
    /////</summary>
    //MyTelegram.Schema.IPeerNotifySettings NotifySettings { get; set; }

    /////<summary>
    /////See <a href="https://core.telegram.org/type/ExportedChatInvite" />
    /////</summary>
    //MyTelegram.Schema.IExportedChatInvite? ExportedInvite { get; set; }
    TVector<Schema.IBotInfo> BotInfo { get; set; }
    //long? MigratedFromChatId { get; set; }
    //int? MigratedFromMaxId { get; set; }
    //int? PinnedMsgId { get; set; }

    /////<summary>
    /////See <a href="https://core.telegram.org/type/StickerSet" />
    /////</summary>
    //MyTelegram.Schema.IStickerSet? Stickerset { get; set; }
    //int? AvailableMinId { get; set; }
    //int? FolderId { get; set; }
    //long? LinkedChatId { get; set; }

    /////<summary>
    /////See <a href="https://core.telegram.org/type/ChannelLocation" />
    /////</summary>
    //MyTelegram.Schema.IChannelLocation? Location { get; set; }
    int? SlowmodeSeconds { get; set; }

    int? SlowmodeNextSendDate { get; set; }

    //int? StatsDc { get; set; }
    int Pts { get; set; }

    /////<summary>
    /////See <a href="https://core.telegram.org/type/InputGroupCall" />
    /////</summary>
    //MyTelegram.Schema.IInputGroupCall? Call { get; set; }
    //int? TtlPeriod { get; set; }
    //TVector<string>? PendingSuggestions { get; set; }

    /////<summary>
    /////See <a href="https://core.telegram.org/type/Peer" />
    /////</summary>
    //MyTelegram.Schema.IPeer? GroupcallDefaultJoinAs { get; set; }
    //string? ThemeEmoticon { get; set; }
    //int? RequestsPending { get; set; }
    //TVector<long>? RecentRequesters { get; set; }

    /////<summary>
    /////See <a href="https://core.telegram.org/type/Peer" />
    /////</summary>
    //MyTelegram.Schema.IPeer? DefaultSendAs { get; set; }
    //TVector<string>? AvailableReactions { get; set; }
}
