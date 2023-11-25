// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Channel statistics graph
/// See <a href="https://corefork.telegram.org/constructor/StatsGraph" />
///</summary>
[JsonDerivedType(typeof(TStatsGraphAsync), nameof(TStatsGraphAsync))]
[JsonDerivedType(typeof(TStatsGraphError), nameof(TStatsGraphError))]
[JsonDerivedType(typeof(TStatsGraph), nameof(TStatsGraph))]
public interface IStatsGraph : IObject
{

}
