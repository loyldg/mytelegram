// ReSharper disable All

namespace MyTelegram.Schema;

public interface IStatsPercentValue : IObject
{
    double Part { get; set; }
    double Total { get; set; }
}
