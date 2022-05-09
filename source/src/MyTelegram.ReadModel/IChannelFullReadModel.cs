namespace MyTelegram.ReadModel;

public interface IChannelFullReadModel : IReadModel
{
    string? About { get; }
    int AdminsCount { get; }
    int? AvailableMinId { get; }
    int BannedCount { get; }
    bool CanSetLocation { get; }
    bool CanSetStickers { get; }
    bool CanSetUserName { get; } //= true;

    bool CanViewParticipants { get; } //= true;
    bool CanViewStats { get; }
    long ChannelId { get; }
    int? FolderId { get; }
    bool HiddenPreHistory { get; }
    string Id { get; }
    int KickedCount { get; }
    long? LinkedChatId { get; }
    long? MigratedFromChatId { get; }
    int? MigratedFromMaxId { get; }
    int OnlineCount { get; }
    int? PinnedMsgId { get; }

    List<int> PinnedMsgIdList { get; }
    int ReadInboxMaxId { get; }
    int ReadOutboxMaxId { get; }
    int? SlowModeNextSendDate { get; }
    int? SlowModeSeconds { get; }
    int UnreadCount { get; }
    string? UserName { get; }
}