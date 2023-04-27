// ReSharper disable All

namespace MyTelegram.Schema;

public interface IStatsGroupTopAdmin : IObject
{
    long UserId { get; set; }
    int Deleted { get; set; }
    int Kicked { get; set; }
    int Banned { get; set; }
}
