// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IAffectedMessages : IObject
{
    int Pts { get; set; }
    int PtsCount { get; set; }
}
