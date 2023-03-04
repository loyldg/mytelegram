// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IAffectedHistory : IObject
{
    int Pts { get; set; }
    int PtsCount { get; set; }
    int Offset { get; set; }
}
