// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IBotCallbackAnswer : IObject
{
    BitArray Flags { get; set; }
    bool Alert { get; set; }
    bool HasUrl { get; set; }
    bool NativeUi { get; set; }
    string? Message { get; set; }
    string? Url { get; set; }
    int CacheTime { get; set; }

}
