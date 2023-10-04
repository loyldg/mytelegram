// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Channel statistics date range
/// See <a href="https://corefork.telegram.org/constructor/StatsDateRangeDays" />
///</summary>
public interface IStatsDateRangeDays : IObject
{
    ///<summary>
    /// Initial date
    ///</summary>
    int MinDate { get; set; }

    ///<summary>
    /// Final date
    ///</summary>
    int MaxDate { get; set; }
}
