// ReSharper disable All

namespace MyTelegram.Schema.Stats;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/stats.StoryStats" />
///</summary>
[JsonDerivedType(typeof(TStoryStats), nameof(TStoryStats))]
public interface IStoryStats : IObject
{
    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/StatsGraph" />
    ///</summary>
    MyTelegram.Schema.IStatsGraph ViewsGraph { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/StatsGraph" />
    ///</summary>
    MyTelegram.Schema.IStatsGraph ReactionsByEmotionGraph { get; set; }
}
