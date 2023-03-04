// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IBotResults : IObject
{
    BitArray Flags { get; set; }
    bool Gallery { get; set; }
    long QueryId { get; set; }
    string? NextOffset { get; set; }
    MyTelegram.Schema.IInlineBotSwitchPM? SwitchPm { get; set; }
    TVector<MyTelegram.Schema.IBotInlineResult> Results { get; set; }
    int CacheTime { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
