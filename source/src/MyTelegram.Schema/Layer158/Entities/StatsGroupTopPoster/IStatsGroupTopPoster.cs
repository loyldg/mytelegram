// ReSharper disable All

namespace MyTelegram.Schema;

public interface IStatsGroupTopPoster : IObject
{
    long UserId { get; set; }
    int Messages { get; set; }
    int AvgChars { get; set; }
}
