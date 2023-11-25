// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Channel statistics value pair
/// See <a href="https://corefork.telegram.org/constructor/StatsAbsValueAndPrev" />
///</summary>
[JsonDerivedType(typeof(TStatsAbsValueAndPrev), nameof(TStatsAbsValueAndPrev))]
public interface IStatsAbsValueAndPrev : IObject
{
    ///<summary>
    /// Current value
    ///</summary>
    double Current { get; set; }

    ///<summary>
    /// Previous value
    ///</summary>
    double Previous { get; set; }
}
