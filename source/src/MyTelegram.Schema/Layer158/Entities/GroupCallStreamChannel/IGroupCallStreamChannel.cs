// ReSharper disable All

namespace MyTelegram.Schema;

public interface IGroupCallStreamChannel : IObject
{
    int Channel { get; set; }
    int Scale { get; set; }
    long LastTimestampMs { get; set; }
}
