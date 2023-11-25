// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Channel statistics percentage
/// See <a href="https://corefork.telegram.org/constructor/StatsPercentValue" />
///</summary>
[JsonDerivedType(typeof(TStatsPercentValue), nameof(TStatsPercentValue))]
public interface IStatsPercentValue : IObject
{
    ///<summary>
    /// Partial value
    ///</summary>
    double Part { get; set; }

    ///<summary>
    /// Total value
    ///</summary>
    double Total { get; set; }
}
