// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IBotApp : IObject
{
    BitArray Flags { get; set; }
    bool Inactive { get; set; }
    bool RequestWriteAccess { get; set; }
    Schema.IBotApp App { get; set; }
}
