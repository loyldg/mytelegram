// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IBotResults : IObject
{
    BitArray Flags { get; set; }
    bool Gallery { get; set; }
    long QueryId { get; set; }
    string? NextOffset { get; set; }
    Schema.IInlineBotSwitchPM? SwitchPm { get; set; }
    Schema.IInlineBotWebView? SwitchWebview { get; set; }
    TVector<Schema.IBotInlineResult> Results { get; set; }
    int CacheTime { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
