// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IAffectedFoundMessages : IObject
{
    int Pts { get; set; }
    int PtsCount { get; set; }
    int Offset { get; set; }
    TVector<int> Messages { get; set; }
}
