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
    MyTelegram.Schema.IUser User { get; set; }
    string? About { get; set; }
    MyTelegram.Schema.IPeerSettings Settings { get; set; }
    MyTelegram.Schema.IPhoto? ProfilePhoto { get; set; }
    MyTelegram.Schema.IPeerNotifySettings NotifySettings { get; set; }
    MyTelegram.Schema.IBotInfo? BotInfo { get; set; }
    int? PinnedMsgId { get; set; }
    int CommonChatsCount { get; set; }
    int? FolderId { get; set; }
    int? TtlPeriod { get; set; }
    string? ThemeEmoticon { get; set; }

}
