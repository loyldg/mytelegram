// ReSharper disable All

namespace MyTelegram.Schema;

public interface IStatsDateRangeDays : IObject
{
    int MinDate { get; set; }
    int MaxDate { get; set; }
}
